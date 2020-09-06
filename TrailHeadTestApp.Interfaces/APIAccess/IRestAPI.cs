using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrailHeadTestApp.Interfaces.APIAccess
{
    public interface IRestAPI
    {
        Task<string> RestServiceGetCallAsync(string url, bool isReturningRaw = false);

    }
}
