namespace EndpointManager.Models
{
    // Object used to communicate between View and Controller without exposing sensitive information
    // DTOs can be used when information displayed to user differs from internal representation of the object
    public class EndpointDTO
    {
        public required string SerialNumber { get; init; }
        public required string ModelId { get; init; }
        public required int MeterNumber { get; init; }
        public required string MeterFirmwareVersion { get; init; }
        public required string SwitchState { get; init; }
    }
}