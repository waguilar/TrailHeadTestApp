﻿using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Autofac;
using TrailHeadTestApp.Infrastructure;
using TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer;
using TrailHeadTestApp.Infrastructure.DataAccessLayer;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;
using TrailHeadTestApp.Interfaces.Infrastructure;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using TrailHeadTestApp.Infrastructure.Repositories;
using TrailHeadTestApp.ApiAccess;
using TrailHeadTestApp.Interfaces.APIAccess;
using TrailHeadTestApp.Services;
using TrailHeadTestApp.Interfaces.Services;

namespace TrailHeadTestApp.Droid
{
    [Activity(Label = "TrailHeadTestApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //Configure Autofac
            DIService.Container = AndroidDIConfiguration.InitDI();
            base.OnCreate(savedInstanceState);

            //Configure Barcode Scanner

            //Configure Xamarin Essentials
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        internal class AndroidDIConfiguration
        {
            public static IContainer InitDI()
            {
                var builder = new ContainerBuilder();

                //Register Platform Dependencies
                builder.RegisterType<NetworkConnection>().As<IConnection>().SingleInstance();
                builder.RegisterType<DbHelperAndroid>().As<IDbHelper>().SingleInstance();
                builder.RegisterType<DialogHelper>().As<IDialogHelper>().SingleInstance();

                //Repository
                builder.RegisterType<EmployeesRepository>().As<IEmployeesRepository>().SingleInstance();

                //Persistence
                builder.RegisterType<EntityPersistence>().As<IEntityPersistence>().SingleInstance();
                builder.RegisterType<DomainPersistenceDAL>(); //nope, we may need a different database for another user

                builder.RegisterType<RestAPI>().As<IRestAPI>().SingleInstance();

                //Services
                builder.RegisterType<LogService>().As<ILogService>().SingleInstance();

                return builder.Build();
            }
        }
    }
}