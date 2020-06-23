using System;
using Xamarin.Forms;
using CoverMyCar.Services;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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


    }
}