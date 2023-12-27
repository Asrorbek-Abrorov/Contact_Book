
using Contact_Book.Displays;
using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var main = new MainMenu();
        main.Run();
    }
}