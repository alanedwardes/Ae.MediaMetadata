﻿using Ae.MediaMetadata.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ae.MediaMetadata
{
    public sealed class ImageSharpExifReader : IExifReader
    {
        private readonly ILogger _logger;

        public ImageSharpExifReader(ILogger logger)
        {
            _logger = logger;
        }

        public ImageSharpExifReader()
            : this(NullLogger.Instance)
        {
        }

        public async Task<MediaInfo> ReadMediaInfo(FileInfo fileInfo, CancellationToken token)
        {
            using var reader = fileInfo.OpenRead();
            using var image = await Image.LoadAsync(reader);

            var exif = image.Metadata.ExifProfile?.Values ?? Array.Empty<IExifValue>();

            return new MediaInfo
            {
                Size = (image.Width, image.Height),
                CameraMake = GetValue<string?>(exif, ExifTag.Make)?.Trim(),
                CameraModel = GetValue<string?>(exif, ExifTag.Model)?.Trim(),
                CameraSoftware = GetValue<string?>(exif, ExifTag.Software)?.Trim(),
                ApertureValue = GetValue<Rational?>(exif, ExifTag.ApertureValue)?.ToDouble(),
                BrightnessValue = GetValue<SignedRational?>(exif, ExifTag.BrightnessValue)?.ToDouble(),
                DigitalZoomRatio = GetValue<Rational?>(exif, ExifTag.DigitalZoomRatio)?.ToDouble(),
                ExposureBias = GetValue<SignedRational?>(exif, ExifTag.ExposureBiasValue)?.ToDouble(),
                FocalLength = GetValue<Rational?>(exif, ExifTag.FocalLength)?.ToDouble(),
                LocationAltitude = GetValue<Rational?>(exif, ExifTag.GPSAltitude)?.ToDouble(),
                ExposureIndex = GetValue<Rational?>(exif, ExifTag.ExposureIndex)?.ToDouble(),
                FStop = GetValue<Rational?>(exif, ExifTag.FNumber)?.ToDouble(),
                IsoSpeed = GetValue<ushort[]>(exif, ExifTag.ISOSpeedRatings)?[0],
                FocalLengthIn35mmFilm = GetValue<ushort?>(exif, ExifTag.FocalLengthIn35mmFilm),
                Contrast = (MediaContrast?)GetValue<ushort?>(exif, ExifTag.Contrast),
                ExposureProgram = (MediaExposureProgram?)GetValue<ushort?>(exif, ExifTag.ExposureProgram),
                Flash = (MediaFlash?)GetValue<ushort?>(exif, ExifTag.Flash),
                MeteringMode = (MediaMeteringMode?)GetValue<ushort?>(exif, ExifTag.MeteringMode),
                Orientation = (MediaOrientation?)GetValue<ushort?>(exif, ExifTag.Orientation),
                Saturation = (MediaSaturation?)GetValue<ushort?>(exif, ExifTag.Saturation),
                WhiteBalance = (MediaWhiteBalance?)GetValue<ushort?>(exif, ExifTag.WhiteBalance),
                ShutterSpeedValue = GetTimeSpan(exif, ExifTag.ShutterSpeedValue),
                ExposureTime = GetTimeSpan(exif, ExifTag.ExposureTime),
                CreationTime = GetDateTime(exif),
                Location = GetLocation(exif)
            };
        }

        private (double, double)? GetLocation(IEnumerable<IExifValue> exif)
        {
            var coordinate = new Coordinate();

            var latitude = GetValue<Rational[]>(exif, ExifTag.GPSLatitude);
            var latitudeRef = GetValue<string>(exif, ExifTag.GPSLatitudeRef);
            var longitude = GetValue<Rational[]>(exif, ExifTag.GPSLongitude);
            var longitudeRef = GetValue<string>(exif, ExifTag.GPSLongitudeRef);
            if (latitude != null && longitude != null)
            {

                coordinate.SetDMS((float)latitude[0].ToDouble(), (float)latitude[1].ToDouble(), (float)latitude[2].ToDouble(), latitudeRef, (float)longitude[0].ToDouble(), (float)longitude[1].ToDouble(), (float)longitude[2].ToDouble(), longitudeRef);
                return (coordinate.Latitude, coordinate.Longitude);
            }

            return null;
        }

        private DateTimeOffset? GetDateTime(IEnumerable<IExifValue> exif)
        {
            var tag = exif.FirstOrDefault(x => x.Tag == ExifTag.DateTime || x.Tag == ExifTag.DateTimeOriginal);
            if (tag != null && DateTimeOffset.TryParseExact(tag.ToString(), "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out var result))
            {
                return result;
            }

            var tag1 = GetValue<string?>(exif, ExifTag.GPSDateStamp);
            var tag2 = GetValue<Rational[]?>(exif, ExifTag.GPSTimestamp);
            if (tag1 != null && tag2 != null)
            {
                var timestampParts = tag1 + ' ' + string.Join(":", tag2.Select(x => x.ToDouble()));
                if (DateTimeOffset.TryParseExact(timestampParts, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out var result1))
                {
                    return result1;
                }
            }

            return null;
        }

        private TimeSpan? GetTimeSpan(IEnumerable<IExifValue> exif, ExifTag name)
        {
            var rational = GetValue<SignedRational?>(exif, name)?.ToDouble() ?? GetValue<Rational?>(exif, name)?.ToDouble();
            return rational.HasValue ? TimeSpan.FromSeconds(rational.Value) : null;
        }

        private TValue? GetValue<TValue>(IEnumerable<IExifValue> exif, ExifTag name)
        {
            var tag = exif.FirstOrDefault(x => x.Tag == name);
            if (tag?.GetValue() is TValue value)
            {
                return value;
            }

            return default;
        }
    }
}
