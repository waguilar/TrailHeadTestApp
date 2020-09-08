using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;

namespace TrailHeadTestApp.Infrastructure.DataAccessLayer
{
    public class DomainPersistenceDAL : BaseDAL<DomainPersistence>, IDomainPersistenceDAL
    {
        public DomainPersistenceDAL(IDbHelper sqlite) : base(sqlite)
        { }

        public async Task<bool> SaveAsync<T>(object entity, string id, bool setLastUpdateValue = true)
        {
            try
            {
                var serializedEntity = JsonConvert.SerializeObject(entity);
                var domainPersistence = new DomainPersistence
                {
                    Data = serializedEntity,
                    EntityId = id,
                    EntityType = typeof(T).ToString()
                };
                if (setLastUpdateValue)
                {
                    domainPersistence.LastUpdate = DateTime.Now;
                }
                await SaveAsync(domainPersistence);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task SaveAsync(DomainPersistence domainPersistence)
        {
            var previousDomainPersistence = await GetEntityAsync(domainPersistence.EntityId, domainPersistence.EntityType);
            if (previousDomainPersistence == null)
            {
                await SqlAsyncConnection.InsertAsync(domainPersistence);
            }
            else
            {
                previousDomainPersistence.Data = domainPersistence.Data;
                previousDomainPersistence.LastUpdate = domainPersistence.LastUpdate;
                await SqlAsyncConnection.UpdateAsync(previousDomainPersistence);
            }
        }

        private async Task<DomainPersistence> GetEntityAsync(string entityId,
                                                              string entityType)
        {
            var query = SqlAsyncConnection
                        .Table<DomainPersistence>()
                        .OrderByDescending(e => e.LastUpdate)
                        .Where(e => e.EntityId == entityId && e.EntityType == entityType);
            var domainPersistence = await query.FirstOrDefaultAsync();
            return domainPersistence;
        }

        public async Task<string> GetAsync(string entityId,
                                            string entityType)
        {

            return (await GetEntityAsync(entityId, entityType))?.Data;
        }

        public async Task<T> GetAsync<T>(string id)
        {
            var data = (await GetEntityAsync(id, typeof(T).ToString()))?.Data;
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
