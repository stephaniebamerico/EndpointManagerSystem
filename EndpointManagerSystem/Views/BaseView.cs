using EndpointManager.Interfaces;

namespace EndpointManager.Views
{
    public class BaseView : IView
    {
        public void DisplayMessage(string message)
        {
            Console.Write(message);
        }

        public void DisplayLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine() ?? "";
        }

        public int ReadInt()
        {
            try
            {
                return int.Parse(ReadLine());
            }
            catch (Exception ex)
            {
                throw new Exception($"Not an integer. {ex.Message}");
            }
        }
    }
}