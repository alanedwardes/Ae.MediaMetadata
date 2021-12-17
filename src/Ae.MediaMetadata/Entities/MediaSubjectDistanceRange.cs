namespace Ae.MediaMetadata.Entities
{
    public enum MediaSubjectDistanceRange : ushort
    {
        Unknown = 0,
        Macro = 1,
        Close = 2,
        Distant = 3
    }

    public enum MediaSensingMethod : ushort
    {
        NotDefined = 1,
        OneChipColorAreaSensor = 2,
        TwoChipColorAreaSensor = 3,
        ThreeChipColorAreaSensor = 4,
        ColorSequentialAreaSensor = 5,
        TrilinearSensor = 7,
        ColorSequentialLinearSensor = 8
    }
}
