using Ae.Galeriya.Core.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace Ae.Galeriya.Core
{
    public sealed class MediaInfoExtractor : IMediaInfoExtractor
    {
        private readonly ILogger<MediaInfoExtractor> _logger;

        public MediaInfoExtractor(ILogger<MediaInfoExtractor> logger)
        {
            _logger = logger;
        }

        public MediaInfoExtractor() : this(NullLogger<MediaInfoExtractor>.Instance)
        {
        }

        private IReadOnlyList<KeyValuePair<string, string>> GetTags(JsonDocument probeResultDocument, string element)
        {
            var tags = new List<KeyValuePair<string, string>>();

            if (probeResultDocument.RootElement.TryGetProperty(element, out JsonElement packetsElement))
            {
                foreach (var packet in packetsElement.EnumerateArray())
                {
                    if (packet.TryGetProperty("tags", out JsonElement packetTagsElement))
                    {
                        foreach (var item in packetTagsElement.EnumerateObject())
                        {
                            var valueString = item.Value.GetString();
                            if (valueString != null)
                            {
                                tags.Add(KeyValuePair.Create(item.Name, valueString.Trim()));
                            }
                        }
                    }
                }
            }

            return tags;
        }

        private ((int Width, int Height) Size, float? Duration) GetVideoStreamInfo(JsonDocument probeResultDocument)
        {
            if (!probeResultDocument.RootElement.TryGetProperty("streams", out JsonElement streamsElement))
            {
                return ((0, 0), null);
            }

            (int Width, int Height)? size = null;
            float? duration = null;
            foreach (var stream in streamsElement.EnumerateArray())
            {
                var codecType = stream.GetProperty("codec_type").GetString();
                if (codecType == "video")
                {
                    size = (stream.GetProperty("width").GetInt32(), stream.GetProperty("height").GetInt32());

                    if (stream.TryGetProperty("duration", out var durationElement))
                    {
                        var valueString = durationElement.GetString();
                        if (valueString != null && float.TryParse(valueString, out float result))
                        {
                            duration = result;
                        }
                    }
                }
            }

            if (!size.HasValue)
            {
                throw new InvalidOperationException("No media with or height found");
            }

            return (size.Value, duration);
        }

        private IReadOnlyList<KeyValuePair<string, string>> GetFormatTags(JsonDocument probeResultDocument)
        {
            var tags = new List<KeyValuePair<string, string>>();

            if (probeResultDocument.RootElement.TryGetProperty("format", out JsonElement format))
            {
                if (format.TryGetProperty("tags", out var tagsElement))
                {
                    foreach (var item in tagsElement.EnumerateObject())
                    {
                        var valueString = item.Value.GetString();
                        if (valueString != null)
                        {
                            tags.Add(KeyValuePair.Create(item.Name, valueString.Trim()));
                        }
                    }
                }
            }

            return tags;
        }

        private static float[] ParseLatLongValue(string value)
        {
            return value.Split(',').Select(x => x.Trim()).Select(x =>
            {
                var parts = x.Split(':').Select(float.Parse).ToArray();
                return parts[0] / parts[1];
            }).ToArray();
        }

        private (float Latitude, float Longitude)? GetLocation(IEnumerable<KeyValuePair<string, string>> tags)
        {
            var coordinate = new Coordinate();

            var location = GetBestStringValue(tags, "location", "com.apple.quicktime.location.ISO6709");
            if (!string.IsNullOrWhiteSpace(location))
            {
                coordinate.ParseIsoString(location);
                return (coordinate.Latitude, coordinate.Longitude);
            }


            var latitude = GetBestStringValue(tags, "GPSLatitude");
            var longitude = GetBestStringValue(tags, "GPSLongitude");
            if (string.IsNullOrWhiteSpace(latitude) || string.IsNullOrWhiteSpace(longitude))
            {
                return null;
            }

            var latitudeValue = ParseLatLongValue(latitude);
            var longitudeValue = ParseLatLongValue(longitude);

            var latitudeRef = GetBestStringValue(tags, "GPSLatitudeRef");
            var longitudeRef = GetBestStringValue(tags, "GPSLongitudeRef");

            coordinate.SetDMS(latitudeValue[0], latitudeValue[1], latitudeValue[2], latitudeRef == "N", longitudeValue[0], longitudeValue[1], longitudeValue[2], !(longitudeRef == "W"));

            return (coordinate.Latitude, coordinate.Longitude);
        }

        private static DateTimeOffset? GetCreationTime(IEnumerable<KeyValuePair<string, string>> tags)
        {
            var creationTime = GetBestStringValue(tags, "creation_time", "DateTime", "DateTimeOriginal", "DateTimeDigitized");
            if (creationTime != null)
            {
                if (DateTimeOffset.TryParse(creationTime, null, DateTimeStyles.RoundtripKind, out var result))
                {
                    return result;
                }

                if (DateTimeOffset.TryParseExact(creationTime, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out result))
                {
                    return result;
                }
            }

            var gpsTimestamp = GetBestStringValue(tags, "GPSTimeStamp");
            var gpsDatestamp = GetBestStringValue(tags, "GPSDateStamp");
            if (gpsTimestamp != null && gpsDatestamp != null)
            {
                var timestampParts = gpsDatestamp + ' ' + string.Join(":", ParseLatLongValue(gpsTimestamp));
                if (DateTimeOffset.TryParseExact(timestampParts, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }

            return null;
        }

        private static string? GetBestStringValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var allMatchingTags = tags.Where(x => names.Contains(x.Key));

            var commonmatchingTags = allMatchingTags.GroupBy(x => x.Value).OrderByDescending(x => x.Count());

            return commonmatchingTags.Select(x => x.Key).FirstOrDefault();
        }

        private static float? GetFloatValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var bestValue = GetBestStringValue(tags, names);
            if (bestValue == null)
            {
                return null;
            }

            var parts = bestValue.Split(':').Select(float.Parse).ToArray();
            return parts[0] / parts[1];
        }

        private static int? GetIntegerValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var bestValue = GetBestStringValue(tags, names);
            if (bestValue == null)
            {
                return null;
            }

            if (!int.TryParse(bestValue, out var result))
            {
                return null;
            }

            return result;
        }

        private static TEnum? GetEnumValue<TEnum>(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
            where TEnum : struct
        {
            var integerValue = GetIntegerValue(tags, names);
            if (integerValue == null)
            {
                return null;
            }

            return (TEnum)Enum.ToObject(typeof(TEnum), integerValue);
        }

        private static TimeSpan? GetTimeSpanValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var floatValue = GetFloatValue(tags, names);
            if (floatValue == null)
            {
                return null;
            }

            return TimeSpan.FromSeconds(floatValue.Value);
        }

        public async Task<Entities.MediaInfo> ExtractInformation(FileInfo fileInfo, CancellationToken token)
        {
            var sw = Stopwatch.StartNew();
            string probeResult = await Probe.New().Start($"-print_format json -show_frames -show_streams -show_format -show_packets \"{fileInfo}\"", token);
            
            _logger.LogInformation("Finished probing information for {File} in {TimeSeconds}s", fileInfo, sw.Elapsed.TotalSeconds);

            var probeResultDocument = JsonDocument.Parse(probeResult);

            var packetTags = GetTags(probeResultDocument, "packets_and_frames");
            var streamTags = GetTags(probeResultDocument, "streams");
            var videoStreamInfo = GetVideoStreamInfo(probeResultDocument);
            var formatTags = GetFormatTags(probeResultDocument);

            var tags = packetTags.Concat(formatTags.Concat(streamTags)).ToArray();

            var orientation = GetEnumValue<MediaOrientation>(tags, "Orientation");
            var flash = GetEnumValue<MediaFlash>(tags, "Flash");
            var saturation = GetEnumValue<MediaSaturation>(tags, "Saturation");
            var exposureProgram = GetEnumValue<MediaExposureProgram>(tags, "ExposureProgram");
            var whiteBalance = GetEnumValue<MediaExposureProgram>(tags, "WhiteBalance");
            var meteringMode = GetEnumValue<MediaExposureProgram>(tags, "MeteringMode");
            var contrast = GetEnumValue<MediaExposureProgram>(tags, "Contrast");

            var fstop = GetFloatValue(tags, "FNumber");
            var exposureTime = GetTimeSpanValue(tags, "ExposureTime");
            var isoSpeed = GetIntegerValue(tags, "ISOSpeedRatings");
            var exposureBias = GetFloatValue(tags, "ExposureBiasValue");
            var focalLength = GetFloatValue(tags, "FocalLength");
            var focalLengthIn35mmFilm = GetIntegerValue(tags, "FocalLengthIn35mmFilm");
            var apertureValue = GetFloatValue(tags, "ApertureValue");
            var brightnessValue = GetFloatValue(tags, "BrightnessValue");
            var shutterSpeedValue = GetTimeSpanValue(tags, "ShutterSpeedValue");
            var exposureIndex = GetFloatValue(tags, "ExposureIndex");
            var digitalZoomRatio = GetFloatValue(tags, "DigitalZoomRatio");
            var gpsAltitude = GetFloatValue(tags, "GPSAltitude");

            var make = GetBestStringValue(tags, "Make", "com.apple.quicktime.make");
            var model = GetBestStringValue(tags, "Model", "com.apple.quicktime.model");
            var software = GetBestStringValue(tags, "Software", "com.apple.quicktime.software");

            var creationTime = GetCreationTime(tags);
            var location = GetLocation(tags);

            return new Entities.MediaInfo
            {
                Duration = videoStreamInfo.Duration,
                CreationTime = creationTime,
                Size = videoStreamInfo.Size,
                Orientation = orientation,
                Flash = flash,
                Saturation = saturation,
                ExposureProgram = exposureProgram,
                WhiteBalance = whiteBalance,
                MeteringMode = meteringMode,
                Contrast = contrast,
                CameraMake = make,
                CameraModel = model,
                CameraSoftware = software,
                Location = location,
                LocationAltitude = gpsAltitude,
                DigitalZoomRatio = digitalZoomRatio,
                ExposureIndex = exposureIndex,
                ShutterSpeedValue = shutterSpeedValue,
                BrightnessValue = brightnessValue,
                ApertureValue = apertureValue,
                FocalLength = focalLength,
                FocalLengthIn35mmFilm = focalLengthIn35mmFilm,
                ExposureBias = exposureBias,
                IsoSpeed = isoSpeed,
                ExposureTime = exposureTime,
                FStop = fstop
            };
        }

        public async Task ExtractSnapshot(FileInfo input, FileInfo output, CancellationToken token)
        {
            var conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(input.FullName, output.FullName, TimeSpan.Zero);
            await conversion.Start(token);
        }
    }
}
