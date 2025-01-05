using System;
using CognizantDataverse.Controllers;

namespace CognizantDataverse
{
    // Controller for the main menu before the user enters entity-specific operations
    public class MainController
    {
        private readonly AccountController _accountController;
        private readonly ContactController _contactController;
        private readonly IncidentController _incidentController;
        private readonly ConsoleUI _ui;

        public MainController(AccountController accountController, ContactController contactController, IncidentController incidentController, ConsoleUI ui)
        {
            _accountController = accountController;
            _contactController = contactController;
            _incidentController = incidentController;
            _ui = ui;
        }

        public void Start()
        {
            while (true)
            {
                // Display the main menu
                _ui.DisplayMainMenu();

                // Prompt the user for input
                string choice = _ui.PromptForInput("Please select an option:");

                // Handle the user's choice
                switch (choice)
                {
                    case "1":
                        _accountController.Start();
                        break;
                    case "2":
                        _contactController.Start();
                        break;
                    case "3":
                        _incidentController.Start();
                        break;
                    case "0":
                        _ui.DisplayMessage("Exiting application.");
                        return;
                    default:
                        _ui.DisplayMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}