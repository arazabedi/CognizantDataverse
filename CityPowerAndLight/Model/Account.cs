using Microsoft.Xrm.Sdk;

namespace CognizantDataverse.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

        public static Account FromEntity(Entity entity)
        {
            return new Account
            {
                Id = entity.Id,
                Name = entity.Contains("name") ? entity["name"].ToString() : "N/A",
                Phone = entity.Contains("telephone1") ? entity["telephone1"].ToString() : "N/A",
                City = entity.Contains("address1_city") ? entity["address1_city"].ToString() : "N/A"
            };
        }
    }
}