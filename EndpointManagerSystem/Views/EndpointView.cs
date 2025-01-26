using EndpointManager.Interfaces;
using EndpointManager.Models;

namespace EndpointManager.Views
{
    public class EndpointView : BaseView, IEndpointView
    {
        public EndpointDTO RequestEndpoint()
        {
            var serialNumber = RequestSerialNumber();
            var meterModel = RequestMeterModel();
            var meterNumber = RequestMeterNumber();
            var meterFirmwareVersion = RequestFirmwareVersion();
            var switchState = RequestSwitchState();

            var endpointDTO = new EndpointDTO()
            {
                SerialNumber = serialNumber,
                ModelId = meterModel,
                MeterNumber = meterNumber,
                MeterFirmwareVersion = meterFirmwareVersion,
                SwitchState = switchState
            };

            return endpointDTO;
        }

        public string RequestSerialNumber()
        {
            DisplayMessage("Enter Endpoint Serial Number: ");
            return ReadLine();
        }

        private string RequestMeterModel()
        {
            DisplayMessage($"Enter Meter Model ({string.Join(", ", MeterModel.NameOptions())}): ");
            return ReadLine();
        }

        private int RequestMeterNumber()
        {
            DisplayMessage("Enter Meter Number: ");
            return ReadInt();
        }

        private string RequestFirmwareVersion()
        {
            DisplayMessage("Enter Firmware Version: ");
            return ReadLine();
        }

        public string RequestSwitchState()
        {
            DisplayMessage($"Enter Switch State ({string.Join(", ", SwitchState.NameOptions())}): ");
            return ReadLine();
        }

        public bool RequestConfirmation()
        {
            DisplayMessage("This action cannot be undone. Are you sure you want to proceed? (N)o/(y)es ");
            switch(ReadLine())
            {
                case "Y": case "y": case "Yes": case "YES": case "yes":
                    return true;

                case "N": case "n": case "No": case "NO": case "no":
                    return false;
                
                default:
                    DisplayMessage("Invalid input. ");
                    return false;
            }
        }

        public void DisplayEndpoints(List<EndpointDTO> endpoints)
        {
            if(endpoints == null || endpoints.Count == 0)
                DisplayLine("No endpoints registered.");
            else
            {
                DisplayLine("List of all Endpoints:");
                foreach (var endpoint in endpoints)
                    DisplayEndpoint(endpoint);
            }
        }

        public void DisplayEndpoint(EndpointDTO endpointDTO)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Serial Number: {endpointDTO.SerialNumber}");
            Console.WriteLine($"Meter Model: {endpointDTO.ModelId}");
            Console.WriteLine($"Meter Number: {endpointDTO.MeterNumber}");
            Console.WriteLine($"Firmware Version: {endpointDTO.MeterFirmwareVersion}");
            Console.WriteLine($"Switch State: {endpointDTO.SwitchState}");
        }
    }
}