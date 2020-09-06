using System.Threading.Tasks;

namespace TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer
{
    public interface IEntityPersistence
    {
        Task<T> GetAsync<T>(string id);
        Task<bool> SaveAsync<T>(object entity, string id);
    }
}
