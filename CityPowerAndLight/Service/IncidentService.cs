using CognizantDataverse.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace CognizantDataverse.Services
{
    // IncidentService class to handle api calls to CRM
    public class IncidentService
    {
        private readonly IOrganizationService _service;

        public IncidentService(IOrganizationService service)
        {
            _service = service;
        }
        
        public Guid CreateIncident(string title, string description, Guid customerId)
        {
            Entity incident = new Entity("incident");
            incident["title"] = title;
            incident["description"] = description;
            incident["customerid"] = new EntityReference("contact", customerId);

            Guid incidentId = _service.Create(incident);
            return incidentId;
        }

        public Entity ReadIncident(Guid incidentId)
        {
            ColumnSet columns = new ColumnSet(true);
            Entity incident = _service.Retrieve("incident", incidentId, columns);
            return incident;
        }

        public void UpdateIncident(Guid incidentId, string newTitle)
        {
            Entity incident = new Entity("incident");
            incident.Id = incidentId;
            incident["title"] = newTitle;

            _service.Update(incident);
        }

        public void DeleteIncident(Guid incidentId)
        {
            _service.Delete("incident", incidentId);
        }
        
        public Incident[] GetAllIncidents()
        {
            QueryExpression query = new QueryExpression("incident")
            {
                ColumnSet = new ColumnSet(true) // Retrieve all columns
            };

            EntityCollection results = _service.RetrieveMultiple(query);
            return results.Entities.Select(Incident.FromEntity).ToArray();
        }
    }
}