public interface IConsoleUI
{
    string PromptForInput(string prompt);
    void DisplayMessage(string message);
    void DisplayError(string error);
    void DisplayAccountMenu();
}

public class ConsoleUI : IConsoleUI
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("\nMain Menu:");
        Console.WriteLine("1. Account Operations");
        Console.WriteLine("2. Contact Operations");
        Console.WriteLine("3. Incident Operations");
        Console.WriteLine("0. Exit");
    }

    public void DisplayAccountMenu()
    {
        Console.WriteLine("\nAccount Operations:");
        Console.WriteLine("1. Create Account");
        Console.WriteLine("2. Read Account");
        Console.WriteLine("3. Update Account");
        Console.WriteLine("4. Delete Account");
        Console.WriteLine("5. View All Accounts");
        Console.WriteLine("0. Back to Main Menu");
    }

    public void DisplayContactMenu()
    {
        Console.WriteLine("\nContact Operations:");
        Console.WriteLine("1. Create Contact");
        Console.WriteLine("2. Read Contact");
        Console.WriteLine("3. Update Contact");
        Console.WriteLine("4. Delete Contact");
        Console.WriteLine("5. View All Contacts");
        Console.WriteLine("0. Back to Main Menu");
    }

    public void DisplayIncidentMenu()
    {
        Console.WriteLine("\nIncident Operations:");
        Console.WriteLine("1. Create Incident");
        Console.WriteLine("2. Read Incident");
        Console.WriteLine("3. Update Incident");
        Console.WriteLine("4. Delete Incident");
        Console.WriteLine("5. View All Incidents");
        Console.WriteLine("0. Back to Main Menu");
    }
    
    // Used to get input from the user
    public string PromptForInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    // Used to display messages (shorthand for Console.WriteLine)
    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Used to display errors
    public void DisplayError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: " + errorMessage);
        Console.ResetColor();
    }
}