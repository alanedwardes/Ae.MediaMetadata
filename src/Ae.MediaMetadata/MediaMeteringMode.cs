namespace Ae.Galeriya.Core.Entities
{
    /// <summary>
    /// The metering mode.
    /// </summary>
    public enum MediaMeteringMode : byte
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Average
        /// </summary>
        Average = 1,
        /// <summary>
        /// Center Weighted Average
        /// </summary>
        CenterWeightedAverage = 2,
        /// <summary>
        /// Spot
        /// </summary>
        Spot = 3,
        /// <summary>
        /// Multi Spot
        /// </summary>
        MultiSpot = 4,
        /// <summary>
        /// Pattern
        /// </summary>
        Pattern = 5,
        /// <summary>
        /// Partial
        /// </summary>
        Partial = 6,
        /// <summary>
        /// Other
        /// </summary>
        Other = 255
    }
}
