using Newtonsoft.Json;
using TrailHeadTestApp.Interfaces.Models;

namespace TrailHeadTestApp.Domain
{
    public class Employee : IEmployee
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }
    }
}
