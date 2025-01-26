namespace EndpointManager.Models
{
    public class Endpoint
    {
        public required string SerialNumber { get; init; }
        public required int ModelId { get; init; }
        public required int MeterNumber { get; init; }
        public required string MeterFirmwareVersion { get; init; }
        public required int SwitchState { get; set; }
    }
}