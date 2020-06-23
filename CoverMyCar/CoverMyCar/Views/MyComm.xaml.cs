using CoverMyCar.Models;
using CoverMyCar.PopUps;
using CoverMyCar.Settings;
using CoverMyCar.ViewModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyComm : ContentPage
    {
        public MyComm()
        {
            InitializeComponent();
            GetComms();
            CheckInternet();
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
            var UserCountEndpoint = Helper.CommPickerUrl + HelperAppSettings.loss_assessor_id;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", HelperAppSettings.Token);

            var result = await client.GetStringAsync(UserCountEndpoint);
            var UsersCnt = JsonConvert.DeserializeObject<AssessModel>(result);

            ComsList.ItemsSource = UsersCnt.assessor[0].communities;
        }

        public async void ViewCommTapped(object sender, ItemTappedEventArgs e)
        {
                if (e.Item == null) return;
                var selectedUser = e.Item as CommsModel;
                await Shell.Current.Navigation.PushAsync(new SingleComm(selectedUser.community_id));
           
        }
        public async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue.Length >= 2)
            {
                    if (Helper.getMembersUrl == null)
                {
                    indicator.IsRunning = true;
                    indicator.IsVisible = true;
                }

                var url = Helper.AssessorComSearchUrl + e.NewTextValue;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var result = await client.GetStringAsync(url);
                var UsersList = JsonConvert.DeserializeObject<List<CommsModel>>(result);


                    if (UsersList != null)
                    {
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;

                        ComsList.ItemsSource = UsersList;
                    }
                    else if (UsersList == null)
                    {
                        searchStack.IsVisible = true;
                        //await DisplayAlert("Search", "No Record Found", "Ok");
                    }
                }

                else if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    GetComms();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        async void NotifyClicked(object sender, EventArgs e)
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
            HelperAppSettings.commCode = "";
            HelperAppSettings.Name = "";
            Application.Current.MainPage = fpm;
        }
    }
}