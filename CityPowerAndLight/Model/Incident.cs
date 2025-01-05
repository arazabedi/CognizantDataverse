using Microsoft.Xrm.Sdk;

namespace CognizantDataverse.Models
{
    public class Incident
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }

        public static Incident FromEntity(Entity entity)
        {
            return new Incident
            {
                Id = entity.Id,
                Title = entity.Contains("title") ? entity["title"].ToString() : "N/A",
                Description = entity.Contains("description") ? entity["description"].ToString() : "N/A",
                CustomerId = entity.Contains("customerid") ? entity["customerid"].ToString() : "N/A"
            };
        }
    }
}