using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailHeadTestApp.Domain;
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
        public async Task LoadEmployeesReturnsDataTestAsync()
        {
            //Arrange
            var employeeList = GetEmployeeList();
            var assignmentsRepositoryMock = new Mock<IEmployeesService>();

            assignmentsRepositoryMock
                .Setup(x => x.GetEmployeeList(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(employeeList);

            //Act
            var vm = new EmployeeListViewModel(assignmentsRepositoryMock.Object);
            await vm.ExecuteLoadEmployeesCommand();

            //Assert
            Assert.IsFalse(vm.ItemsIsEmpty);
        }

        [Test]
        public async Task LoadEmployeesReturnsEmptyDataTestAsync()
        {
            //Arrange
            var employeeList = new List<IEmployee>();
            var assignmentsRepositoryMock = new Mock<IEmployeesService>();

            assignmentsRepositoryMock
                .Setup(x => x.GetEmployeeList(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(employeeList);

            //Act
            var vm = new EmployeeListViewModel(assignmentsRepositoryMock.Object);
            await vm.ExecuteLoadEmployeesCommand();

            //Assert
            Assert.IsTrue(vm.ItemsIsEmpty);
        }

        [Test]
        public async Task LoadEmployeesReturnsNullDataTestAsync()
        {
            //Arrange
            var assignmentsRepositoryMock = new Mock<IEmployeesService>();

            assignmentsRepositoryMock
                .Setup(x => x.GetEmployeeList(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync((List<IEmployee>)null);

            //Act
            var vm = new EmployeeListViewModel(assignmentsRepositoryMock.Object);
            await vm.ExecuteLoadEmployeesCommand();

            //Assert
            Assert.IsTrue(vm.ItemsIsEmpty);
        }

    }
}