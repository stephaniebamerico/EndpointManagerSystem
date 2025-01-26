using EndpointManager.Models;

namespace EndpointManager.Interfaces
{
    public interface IEndpointView
    {
        EndpointDTO RequestEndpoint();

        string RequestSerialNumber();

        string RequestSwitchState();

        bool RequestConfirmation();

        void DisplayEndpoints(EndpointDTO[] endpoints);

        void DisplayEndpoint(EndpointDTO endpointDTO);
    }
}