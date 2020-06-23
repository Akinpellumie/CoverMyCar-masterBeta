using System;
using Xamarin.Forms;
using CoverMyCar.Services;
using System.Threading;
using System.Threading.Tasks;

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
            // Handle when your app starts
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