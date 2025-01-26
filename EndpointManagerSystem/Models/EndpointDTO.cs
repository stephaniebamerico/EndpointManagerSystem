namespace EndpointManager.Models
{
    public class EndpointDTO
    {
        public required string SerialNumber { get; init; }
        public required string ModelId { get; init; }
        public required int MeterNumber { get; init; }
        public required string MeterFirmwareVersion { get; init; }
        public required string SwitchState { get; init; }
    }
}