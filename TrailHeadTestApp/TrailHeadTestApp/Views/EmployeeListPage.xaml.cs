using Autofac;
using System;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrailHeadTestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeListPage : ContentPage
    {
        private readonly IEmployeesRepository _employeesRepository;
        public EmployeeListPage(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        public EmployeeListPage() : this(DIService.Container.Resolve<IEmployeesRepository>())
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var list = _employeesRepository.GetEmployeeList(0);
            }
            catch (Exception e)
            {

            }
        }
    }
}