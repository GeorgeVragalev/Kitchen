namespace Kitchen.Helpers;

public static class PrintConsole
{
    public static void Write(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
    } 
    
    public static void WriteSpace(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
    } 
}