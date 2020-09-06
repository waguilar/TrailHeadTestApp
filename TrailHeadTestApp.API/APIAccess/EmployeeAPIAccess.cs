using Autofac;
using System;
using System.Threading.Tasks;
using TrailHeadTestApp.API.HttpClient;
using TrailHeadTestApp.API.Service;
using TrailHeadTestApp.Interfaces.APIAccess;
using TrailHeadTestApp.Interfaces.DTO;
using TrailHeadTestApp.Interfaces.Infrastructure;
using TrailHeadTestApp.Services;

namespace TrailHeadTestApp.API.APIAccess
{
    public class EmployeeAPIAccess : IEmployeeAPIAccess
    {
        private readonly IConnection _connection;
        private readonly IDialogHelper _dialogHelper;
        private readonly ServiceFactory<IUserService> _listUserService;
        public EmployeeAPIAccess() : this ( ServiceLocator.Container.Resolve<IConnection>(),
                                            ServiceLocator.Container.Resolve<IDialogHelper>())
        {
        }

        public EmployeeAPIAccess( IConnection connection,
                                  IDialogHelper dialogHelper)
        {
            _connection = connection;
            _dialogHelper = dialogHelper;
            _listUserService = new ServiceFactory<IUserService>();
        }

        public async Task<IListUserResponseDTO> GetEmployeeList(string id)
        {
            try
            {
                var userResponseDTO = await _listUserService.CallApi().ListUsers(1);
                if (_connection.IsConnected)
                {
                    return userResponseDTO;
                }
                return userResponseDTO;
            }
            catch(Exception e)
            {
                _dialogHelper.Error(e.Message);
                throw;
            }
        }
    }
}
