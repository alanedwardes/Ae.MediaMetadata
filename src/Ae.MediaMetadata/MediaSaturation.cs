﻿namespace Ae.Galeriya.Core.Entities
{
    /// <summary>
    /// Indicates the direction of saturation processing applied by the camera when the image was shot.
    /// </summary>
    public enum MediaSaturation : byte
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Low saturation
        /// </summary>
        LowSaturation = 1,
        /// <summary>
        /// High saturation
        /// </summary>
        HighSaturation = 2
    }
}
