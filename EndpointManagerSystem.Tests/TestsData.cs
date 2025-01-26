using EndpointManager.Models;

namespace EndpointManagerSystem.Tests
{
    // Define Data used in tests
    public static class TestsData
    {
        // Endpoint should match the DTOs defined and in the same order
        public static List<Endpoint> ReturnEndpoints()
        {
            return new List<Endpoint>()
            {
                new Endpoint()
                {
                    SerialNumber = "A1",
                    ModelId = 16,
                    MeterNumber = 5,
                    MeterFirmwareVersion = "v1.0",
                    SwitchState = 0
                },
                new Endpoint()
                {
                    SerialNumber = "XY",
                    ModelId = 18,
                    MeterNumber = 2,
                    MeterFirmwareVersion = "v1.5",
                    SwitchState = 2
                },
                new Endpoint()
                {
                    SerialNumber = "8C",
                    ModelId = 17,
                    MeterNumber = 18,
                    MeterFirmwareVersion = "v1.0",
                    SwitchState = 1
                },
                new Endpoint()
                {
                    SerialNumber = "3B",
                    ModelId = 19,
                    MeterNumber = 0,
                    MeterFirmwareVersion = "v1.8",
                    SwitchState = 0
                }
            };

        }
        
        // DTOs should match the Endpoints defined and in the same order
        public static List<EndpointDTO> ReturnDTOs()
        {
            return new List<EndpointDTO>()
            {
                new()
                {
                    SerialNumber = "A1",
                    ModelId = "NSX1P2W",
                    MeterNumber = 5,
                    MeterFirmwareVersion = "v1.0",
                    SwitchState = "Disconnected"
                },
                new()
                {
                    SerialNumber = "XY",
                    ModelId = "NSX2P3W",
                    MeterNumber = 2,
                    MeterFirmwareVersion = "v1.5",
                    SwitchState = "Armed"
                },
                new()
                {
                    SerialNumber = "8C",
                    ModelId = "NSX1P3W",
                    MeterNumber = 18,
                    MeterFirmwareVersion = "v1.0",
                    SwitchState = "Connected"
                },
                new()
                {
                    SerialNumber = "3B",
                    ModelId = "NSX3P4W",
                    MeterNumber = 0,
                    MeterFirmwareVersion = "v1.8",
                    SwitchState = "Disconnected"
                }
            };
        }
    
        // New Switch States should be different from the ones defined for the Endpoints and DTOs
        public static List<(string, int)> ReturnNewSwitchStates()
        {
            return new List<(string, int)>()
            {
                ("Connected", 1),
                ("Disconnected", 0),
                ("Armed", 2),
                ("Armed", 2)
            };

        }
    }
}