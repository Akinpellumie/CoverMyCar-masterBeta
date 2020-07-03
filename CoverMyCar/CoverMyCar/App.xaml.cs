using System;
using Xamarin.Forms;
using CoverMyCar.Services;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;

namespace CoverMyCar
{
    public partial class App : Application
    {

        //public static Page currentpage { get; private set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new SplashPage();
        }

        protected override void OnStart() 
        {
            Permission();
                AppCenter.Start("android=046ccbae-cd95-4731-9b9b-56f3e81ffb6f;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));

        }

    protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        async void Permission()
        {
            await Permissions.RequestAsync<Permissions.Camera>();
            await Permissions.RequestAsync<Permissions.StorageRead>();
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            await Permissions.RequestAsync<Permissions.LocationAlways>();
            await Permissions.RequestAsync<Permissions.NetworkState>();
        }

    }
}