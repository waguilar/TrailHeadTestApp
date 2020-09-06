using System;
using System.Threading.Tasks;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;

namespace TrailHeadTestApp.Infrastructure.DataAccessLayer
{
    public class DomainPersistenceDAL : BaseDAL<DomainPersistence>
    {
        public DomainPersistenceDAL(IDbHelper sqlite) : base(sqlite)
        { }

        public async Task SaveAsync(string entityId,
                                     string entityType,
                                     string entityData,
                                     bool setLastUpdateValue = true)
        {
            var domainPersistence = new DomainPersistence
            {
                Data = entityData,
                EntityId = entityId,
                EntityType = entityType
            };
            if (setLastUpdateValue)
            {
                domainPersistence.LastUpdate = DateTime.Now;
            }
            await SaveAsync(domainPersistence);
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
    }
}
