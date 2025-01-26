using EndpointManager.Interfaces;
using EndpointManager.Models;

namespace EndpointManager.Repositories
{
    public class EndpointRepository : IEndpointRepository
    {
        private readonly List<Endpoint> _endpoints;

        public EndpointRepository()
        {
            _endpoints = new List<Endpoint>();
        }

        public void Insert(Endpoint endpoint)
        {
            if(_endpoints.Any(e => e.SerialNumber == endpoint.SerialNumber))
                throw new Exception("Endpoint with this serial number already exists.");
            
            _endpoints.Add(endpoint);
        }

        public void Edit(string serialNumber, int newSwitchState)
        {
            var endpoint = Find(serialNumber);
            
            if(endpoint == null)
                throw new Exception("Endpoint not found.");

            endpoint.SwitchState = newSwitchState;
        }

        public void Delete(string serialNumber)
        {
            var endpoint = Find(serialNumber);
            
            if(endpoint == null)
                throw new Exception("Endpoint not found.");
            
            _endpoints.Remove(endpoint);
        }

        public List<Endpoint> ListAll()
        {
            var endpoints = from e in _endpoints
                            select e;

            return endpoints.ToList<Endpoint>();
        }
        
        public Endpoint? Find(string serialNumber)
        {
            return _endpoints.FirstOrDefault(e => e.SerialNumber == serialNumber);
        }
    }
}