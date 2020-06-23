using System;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms.Platform;
using Android.OS;
using static Android.Provider.Settings;
using CoverMyCar.Settings;

namespace CoverMyCar.Droid
{
    [Activity(Label = "PeerCover Agent", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        TextView msgText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.Main);
            //msgText = FindViewById<TextView>(Resource.Id.msgText);

            IsPlayServicesAvailable();

            CreateNotificationChannel();

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            GetDevId();
            CachedImageRenderer.InitImageViewHandler();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //the OnCreate method will verify that Google Play Services is available before the app attempts to use FCM services.
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    //msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        //Apps that are running on Android 8.0 (API level 26) or higher must create a notification channel for publishing their notifications. This method will create a notification panel

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }


        public string GetDevId()
        {
            id = Build.Serial;
            if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
            {
                try
                {
                    var context = Application.Context;
                    id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                }
                catch (Exception ex)
                {
                    Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                }
            }

            Console.WriteLine("The device serial number is: " + id);
            HelperAppSettings.AndroidId = id;
            return id;
        }

        string id = string.Empty;

    }
}