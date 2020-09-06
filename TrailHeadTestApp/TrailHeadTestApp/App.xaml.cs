using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrailHeadTestApp.Services;
using TrailHeadTestApp.Views;

namespace TrailHeadTestApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
