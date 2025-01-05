using CognizantDataverse.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

public interface IAccountService
{
    Guid CreateAccount(string name, string phone, string city);
    Entity ReadAccount(Guid accountId);
    void UpdateAccount(Guid accountId, string newName);
    void DeleteAccount(Guid accountId);
    Account[] GetAllAccounts();
}

namespace CognizantDataverse.Services

{
    // AccountService class to handle api calls to CRM
    public class AccountService : IAccountService
    {
        private readonly IOrganizationService _service;

        public AccountService(IOrganizationService service)
        {
            _service = service;
        }
        
        
        public Guid CreateAccount(string name, string phone, string city)
        {
            Entity account = new Entity("account");
            account["name"] = name;
            account["telephone1"] = phone;
            account["address1_city"] = city;

            Guid accountId = _service.Create(account);
            return accountId;
        }

        public Entity ReadAccount(Guid accountId)
        {
            ColumnSet columns = new ColumnSet(true);
            Entity account = _service.Retrieve("account", accountId, columns);
            return account;
        }

        public void UpdateAccount(Guid accountId, string newName)
        {
            Entity account = new Entity("account");
            account.Id = accountId;
            account["name"] = newName;

            _service.Update(account);
        }

        public void DeleteAccount(Guid accountId)
        {
            _service.Delete("account", accountId);
        }
        
        public Account[] GetAllAccounts()
        {
            QueryExpression query = new QueryExpression("account")
            {
                ColumnSet = new ColumnSet(true) // Retrieve all columns
            };

            EntityCollection results = _service.RetrieveMultiple(query);
            return results.Entities.Select(Account.FromEntity).ToArray();
        }
    }
}