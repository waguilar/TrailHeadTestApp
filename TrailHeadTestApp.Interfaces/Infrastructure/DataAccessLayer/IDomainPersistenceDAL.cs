using System.Threading.Tasks;

namespace TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer
{
    public interface IDomainPersistenceDAL
    {
        Task<T> GetAsync<T>(string id);

        Task<bool> SaveAsync<T>(object entity, string id, bool setLastUpdateValue = true);
    }
}
