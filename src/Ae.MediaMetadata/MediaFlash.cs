using System;

namespace Ae.Galeriya.Core.Entities
{
    /// <summary>
    /// Indicates the status of flash when the image was shot.
    /// </summary>
    [Flags]
    public enum MediaFlash : byte
    {
        FlashDidNotFire = 0,
        StrobeReturnLightNotDetected = 4,
        StrobeReturnLightDetected = 2,
        FlashFired = 1,
        CompulsoryFlashMode = 8,
        AutoMode = 16,
        NoFlashFunction = 32,
        RedEyeReductionMode = 64
    }
}
