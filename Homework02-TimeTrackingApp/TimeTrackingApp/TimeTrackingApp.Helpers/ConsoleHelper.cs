namespace TimeTrackingApp.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteLineInColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
