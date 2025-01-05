using CognizantDataverse.Services;
using Microsoft.Xrm.Sdk;
using System;
using CognizantDataverse.Models;

namespace CognizantDataverse.Controllers
{
    // IncidentController class to handle user interactions
    public class IncidentController
    {
        // Uses the AccountService and ConsoleUI to handle API calls and console output respectively
        private readonly IncidentService _incidentService;
        private readonly ConsoleUI _ui;

        public IncidentController(IncidentService incidentService, ConsoleUI ui)
        {
            _incidentService = incidentService;
            _ui = ui;
        }

        // Displays options to the user
        public void Start()
        {
            while (true)
            {
                _ui.DisplayIncidentMenu();
                string choice = _ui.PromptForInput("Please select an option:");

                switch (choice)
                {
                    case "1":
                        HandleCreateIncident();
                        break;
                    case "2":
                        HandleReadIncident();
                        break;
                    case "3":
                        HandleUpdateIncident();
                        break;
                    case "4":
                        HandleDeleteIncident();
                        break;
                    case "5": // New option
                        HandleViewAllIncidents();
                        break;
                    case "0":
                        return;
                    default:
                        _ui.DisplayMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void HandleCreateIncident()
        {
            string title = _ui.PromptForInput("Enter Incident Title:");
            string description = _ui.PromptForInput("Enter Incident Description:");
            string customerIdInput = _ui.PromptForInput("Enter Customer ID:");

            if (Guid.TryParse(customerIdInput, out Guid customerId))
            {
                try
                {
                    Guid incidentId = _incidentService.CreateIncident(title, description, customerId);
                    _ui.DisplayMessage($"Incident created with ID: {incidentId}");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Customer ID.");
            }
        }

        private void HandleReadIncident()
        {
            string input = _ui.PromptForInput("Enter Incident ID:");
            if (Guid.TryParse(input, out Guid incidentId))
            {
                try
                {
                    Entity incident = _incidentService.ReadIncident(incidentId);
                    _ui.DisplayMessage($"Incident Title: {incident["title"]}");
                    _ui.DisplayMessage($"Description: {incident["description"]}");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Incident ID.");
            }
        }

        private void HandleUpdateIncident()
        {
            string input = _ui.PromptForInput("Enter Incident ID:");
            if (Guid.TryParse(input, out Guid incidentId))
            {
                string newTitle = _ui.PromptForInput("Enter New Incident Title:");

                try
                {
                    _incidentService.UpdateIncident(incidentId, newTitle);
                    _ui.DisplayMessage("Incident updated successfully.");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Incident ID.");
            }
        }

        private void HandleDeleteIncident()
        {
            string input = _ui.PromptForInput("Enter Incident ID:");
            if (Guid.TryParse(input, out Guid incidentId))
            {
                try
                {
                    _incidentService.DeleteIncident(incidentId);
                    _ui.DisplayMessage("Incident deleted successfully.");
                }
                catch (Exception ex)
                {
                    _ui.DisplayError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                _ui.DisplayError("Invalid Incident ID.");
            }
        }
        
        private void HandleViewAllIncidents()
        {
            try
            {
                Incident[] incidents = _incidentService.GetAllIncidents();
                if (incidents.Length == 0)
                {
                    _ui.DisplayMessage("No incidents found.");
                    return;
                }

                foreach (var incident in incidents)
                {
                    _ui.DisplayMessage($"Incident ID: {incident.Id}");
                    _ui.DisplayMessage($"Title: {incident.Title}");
                    _ui.DisplayMessage($"Description: {incident.Description}");
                    _ui.DisplayMessage($"Customer ID: {incident.CustomerId}");
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