using Microsoft.Xrm.Sdk;

namespace CognizantDataverse.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public static Contact FromEntity(Entity entity)
        {
            return new Contact
            {
                Id = entity.Id,
                FirstName = entity.Contains("firstname") ? entity["firstname"].ToString() : "N/A",
                LastName = entity.Contains("lastname") ? entity["lastname"].ToString() : "N/A",
                Email = entity.Contains("emailaddress1") ? entity["emailaddress1"].ToString() : "N/A",
                Phone = entity.Contains("telephone1") ? entity["telephone1"].ToString() : "N/A"
            };
        }
    }
}