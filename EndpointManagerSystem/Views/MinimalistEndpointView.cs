using EndpointManager.Interfaces;
using EndpointManager.Models;

namespace EndpointManager.Views
{
    public class MinimalistEndpointView : BaseView, IEndpointView
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
            DisplayMessage("SN: ");
            return ReadLine();
        }

        private string RequestMeterModel()
        {
            DisplayMessage($"Model ({string.Join(", ", MeterModel.NameOptions())}): ");
            return ReadLine();
        }

        private int RequestMeterNumber()
        {
            DisplayMessage("Number: ");
            return ReadInt();
        }

        private string RequestFirmwareVersion()
        {
            DisplayMessage("FW: ");
            return ReadLine();
        }

        public string RequestSwitchState()
        {
            DisplayMessage($"Switch State ({string.Join(", ", SwitchState.NameOptions())}): ");
            return ReadLine();
        }

        public bool RequestConfirmation()
        {
            DisplayMessage("Are you sure you want to proceed? (N)o/(y)es ");
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
            Console.WriteLine($"SN: {endpointDTO.SerialNumber}");
            Console.WriteLine($"Model: {endpointDTO.ModelId}");
            Console.WriteLine($"Number: {endpointDTO.MeterNumber}");
            Console.WriteLine($"FW: {endpointDTO.MeterFirmwareVersion}");
            Console.WriteLine($"Switch State: {endpointDTO.SwitchState}");
        }
    }
}