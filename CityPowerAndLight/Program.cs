using Microsoft.PowerPlatform.Dataverse.Client;
using DotNetEnv;
using CognizantDataverse.Controllers;
using CognizantDataverse.Services;

namespace CognizantDataverse
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Load environment variables
                Env.Load("/Users/arazabedi/csharp/CognizantDataverse/CityPowerAndLight/.env");
                
                // Build the connection string
                string connectionString = BuildConnectionString();

                // Connect to Dataverse
                using (ServiceClient service = new ServiceClient(connectionString))
                {
                    // Initialize services
                    AccountService accountService = new AccountService(service);
                    ContactService contactService = new ContactService(service);
                    IncidentService incidentService = new IncidentService(service);

                    // Initialize UI
                    ConsoleUI ui = new ConsoleUI();

                    // Initialize controllers
                    AccountController accountController = new AccountController(accountService, ui);
                    ContactController contactController = new ContactController(contactService, ui);
                    IncidentController incidentController = new IncidentController(incidentService, ui);

                    // Initialize and start the main controller
                    MainController mainController = new MainController(accountController, contactController, incidentController, ui);
                    mainController.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Build the connection string
        private static string BuildConnectionString()
        {
            return $@"AuthType=ClientSecret;
                      SkipDiscovery=true;
                      url={Environment.GetEnvironmentVariable("INSTANCE_URI")};
                      Secret={Environment.GetEnvironmentVariable("SECRET_ID")};
                      ClientId={Environment.GetEnvironmentVariable("APP_ID")};
                      RequireNewInstance=true";
        }
    }
}