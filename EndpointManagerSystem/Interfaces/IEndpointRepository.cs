using EndpointManager.Models;

namespace EndpointManager.Interfaces
{
    public interface IEndpointRepository
    {
        void Insert(Endpoint endpoint);

        void Edit(string serialNumber, int newSwitchState);

        void Delete(string serialNumber);

        List<Endpoint> ListAll();
        
        Endpoint? Find(string serialNumber);
    }
}