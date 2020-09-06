using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrailHeadTestApp.Interfaces.Models;

namespace TrailHeadTestApp.Interfaces.Infrastructure.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IEmployee> GetEmployee(int Id,bool forceRefresh = false);
        Task<List<IEmployee>> GetEmployeeList(int Id, bool forceRefresh = false);
    }
}
