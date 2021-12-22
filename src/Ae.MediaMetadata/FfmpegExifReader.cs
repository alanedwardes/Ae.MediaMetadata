using Ae.MediaMetadata.Entities;
using Ae.MediaMetadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace Ae.MediaMetadata
{

    public sealed class FfmpegExifReader : IExifReader
    {
        private readonly ILogger _logger;

        public FfmpegExifReader(ILogger logger)
        {
            _logger = logger;
        }

        public FfmpegExifReader()
            : this(NullLogger.Instance)
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

        private (MediaSize Size, float? Duration) GetVideoStreamInfo(JsonDocument probeResultDocument)
        {
            if (!probeResultDocument.RootElement.TryGetProperty("streams", out JsonElement streamsElement))
            {
                return (new MediaSize(), null);
            }

            MediaSize? size = null;
            float? duration = null;
            foreach (var stream in streamsElement.EnumerateArray())
            {
                var codecType = stream.GetProperty("codec_type").GetString();
                if (codecType == "video")
                {
                    size = new(stream.GetProperty("width").GetInt32(), stream.GetProperty("height").GetInt32());

                    if (stream.TryGetProperty("duration", out var durationElement))
                    {
                        var valueString = durationElement.GetString();
                        if (valueString != null && float.TryParse(valueString, out float result))
                        {
                            if (stream.TryGetProperty("codec_name", out var codecName) && codecName.GetString() == "mjpeg" && result == 0.04f)
                            {
                                // This is a JPEG image
                                continue;
                            }

                            duration = result;
                        }
                    }
                }
            }

            if (size == null)
            {
                throw new InvalidOperationException("No media with or height found");
            }

            return (size, duration);
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

        private MediaLocation? GetLocation(IEnumerable<KeyValuePair<string, string>> tags)
        {
            var altitude = GetFloatValue(tags, "GPSAltitude");

            var coordinate = new Coordinate();

            var location = GetBestStringValue(tags, "location", "com.apple.quicktime.location.ISO6709");
            if (!string.IsNullOrWhiteSpace(location))
            {
                coordinate.ParseIsoString(location);
                return new(coordinate.Latitude, coordinate.Longitude)
                {
                    Altitude = altitude
                };
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

            coordinate.SetDMS(latitudeValue[0], latitudeValue[1], latitudeValue[2], latitudeRef, longitudeValue[0], longitudeValue[1], longitudeValue[2], longitudeRef);

            return new(coordinate.Latitude, coordinate.Longitude)
            {
                Altitude = altitude
            };
        }

        private static DateTimeOffset? GetCreationTime(IEnumerable<KeyValuePair<string, string>> tags)
        {
            var creationTime = GetBestStringValue(tags, "creation_time", "DateTime", "DateTimeOriginal", "DateTimeDigitized");
            if (creationTime != null)
            {
                var result = creationTime.ParseDateTimeString();
                if (result.HasValue)
                {
                    return result;
                }
            }

            var gpsTimestamp = GetBestStringValue(tags, "GPSTimeStamp");
            var gpsDatestamp = GetBestStringValue(tags, "GPSDateStamp");
            if (gpsTimestamp != null && gpsDatestamp != null)
            {
                var result1 = MetadataStringExtensions.ParseGpsTime(gpsDatestamp, ParseLatLongValue(gpsTimestamp).Select(x => (double)x));
                if (result1.HasValue)
                {
                    return result1.Value;
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

        private static double GetFloatValue(string value)
        {
            var parts = value.Split(':').Select(double.Parse).ToArray();
            return parts[0] / parts[1];
        }

        private static double? GetFloatValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var bestValue = GetBestStringValue(tags, names);
            return bestValue == null ? null : GetFloatValue(bestValue);
        }

        private static double[] ParseLatLongValue(string value)
        {
            return value.Split(',').Select(x => x.Trim()).Select(GetFloatValue).ToArray();
        }

        private static uint? GetIntegerValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var bestValue = GetBestStringValue(tags, names);
            if (bestValue == null)
            {
                return null;
            }

            if (!uint.TryParse(bestValue, out var result))
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

        public async Task<Entities.MediaInfo> ReadMediaInfo(FileInfo fileInfo, CancellationToken token)
        {
            var sw = Stopwatch.StartNew();
            string probeResult = await Probe.New().Start($"-print_format json -show_frames -show_streams -show_format -show_packets \"{fileInfo}\"", token);

            _logger.LogInformation("Finished probing information for {File} in {TimeSeconds}s", fileInfo, sw.Elapsed.TotalSeconds);

            var probeResultDocument = JsonDocument.Parse(probeResult);

            if (!new HashSet<string> { "packets_and_frames", "streams", "format" }.SetEquals(probeResultDocument.RootElement.EnumerateObject().Select(x => x.Name)))
            {
                throw new InvalidOperationException("ffprobe did not return the required JSON keys, it appears to have not been able to load the file");
            }

            var (size, duration) = GetVideoStreamInfo(probeResultDocument);

            var tags = new[] { GetTags(probeResultDocument, "packets_and_frames"), GetFormatTags(probeResultDocument), GetTags(probeResultDocument, "streams") }.SelectMany(x => x).ToArray();

            return new Entities.MediaInfo
            {
                Duration = duration,
                CreationTime = GetCreationTime(tags),
                Size = size,
                Orientation = GetEnumValue<MediaOrientation>(tags, "Orientation"),
                Flash = GetEnumValue<MediaFlash>(tags, "Flash"),
                Saturation = GetEnumValue<MediaSaturation>(tags, "Saturation"),
                ExposureProgram = GetEnumValue<MediaExposureProgram>(tags, "ExposureProgram"),
                WhiteBalance = GetEnumValue<MediaWhiteBalance>(tags, "WhiteBalance"),
                MeteringMode = GetEnumValue<MediaMeteringMode>(tags, "MeteringMode"),
                Contrast = GetEnumValue<MediaContrast>(tags, "Contrast"),
                CameraMake = GetBestStringValue(tags, "Make", "com.apple.quicktime.make"),
                CameraModel = GetBestStringValue(tags, "Model", "com.apple.quicktime.model"),
                CameraSoftware = GetBestStringValue(tags, "Software", "com.apple.quicktime.software"),
                Location = GetLocation(tags),
                DigitalZoomRatio = GetFloatValue(tags, "DigitalZoomRatio"),
                ExposureIndex = GetFloatValue(tags, "ExposureIndex"),
                ShutterSpeedValue = GetFloatValue(tags, "ShutterSpeedValue"),
                BrightnessValue = GetFloatValue(tags, "BrightnessValue"),
                ApertureValue = GetFloatValue(tags, "ApertureValue"),
                FocalLength = GetFloatValue(tags, "FocalLength"),
                FocalLengthIn35mmFilm = (ushort?)GetIntegerValue(tags, "FocalLengthIn35mmFilm"),
                ExposureBias = GetFloatValue(tags, "ExposureBiasValue"),
                IsoSpeed = GetIntegerValue(tags, "ISOSpeedRatings"),
                ExposureTime = GetFloatValue(tags, "ExposureTime"),
                FStop = GetFloatValue(tags, "FNumber"),
                SubjectDistanceRange = GetEnumValue<MediaSubjectDistanceRange>(tags, "SubjectDistanceRange"),
                SceneCaptureType = GetEnumValue<MediaSceneCaptureType>(tags, "SceneCaptureType"),
                SensingMethod = GetEnumValue<MediaSensingMethod>(tags, "SensingMethod"),
                ImageUniqueId = GetBestStringValue(tags, "ImageUniqueID"),
            };
        }
    }
}
