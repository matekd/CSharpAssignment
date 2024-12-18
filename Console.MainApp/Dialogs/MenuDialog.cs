namespace ConsoleApp.MainApp.Dialogs;

public abstract class MenuDialog
{
    protected virtual void QuitOption()
    {
        Console.Clear();
    }

    protected void InvalidOption()
    {
        Console.Clear();
        Console.WriteLine("You must write a valid input.");
        Console.ReadKey();
    }

    protected void OutputDialog(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
