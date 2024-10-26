using OTPSimulation.Interfaces;

namespace OTPSimulation.Common
{
    public class ConsoleUserInteraction : IConsoleUserInteraction
    {
        public string ReadUserInput() 
        {
            return Console.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
