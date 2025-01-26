using EndpointManager.Interfaces;
using EndpointManager.Repositories;
using EndpointManager.Models;
using EndpointManager.Views;

namespace EndpointManager.Controllers
{
    public class EndpointController(IEndpointRepository repository, IEndpointView endpointView) : IEndpointController
    {
        public void InsertEndpoint()
        {
            var endpointDTO = endpointView.RequestEndpoint();

            if(repository.Find(endpointDTO.SerialNumber) != null)
                throw new Exception("Endpoint with this serial number already exists.");

            ValidateMeterModel(MeterModel.Value(endpointDTO.ModelId));
            ValidateSwitchState(SwitchState.Value(endpointDTO.SwitchState));

            var endpoint = new Endpoint()
            {
                SerialNumber = endpointDTO.SerialNumber,
                ModelId = MeterModel.Value(endpointDTO.ModelId),
                MeterNumber = endpointDTO.MeterNumber,
                MeterFirmwareVersion = endpointDTO.MeterFirmwareVersion,
                SwitchState = SwitchState.Value(endpointDTO.SwitchState)
            };

            repository.Insert(endpoint);
        }

        public void EditEndpoint()
        {
            var serialNumber = endpointView.RequestSerialNumber();
            var endpoint = repository.Find(serialNumber) ?? throw new Exception("Endpoint not found.");

            var switchState = SwitchState.Value(endpointView.RequestSwitchState());
            ValidateSwitchState(switchState);
            
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

        private static void ValidateMeterModel(int meterModel)
        {
            var validModelIds = MeterModel.ValueOptions();
            
            if(!validModelIds.Contains(meterModel))
                throw new Exception("Meter Model not valid.");
        }

        private static void ValidateSwitchState(int switchState)
        {
            var validSwitchStates = SwitchState.ValueOptions();
            
            if(!validSwitchStates.Contains(switchState))
                throw new Exception("Switch State not valid.");
        }
    }
}