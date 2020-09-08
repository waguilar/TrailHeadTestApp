using Newtonsoft.Json;
using System.Collections.Generic;
using TrailHeadTestApp.Domain;
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

    
}
