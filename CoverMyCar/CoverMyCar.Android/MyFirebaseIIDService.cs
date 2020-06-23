using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using CoverMyCar.Settings;
using CoverMyCar.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace CoverMyCar.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    [Obsolete]
    
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        [Obsolete]
        public override async void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            Console.WriteLine(refreshedToken);
            HelperAppSettings.AppToken = refreshedToken;
            SendRegistrationToServerAsync(refreshedToken);
            try
            {
                await SecureStorage.SetAsync("refreshedToken", refreshedToken);
            }
            catch (Exception)
            {
                return;
            }
        }
        void SendRegistrationToServerAsync(string refreshedToken)
        {
            
        }

    }
}