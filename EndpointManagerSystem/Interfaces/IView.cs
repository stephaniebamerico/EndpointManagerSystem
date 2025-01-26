namespace EndpointManager.Interfaces
{
    public interface IView
    {
        void DisplayMessage(string message);

        void DisplayLine(string message);

        string ReadLine();

        int ReadInt();
    }
}