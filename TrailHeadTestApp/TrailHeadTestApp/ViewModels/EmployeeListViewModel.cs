using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using TrailHeadTestApp.Interfaces.Models;
using TrailHeadTestApp.Util;
using Xamarin.Forms;

namespace TrailHeadTestApp.ViewModels
{
    public class EmployeeListViewModel : BaseViewModel
    {
        private readonly IEmployeesService _employeeService;

        public EmployeeListViewModel() : this(DIService.Container.Resolve<IEmployeesService>())
        {
        }

        public EmployeeListViewModel(IEmployeesService employeeService)
        {
            _employeeService = employeeService;
            Title = "Employee List - XAML";
            Items = new ObservableRangeCollection<IEmployee>();
            LoadEmployeesFromWebServiceCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
        }

        public ObservableRangeCollection<IEmployee> Items { get; set; }

        private bool itemsIsEmpty = true;
        public bool ItemsIsEmpty
        {
            get { return itemsIsEmpty; }
            set { SetProperty(ref itemsIsEmpty, value); }
        }

        public Command LoadEmployeesFromWebServiceCommand { get; set; }

        public async Task ExecuteLoadEmployeesCommand()
        {
            try
            {
                IsBusy = true;
                var result = await _employeeService.GetEmployeeList(0);
                Items.Clear();
                Items.AddRange(new ObservableCollection<IEmployee>(result));
                ItemsIsEmpty = !Items.Any();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
