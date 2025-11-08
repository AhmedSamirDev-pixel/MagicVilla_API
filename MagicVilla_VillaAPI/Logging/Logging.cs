namespace MagicVilla_VillaAPI.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {message}");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Info: {message}");
            }
            
        }
    }
}
