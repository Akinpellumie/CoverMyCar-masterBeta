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
    public partial class ClaimsPage : ContentPage
    {
        string comCoda;
        public ClaimsPage()
        {
            InitializeComponent();
            GetComms();
            FetchClaims();
            CheckInternet();
            //GetAssClaims();
            //CommPicker.BindingContext = new CommViewModel();
        }
        async void CheckInternet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
            }
        }

        private void ComPck_SldIdxChanged(object sender, EventArgs e)
        {
            ScndStack.IsVisible = false;
            FrstStack.IsVisible = true;
            GetAssClaims();
        }
        async void GetAssClaims()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            try
            {

                HttpClient client = new HttpClient();
                var pckCode = (CommPicker.SelectedItem as CommsModel).community_code;
                var dashboardEndpoint = Helper.getCommClaimsUrl + pckCode;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var result = await client.GetStringAsync(dashboardEndpoint);
                var ClaimsList = JsonConvert.DeserializeObject<ClaimsListModel>(result);


                ClaimList.ItemsSource = ClaimsList.claims;
                if (result.Contains("id"))
                {
                    FrstStack.IsVisible = true;
                    emptyStack.IsVisible = false;
                }
                else
                {
                    FrstStack.IsVisible = false;
                    emptyStack.IsVisible = true;
                }

                indicator.IsRunning = false;
                indicator.IsVisible = false;
            }
            catch (Exception)
            {
                return;

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

            CommPicker.ItemsSource = UsersCnt.assessor[0].communities;
        }

        public async void ViewClaimTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {

            if (e.Item == null) return;
            var selectedUser = e.Item as ClaimsModel;
                if (selectedUser.status == "Reported")
                {
                    await Shell.Current.Navigation.PushAsync(new LossAssessorClaim(selectedUser.id));
                }
                else
                {
                    await Shell.Current.Navigation.PushAsync(new SingleClaim(selectedUser.id));
                }

            }
            catch(Exception)
            {
                return;
            }
        }


        async void FetchClaims()
        {
            HttpClient client = new HttpClient();
            var UserdetailEndpoint = Helper.EditAssessorUrl + HelperAppSettings.loss_assessor_id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UserdetailEndpoint);
            var ClaimsList = JsonConvert.DeserializeObject<AssessModel>(result);
            comCoda = ClaimsList.assessor[0].communities[0].community_code;

            if (!string.IsNullOrEmpty(comCoda))
            {
                HttpClient clientee = new HttpClient();
                var dashboardEndpoint = Helper.getCommClaimsUrl + comCoda;
                clientee.DefaultRequestHeaders.Clear();
                clientee.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var resultee = await client.GetStringAsync(dashboardEndpoint);
                var ClaimseList = JsonConvert.DeserializeObject<ClaimsListModel>(resultee);

                ClmList.ItemsSource = ClaimseList.claims;
            }
            else
            {
                return;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetComms();
            FetchClaims();
            
            //GetAssClaims();

        }
        async void NotifyClicked(object sender, EventArgs e)
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
            HelperAppSettings.phonenumber = "";
            HelperAppSettings.loss_assessor_id = "";
            HelperAppSettings.agentType = "";
            HelperAppSettings.fcm_token = "";
            HelperAppSettings.capName = "";
            HelperAppSettings.bankname = "";
            HelperAppSettings.address = "";
            HelperAppSettings.gender = "";
            HelperAppSettings.Name = "";
            HelperAppSettings.account_number = "";
            Application.Current.MainPage = fpm;
        }

        public async void changePassClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }
    }
}