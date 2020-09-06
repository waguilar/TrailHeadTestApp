using Autofac;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailHeadTestApp.Infrastructure.ApiModel;
using TrailHeadTestApp.Interfaces.APIAccess;
using TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using TrailHeadTestApp.Interfaces.Models;
using TrailHeadTestApp.Interfaces.Services;

namespace TrailHeadTestApp.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IRestAPI _restAPI;
        private readonly IConnection _connection;
        private readonly IEntityPersistence _entityPersistence;
        private readonly ILogService _logService;

        public EmployeesRepository() : base()
        {
            _restAPI = DIService.Container.Resolve<IRestAPI>();
            _connection = DIService.Container.Resolve<IConnection>();
            _entityPersistence = DIService.Container.Resolve<IEntityPersistence>();
            _logService = DIService.Container.Resolve<ILogService>();
        }

        public Task<IEmployee> GetEmployee(int Id, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IEmployee>> GetEmployeeList(int page, bool forceRefresh = false)
        {
            try
            {
                if (_connection.IsConnected)
                {
                    var result = await _restAPI.RestServiceGetCallAsync(string.Format( Constants.GET_USER_LIST + $"?page={page}"));
                    if (!string.IsNullOrEmpty(result))
                    {
                        var response = JsonConvert.DeserializeObject<GetEmpoyeeListResponse>(result);
                        await _entityPersistence.SaveAsync<GetEmpoyeeListResponse>(response, "GetEmpoyeeListResponse");
                        return response.data;

                    }
                    return new List<IEmployee>();
                }
                else
                {
                    var cache = await _entityPersistence.GetAsync<GetEmpoyeeListResponse>("GetEmpoyeeListResponse");
                    return cache.data;
                }
            }
            catch (Exception ex)
            {
                _logService.LogError(ex);
                return null;
            }
        }
    }
}
