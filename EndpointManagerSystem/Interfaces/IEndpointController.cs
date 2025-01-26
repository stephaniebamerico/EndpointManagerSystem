namespace EndpointManager.Interfaces
{
    public interface IEndpointController
    {
        void InsertEndpoint();

        void EditEndpoint();

        string? DeleteEndpoint();

        void ListAllEndpoints();

        void FindEndpointBySerialNumber();
    }
}