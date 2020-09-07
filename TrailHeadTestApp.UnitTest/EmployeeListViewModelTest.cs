using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailHeadTestApp.Infrastructure.ApiModel;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using TrailHeadTestApp.Interfaces.Models;
using TrailHeadTestApp.ViewModels;

namespace TrailHeadTestApp.UnitTest
{
    [TestFixture]
    public class Tests
    {

        private List<IEmployee> GetEmployeeList()
        {
            var employeeList = new List<IEmployee>()
            {
                new Employee{ Id=0, FirstName="Walter", LastName="Aguilar", Avatar="" },
                new Employee{ Id=0, FirstName="Jhon", LastName="Doe", Avatar="" }
            };
            return employeeList;
        }

        [Test]
        public async Task LoadEmployeesTestAsync()
        {
            //Arrange
            var employeeList = GetEmployeeList();
            var assignmentsRepositoryMock = new Mock<IEmployeesRepository>();

            assignmentsRepositoryMock
                .Setup(x => x.GetEmployeeList(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(employeeList);

            //Act
            var vm = new EmployeeListViewModel(assignmentsRepositoryMock.Object);
            await vm.ExecuteLoadEmployeesCommand();

            //Assert
            Assert.AreEqual(vm.Items.Count, 2);
        }

    }
}