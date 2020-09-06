using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrailHeadTestApp.Interfaces.APIAccess;
using TrailHeadTestApp.Interfaces.Services;

namespace TrailHeadTestApp.ApiAccess
{
    public class RestAPI : IRestAPI
    {
        private readonly HttpClient _authorizedHttpClient;
        private readonly ILogService _log;

        public RestAPI(ILogService logger)
        {
            _authorizedHttpClient = new HttpClient()
            {
                Timeout = new TimeSpan(0, 0, 1, 0)
            };
            _log = logger;
        }

        public async Task<string> RestServiceGetCallAsync(string url,
                                                           bool isReturningRaw = false)
        {
            var baseUrl = Constants.BASE_URL + url;
            HttpResponseMessage response;
            string responseJson;
            try
            {
                var newUriBaseUrl = new Uri(baseUrl);
                response = await _authorizedHttpClient.GetAsync(newUriBaseUrl);
                responseJson = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _log.LogError(ex);
                throw;
            }
            if (isReturningRaw)
            {
                return responseJson;
            }
            return
               responseJson
                  .Replace("[]", "{}"); // Replace empty array with empty object to avoid serialization problems
        }

    }
}
