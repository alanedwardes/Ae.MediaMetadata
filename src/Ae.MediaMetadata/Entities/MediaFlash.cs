using System;

namespace Ae.MediaMetadata.Entities
{
    /// <summary>
    /// Indicates the status of flash when the image was shot.
    /// </summary>
    [Flags]
    public enum MediaFlash : ushort
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
