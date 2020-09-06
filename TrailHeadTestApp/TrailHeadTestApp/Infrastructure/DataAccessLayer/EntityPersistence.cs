using Autofac;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer;
using TrailHeadTestApp.Interfaces.Services;
using TrailHeadTestApp.Services;

namespace TrailHeadTestApp.Infrastructure.DataAccessLayer
{
    public class EntityPersistence : IEntityPersistence
    {
        private DomainPersistenceDAL _domainPersistenceDAL => DIService.Container.Resolve<DomainPersistenceDAL>();
        private ILogService _logService => DIService.Container.Resolve<LogService>();
        public async Task<T> GetAsync<T>(string id)
        {
            try
            {
                var serializedEntity = await _domainPersistenceDAL.GetAsync(id, typeof(T).ToString());
                var entity = JsonConvert.DeserializeObject<T>(serializedEntity);
                return entity;
            }
            catch (Exception ex)
            {
                var detail = $"Issues with GetAsync<{typeof(T)}> for id: {id}";
                var loggedException = new Exception(detail, ex);
                _logService.LogError(loggedException);
                return default(T);
            }
        }

        public async Task<bool> SaveAsync<T>(object entity,
                                              string id)
        {
            try
            {
                var serializedEntity = JsonConvert.SerializeObject(entity);
                await _domainPersistenceDAL.SaveAsync(id, typeof(T).ToString(), serializedEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logService.LogError(ex);
                return false;
            }
        }
    }
}
