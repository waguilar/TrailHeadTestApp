using Newtonsoft.Json;
using System.Collections.Generic;
using TrailHeadTestApp.Interfaces.Models;

namespace TrailHeadTestApp.Infrastructure.ApiModel
{
    public class GetEmpoyeeListResponse
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty(ItemConverterType = typeof(GenericConverter<Employee>), PropertyName = "data")]
        public List<IEmployee> data { get; set; }
    }

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
