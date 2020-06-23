using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoverMyCar.Settings;
using System.Net.Http;
using Newtonsoft.Json;
using CoverMyCar.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using CoverMyCar.PopUps;
using Rg.Plugins.Popup.Services;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMkt : ContentPage
    {
        private string appToken;

        //{ public ObservableCollection<Models.PlansListModel> plan_id { get; set; }
        public DashboardMkt()
        {
            InitializeComponent();
            GetComms();
            CheckInternet();
            LblName.Text = HelperAppSettings.Name;
            string lblName = HelperAppSettings.firstname.Substring(0, 1);
            string shName = HelperAppSettings.lastname.Substring(0, 1);
            LblProName.Text = lblName.ToUpper() + shName.ToUpper();
        }

        async void CheckInternet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
            }
        }
        public async void GetComms()
        {
            HttpClient client = new HttpClient();
            var UserCountEndpoint = Helper.GetCommsUrl + HelperAppSettings.marketing_consultant_id;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", HelperAppSettings.Token);

            var result = await client.GetStringAsync(UserCountEndpoint);
            var UsersCnt = JsonConvert.DeserializeObject<List<CommsModel>>(result);

            DashmKTList.ItemsSource = UsersCnt;
        }
        public void SubHider_clicked(object sender, EventArgs e)
        {
            if (actSubHide.IsVisible == true)
            {
                actSubHide.IsVisible = false;
                Frm2.IsVisible = false;
                Frm2b.IsVisible = true;
                chvUp.Source = "chevronP.png";
            }
            else if (actSubHide.IsVisible == false)
            {
                actSubHide.IsVisible = true;
                Frm2.IsVisible = true;
                Frm2b.IsVisible = false;
                chvUp.Source = "chevronDn.png";
            }

        }

        public async void NotifyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notification());
        }


        public async void changePassClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }


        public async void NotifyIconClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notification());
        }
        

        public void SignOutClicked(object sender, EventArgs e)
        {
            ContentPage fpm = new LoginPage();
            HelperAppSettings.Token = "";
            HelperAppSettings.firstname = "";
            HelperAppSettings.lastname = "";
            HelperAppSettings.username = "";
            HelperAppSettings.email = "";
            HelperAppSettings.agentType = "";
            HelperAppSettings.loss_assessor_id = "";
            HelperAppSettings.marketing_consultant_id = "";
            HelperAppSettings.phonenumber = "";
            HelperAppSettings.Name = "";
            Application.Current.MainPage = fpm;
        }

        public async void ViewCommTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedUser = e.Item as CommsModel;
            await Shell.Current.Navigation.PushAsync(new SingleComm(selectedUser.community_id));

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GetComms();

            try
            {
                appToken = await SecureStorage.GetAsync("refreshedToken");
            }
            catch (Exception)
            {
                return;
            }


            if (!string.IsNullOrEmpty(appToken))
            {
                User update = new User()
                {
                    username = HelperAppSettings.username,
                    registrationToken = appToken
                };
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(update);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(Helper.RegMktTokenUrl, result);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User App Registration Token sent successfully!");
                }
            }
        }

    }
}