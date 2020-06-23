using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoverMyCar.Settings;
using System.Net.Http;
using Newtonsoft.Json;
using CoverMyCar.Models;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using CoverMyCar.PopUps;
using Rg.Plugins.Popup.Services;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        string commCoda;
        private string appToken;

        public Dashboard()
        {
            InitializeComponent();
            CheckInternet();
            GetUnreadNotifications();
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

       public async void Notify1Clicked(object sender, EventArgs e)
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

        public async void FetchClaims()
        {
            try
            {
                HttpClient client = new HttpClient();
                var UserdetailEndpoint = Helper.EditAssessorUrl + HelperAppSettings.loss_assessor_id;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var result = await client.GetStringAsync(UserdetailEndpoint);
                var ClaimsList = JsonConvert.DeserializeObject<AssessModel>(result);
                commCoda = ClaimsList.assessor[0].communities[0].community_code;

                if (!string.IsNullOrEmpty(commCoda))
                {
                    try
                    {
                        HttpClient clientee = new HttpClient();
                        var dashboardEndpoint = Helper.getCommClaimsUrl + commCoda;
                        clientee.DefaultRequestHeaders.Clear();
                        clientee.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                        var resultee = await client.GetStringAsync(dashboardEndpoint);
                        var ClaimseeList = JsonConvert.DeserializeObject<ClaimsListModel>(resultee);

                        DashList.ItemsSource = ClaimseeList.claims;
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else if(string.IsNullOrEmpty(commCoda))
                {
                    await DisplayAlert("Hallo!", "No Available claims!", "Ok");
                }

            }
            catch(Exception)
            {
                return;
            }
        }

        public async void ViewClaimTapped(object sender, ItemTappedEventArgs e)
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

        public async void GetUnreadNotifications()
        {

            HttpClient client = new HttpClient();
            var dashboardEndpoint = Helper.GetNotificationsUrl + HelperAppSettings.username + Helper.msgReadFilter;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(dashboardEndpoint);
            var NotsList = JsonConvert.DeserializeObject<NotificationsModel>(result);

            if (result.Contains("id"))
            {
                notifyIcn.IconImageSource = "notify.png";
            }
            else
            {
                notifyIcn.IconImageSource = "notifyMe.png";
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GetUnreadNotifications();
            FetchClaims();
            sendFcmToken();

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

                var response = await client.PutAsync(Helper.RegTokenUrl, result);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User App Registration Token sent successfully!");
                }
            }
        }
        public async void sendFcmToken()
        {
            if (string.IsNullOrEmpty(HelperAppSettings.fcm_token))
            {
                User update = new User()
                {
                    username = HelperAppSettings.username,
                    registrationToken = HelperAppSettings.AppToken
                };
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(update);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (HelperAppSettings.agentType.Contains("Marketing Consultant"))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                    var response = await client.PutAsync(Helper.RegMktTokenUrl, result);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Mktn Cons Registration Token sent successfully!");
                    }
                }
                else if (HelperAppSettings.agentType.Contains("Loss Assessor"))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                    var response = await client.PutAsync(Helper.RegTokenUrl, result);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Assessor Registration Token sent successfully!");
                    }
                }

            }
        }
    }
}