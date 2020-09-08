using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Autofac;
using TrailHeadTestApp.Infrastructure;
using TrailHeadTestApp.Infrastructure.DataAccessLayer;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;
using TrailHeadTestApp.Interfaces.Infrastructure;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using TrailHeadTestApp.ApiAccess;
using TrailHeadTestApp.Interfaces.APIAccess;
using TrailHeadTestApp.Services;
using TrailHeadTestApp.Interfaces.Services;
using TrailHeadTestApp.Infrastructure.Services;
using TrailHeadTestApp.Interfaces.Infrastructure.DataAccessLayer;

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

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            //Configure Xamarin Essentials
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Configure Barcode Scanner
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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
                builder.RegisterType<EmployeesService>().As<IEmployeesService>().SingleInstance();

                //Data Access Layer
                builder.RegisterType<DomainPersistenceDAL>().As<IDomainPersistenceDAL>(); //nope, we may need a different database for another user

                //Api Access
                builder.RegisterType<RestAPI>().As<IRestAPI>().SingleInstance();

                //Services
                builder.RegisterType<LogService>().As<ILogService>().SingleInstance();

                return builder.Build();
            }
        }
    }
}