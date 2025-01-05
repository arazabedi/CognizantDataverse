using CognizantDataverse.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace CognizantDataverse.Services
{
    // ContactService class to handle api calls to CRM
    public class ContactService
    {
        private readonly IOrganizationService _service;

        public ContactService(IOrganizationService service)
        {
            _service = service;
        }
        
        public Guid CreateContact(string firstName, string lastName, string email, Guid accountId)
        {
            Entity contact = new Entity("contact");
            contact["firstname"] = firstName;
            contact["lastname"] = lastName;
            contact["emailaddress1"] = email;
            contact["parentcustomerid"] = new EntityReference("account", accountId);

            Guid contactId = _service.Create(contact);
            return contactId;
        }

        public Entity ReadContact(Guid contactId)
        {
            ColumnSet columns = new ColumnSet(true);
            Entity contact = _service.Retrieve("contact", contactId, columns);
            return contact;
        }

        public void UpdateContact(Guid contactId, string newEmail)
        {
            Entity contact = new Entity("contact");
            contact.Id = contactId;
            contact["emailaddress1"] = newEmail;

            _service.Update(contact);
        }

        public void DeleteContact(Guid contactId)
        {
            _service.Delete("contact", contactId);
        }
        
        public Contact[] GetAllContacts()
        {
            QueryExpression query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet(true) // Retrieve all columns
            };

            EntityCollection results = _service.RetrieveMultiple(query);
            return results.Entities.Select(Contact.FromEntity).ToArray();
        }
    }
}