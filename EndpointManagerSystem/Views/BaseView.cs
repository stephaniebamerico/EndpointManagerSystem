using EndpointManager.Interfaces;

namespace EndpointManager.Views
{
    // Base View, responsible for the most basic user interactions.
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
            var input = Console.ReadLine() ?? throw new Exception("Empty input.");
            if (input.Length == 0)
                throw new Exception("Empty input.");
            return input;
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