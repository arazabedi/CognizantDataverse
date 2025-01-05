using CognizantDataverse.Services;
using Microsoft.Xrm.Sdk;
using System;
using CognizantDataverse.Models;

namespace CognizantDataverse.Controllers
{
    
    // ContactController class to handle user interactions
    public class ContactController
    {
        // Uses the AccountService and ConsoleUI to handle API calls and console output respectively
        private readonly ContactService _contactService;
        private readonly ConsoleUI _ui;

        public ContactController(ContactService contactService, ConsoleUI ui)
        {
            _contactService = contactService;
            _ui = ui;
        }

        // Displays options to the user
        public void Start()
        {
            while (true)
            {
                _ui.DisplayContactMenu();
                string choice = _ui.PromptForInput("Please select an option:");

                switch (choice)
                {
                    case "1":
                        HandleCreateContact();
                        break;
                    case "2":
                        HandleReadContact();
                        break;
                    case "3":
                        HandleUpdateContact();
                        break;
                    case "4":
                        HandleDeleteContact();
                        break;
                    case "5": // New option
                        HandleViewAllContacts();
                        break;
                    case "0":
                        return;
                    default:
                        _ui.DisplayMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void HandleCreateContact()
        {
            string firstName = _ui.PromptForInput("Enter First Name:");
            string lastName = _ui.PromptForInput("Enter Last Name:");
            string email = _ui.PromptForInput("Enter Email:");
            string accountIdInput = _ui.PromptForInput("Enter Account ID:");

            if (Guid.TryParse(accountIdInput, out Guid accountId))
            {
                try
                {
                    Guid contactId = _contactService.CreateContact(firstName, lastName, email, accountId);
                    _ui.DisplayMessage($"Contact created with ID: {contactId}");
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

        private void HandleReadContact()
        {
            string input = _ui.PromptForInput("Enter Contact ID:");
            if (Guid.TryParse(input, out Guid contactId))
            {
                try
                {
                    Entity contact = _contactService.ReadContact(contactId);
                    _ui.DisplayMessage($"First Name: {contact["firstname"]}");
                    _ui.DisplayMessage($"Last Name: {contact["lastname"]}");
                    _ui.DisplayMessage($"Email: {contact["emailaddress1"]}");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Contact ID.");
            }
        }

        private void HandleUpdateContact()
        {
            string input = _ui.PromptForInput("Enter Contact ID:");
            if (Guid.TryParse(input, out Guid contactId))
            {
                string newEmail = _ui.PromptForInput("Enter New Email:");

                try
                {
                    _contactService.UpdateContact(contactId, newEmail);
                    _ui.DisplayMessage("Contact updated successfully.");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Contact ID.");
            }
        }

        private void HandleDeleteContact()
        {
            string input = _ui.PromptForInput("Enter Contact ID:");
            if (Guid.TryParse(input, out Guid contactId))
            {
                try
                {
                    _contactService.DeleteContact(contactId);
                    _ui.DisplayMessage("Contact deleted successfully.");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Contact ID.");
            }
        }
        
        private void HandleViewAllContacts()
        {
            try
            {
                Contact[] contacts = _contactService.GetAllContacts();
                if (contacts.Length == 0)
                {
                    _ui.DisplayMessage("No contacts found.");
                    return;
                }

                foreach (var contact in contacts)
                {
                    _ui.DisplayMessage($"Contact ID: {contact.Id}");
                    _ui.DisplayMessage($"First Name: {contact.FirstName}");
                    _ui.DisplayMessage($"Last Name: {contact.LastName}");
                    _ui.DisplayMessage($"Email: {contact.Email}");
                    _ui.DisplayMessage($"Phone: {contact.Phone}");
                    _ui.DisplayMessage("-----------------------------");
                }
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"An error occurred: {ex.Message}");
            }
        }
    }
}