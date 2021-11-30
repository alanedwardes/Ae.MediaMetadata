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

            var packetsElement = probeResultDocument.RootElement.GetProperty(element);
            foreach (var packet in packetsElement.EnumerateArray())
            {
                if (packet.TryGetProperty("tags", out JsonElement packetTagsElement))
                {
                    foreach (var item in packetTagsElement.EnumerateObject())
                    {
                        tags.Add(KeyValuePair.Create(item.Name, item.Value.GetString().Trim()));
                    }
                }
            }

            return tags;
        }

        private ((int Width, int Height) Size, float? Duration) GetVideoStreamInfo(JsonDocument probeResultDocument)
        {
            var streamsElement = probeResultDocument.RootElement.GetProperty("streams");

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
                        duration = float.Parse(durationElement.GetString());
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
            var format = probeResultDocument.RootElement.GetProperty("format");

            var tags = new List<KeyValuePair<string, string>>();

            if (format.TryGetProperty("tags", out var tagsElement))
            {
                foreach (var item in tagsElement.EnumerateObject())
                {
                    tags.Add(KeyValuePair.Create(item.Name, item.Value.GetString().Trim()));
                }
            }

            return tags;
        }

        private (string Make, string Model, string Software) GetCamera(IEnumerable<KeyValuePair<string, string>> tags)
        {
            var make = tags.Where(x => x.Key == "Make" || x.Value == "com.apple.quicktime.make").Select(x => x.Value).FirstOrDefault();
            var model = tags.Where(x => x.Key == "Model" || x.Value == "com.apple.quicktime.model").Select(x => x.Value).FirstOrDefault();
            var software = tags.Where(x => x.Key == "Software" || x.Value == "com.apple.quicktime.software").Select(x => x.Value).FirstOrDefault();
            return (make, model, software);
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
            var latitude = tags.Where(x => x.Key == "GPSLatitude").Select(x => x.Value).FirstOrDefault();
            var latitudeRef = tags.Where(x => x.Key == "GPSLatitudeRef").Select(x => x.Value).FirstOrDefault();
            var longitude = tags.Where(x => x.Key == "GPSLongitude").Select(x => x.Value).FirstOrDefault();
            var longitudeRef = tags.Where(x => x.Key == "GPSLongitudeRef").Select(x => x.Value).FirstOrDefault();
            var iso6709 = tags.Where(x => x.Key == "com.apple.quicktime.location.ISO6709").Select(x => x.Value).FirstOrDefault();
            var location = tags.Where(x => x.Key == "location").Select(x => x.Value).FirstOrDefault();

            var coordinate = new Coordinate();

            if (!string.IsNullOrWhiteSpace(location))
            {
                coordinate.ParseIsoString(location);
                return (coordinate.Latitude, coordinate.Longitude);
            }

            if (!string.IsNullOrWhiteSpace(iso6709))
            {
                coordinate.ParseIsoString(iso6709);
                return (coordinate.Latitude, coordinate.Longitude);
            }

            if (string.IsNullOrWhiteSpace(latitude) || string.IsNullOrWhiteSpace(longitude))
            {
                return null;
            }

            var latitudeValue = ParseLatLongValue(latitude);
            var longitudeValue = ParseLatLongValue(longitude);

            coordinate.SetDMS(latitudeValue[0], latitudeValue[1], latitudeValue[2], latitudeRef == "N", longitudeValue[0], longitudeValue[1], longitudeValue[2], !(longitudeRef == "W"));

            return (coordinate.Latitude, coordinate.Longitude);
        }

        private static DateTimeOffset? GetCreationTime(IEnumerable<KeyValuePair<string, string>> tags)
        {
            var creationTime = GetBestTagValue(tags, "creation_time", "DateTime", "DateTimeOriginal", "DateTimeDigitized");
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

            var gpsTimestamp = GetBestTagValue(tags, "GPSTimeStamp");
            var gpsDatestamp = GetBestTagValue(tags, "GPSDateStamp");
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

        private static string? GetBestTagValue(IEnumerable<KeyValuePair<string, string>> tags, params string[] names)
        {
            var allMatchingTags = tags.Where(x => names.Contains(x.Key));

            var commonmatchingTags = allMatchingTags.GroupBy(x => x.Value).OrderByDescending(x => x.Count());

            return commonmatchingTags.Select(x => x.Key).FirstOrDefault();
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

            var orientation = MediaOrientation.Unknown;
            if (int.TryParse(tags.Where(x => x.Key == "Orientation").Select(x => x.Value).FirstOrDefault() ?? "0", out int orientationNumber))
            {
                orientation = (MediaOrientation)orientationNumber;
            }

            var creationTime = GetCreationTime(tags);
            var make = GetCamera(tags);
            var location = GetLocation(tags);

            return new Entities.MediaInfo
            {
                Duration = videoStreamInfo.Duration,
                CreationTime = creationTime,
                Size = videoStreamInfo.Size,
                Orientation = orientation,
                Camera = make,
                Location = location
            };
        }

        public async Task ExtractSnapshot(FileInfo input, FileInfo output, CancellationToken token)
        {
            var conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(input.FullName, output.FullName, TimeSpan.Zero);
            await conversion.Start(token);
        }
    }
}
