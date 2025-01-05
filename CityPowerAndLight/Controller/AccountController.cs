using CognizantDataverse.Models;
using CognizantDataverse.Services;
using Microsoft.Xrm.Sdk;

// AccountController class to handle user interactions
public class AccountController
{
    // Uses the AccountService and ConsoleUI to handle API calls and console output respectively
    private readonly IAccountService _accountService;
    private readonly IConsoleUI _ui;

    public AccountController(IAccountService accountService, IConsoleUI ui)
    {
        _accountService = accountService;
        _ui = ui;
    }

    // Displays options to the user
    public void Start()
    {
        while (true)
        {
            _ui.DisplayAccountMenu();
            string choice = _ui.PromptForInput("Please select an option:");

            switch (choice)
            {
                case "1":
                    HandleCreateAccount();
                    break;
                case "2":
                    HandleReadAccount();
                    break;
                case "3":
                    HandleUpdateAccount();
                    break;
                case "4":
                    HandleDeleteAccount();
                    break;
                case "5": // New option
                    HandleViewAllAccounts();
                    break;
                case "0":
                    return;
                default:
                    _ui.DisplayMessage("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    internal void HandleCreateAccount()
    {
        string name = _ui.PromptForInput("Enter Account Name:");
        string phone = _ui.PromptForInput("Enter Phone Number:");
        string city = _ui.PromptForInput("Enter City:");

        try
        {
            Guid accountId = _accountService.CreateAccount(name, phone, city);
            _ui.DisplayMessage($"Account created with ID: {accountId}");
        }
        catch (Exception ex)
        {
            _ui.DisplayError($"An error occurred: {ex.Message}");
        }
    }

    internal void HandleReadAccount()
    {
        string input = _ui.PromptForInput("Enter Account ID:"); // Prompt for account ID
        if (Guid.TryParse(input, out Guid accountId))
        {
            try
            {
                Entity account = _accountService.ReadAccount(accountId);
                _ui.DisplayMessage($"Account Name: {account["name"]}");
                _ui.DisplayMessage($"Telephone: {account["telephone1"]}");
                _ui.DisplayMessage($"City: {account["address1_city"]}");
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            _ui.DisplayError("Invalid Account ID.");
        }
    }

    public void HandleUpdateAccount()
    {
        string input = _ui.PromptForInput("Enter Account ID:");
        if (Guid.TryParse(input, out Guid accountId))
        {
            string newName = _ui.PromptForInput("Enter New Account Name:");

            try
            {
                _accountService.UpdateAccount(accountId, newName);
                _ui.DisplayMessage("Account updated successfully.");
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            _ui.DisplayError("Invalid Account ID.");
        }
    }

    public void HandleDeleteAccount()
    {
        string input = _ui.PromptForInput("Enter Account ID:");
        if (Guid.TryParse(input, out Guid accountId))
        {
            try
            {
                _accountService.DeleteAccount(accountId);
                _ui.DisplayMessage("Account deleted successfully.");
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            _ui.DisplayError("Invalid Account ID.");
        }
    }

    public void HandleViewAllAccounts()
    {
        try
        {
            Account[] accounts = _accountService.GetAllAccounts();
            if (accounts.Length == 0)
            {
                _ui.DisplayMessage("No accounts found.");
                return;
            }

            foreach (var account in accounts)
            {
                _ui.DisplayMessage($"Account ID: {account.Id}");
                _ui.DisplayMessage($"Name: {account.Name}");
                _ui.DisplayMessage($"Phone: {account.Phone}");
                _ui.DisplayMessage($"City: {account.City}");
                _ui.DisplayMessage("-----------------------------");
            }
        }
        catch (Exception ex)
        {
            _ui.DisplayError($"An error occurred: {ex.Message}");
        }
    }
}