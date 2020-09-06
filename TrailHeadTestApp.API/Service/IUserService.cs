using Refit;
using System.Threading.Tasks;
using TrailHeadTestApp.API.DTO;

namespace TrailHeadTestApp.API.Service
{
    public interface IUserService
    {
        [Get("/users")]
        Task<ListUserResponseDTO> ListUsers([AliasAs("page")] int page);
    }
}
