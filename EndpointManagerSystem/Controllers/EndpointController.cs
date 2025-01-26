using EndpointManager.Interfaces;
using EndpointManager.Models;

namespace EndpointManager.Controllers
{
    public class EndpointController(IEndpointRepository repository, IEndpointView endpointView) : IEndpointController
    {
        public void InsertEndpoint()
        {
            var endpointDTO = endpointView.RequestEndpoint();

            if(repository.Find(endpointDTO.SerialNumber) != null)
                throw new Exception("Endpoint with this serial number already exists.");

            var meterModel = MeterModel.Value(endpointDTO.ModelId);
            var switchState = SwitchState.Value(endpointDTO.SwitchState);

            var endpoint = new Endpoint()
            {
                SerialNumber = endpointDTO.SerialNumber,
                ModelId = meterModel,
                MeterNumber = endpointDTO.MeterNumber,
                MeterFirmwareVersion = endpointDTO.MeterFirmwareVersion,
                SwitchState = switchState
            };

            repository.Insert(endpoint);
        }

        public void EditEndpoint()
        {
            var serialNumber = endpointView.RequestSerialNumber();
            var endpoint = repository.Find(serialNumber) ?? throw new Exception("Endpoint not found.");

            var switchState = SwitchState.Value(endpointView.RequestSwitchState());
            
            if(endpoint.SwitchState == switchState)
                throw new Exception("Same Switch State value already registered.");

            repository.Edit(serialNumber, switchState);
        }

        public string? DeleteEndpoint()
        {
            var serialNumber = endpointView.RequestSerialNumber();

            if(repository.Find(serialNumber) == null)
                throw new Exception("Endpoint not found.");
            
            if(endpointView.RequestConfirmation())
                repository.Delete(serialNumber);
            else
                return null;
            
            return serialNumber;
        }

        public void ListAllEndpoints()
        {
            var endpoints = repository.ListAll();

            var endpointsDTO = new List<EndpointDTO>();
            foreach (var endpoint in endpoints)
            {
                var endpointDTO = new EndpointDTO()
                {
                    SerialNumber = endpoint.SerialNumber,
                    ModelId = MeterModel.Name(endpoint.ModelId),
                    MeterNumber = endpoint.MeterNumber,
                    MeterFirmwareVersion = endpoint.MeterFirmwareVersion,
                    SwitchState = SwitchState.Name(endpoint.SwitchState)
                };

                endpointsDTO.Add(endpointDTO);
            }
            endpointView.DisplayEndpoints(endpointsDTO);
        }

        public void FindEndpointBySerialNumber()
        {
            var serialNumber = endpointView.RequestSerialNumber();
            var endpoint = repository.Find(serialNumber) ?? throw new Exception("Endpoint not found.");

            var endpointDTO = new EndpointDTO()
            {
                SerialNumber = endpoint.SerialNumber,
                ModelId = MeterModel.Name(endpoint.ModelId),
                MeterNumber = endpoint.MeterNumber,
                MeterFirmwareVersion = endpoint.MeterFirmwareVersion,
                SwitchState = SwitchState.Name(endpoint.SwitchState)
            };

            endpointView.DisplayEndpoint(endpointDTO);
        }
    }
}