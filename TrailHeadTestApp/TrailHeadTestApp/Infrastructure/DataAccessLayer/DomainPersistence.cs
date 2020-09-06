using SQLite;
using System;

namespace TrailHeadTestApp.Infrastructure.DataAccessLayer
{
    public class DomainPersistence
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public string Data { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
