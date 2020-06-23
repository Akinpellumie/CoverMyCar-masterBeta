using Newtonsoft.Json;
using CoverMyCar.Models;
using CoverMyCar.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using CoverMyCar.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
            CheckInternet();


        }
        async void CheckInternet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
            }
        }

        public async void ForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPass());
        }
        //public async void SignUpClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new SignUp());
        //}

        public async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
                return;
            }
            await PopupNavigation.Instance.PushAsync(new PopLoader());
            try
            {
                if (string.IsNullOrEmpty(UsernameInput.Text) || string.IsNullOrEmpty(PasswordInput.Text))
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    await DisplayAlert("Alert", "Username or Password cannot be empty", "ok");
                    return;
                }
                User members = new User(UsernameInput.Text, PasswordInput.Text)
                {
                    username = UsernameInput.Text,
                    password = PasswordInput.Text,
                    mac = HelperAppSettings.AndroidId,
                    logOutOfDevice = false
                };

                if (members.CheckInformation())
                {


                    var json = JsonConvert.SerializeObject(members);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Token", HelperAppSettings.Token);
                    var loginEndpoint = Helper.LoginUrl;

                    var result = await client.PostAsync(loginEndpoint, content);
                    
                    if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        await DisplayAlert("InHub", "Invalid Username or Password", "Ok");
                        return;
                    }

                    else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        var pels = await DisplayAlert("Alert", UsernameInput.Text + " is logged in on another device. LogOut of the old device to continue!", "Confirm", "Decline");
                        if (pels == true)
                        {
                            User member = new User(UsernameInput.Text, PasswordInput.Text)
                            {
                                username = UsernameInput.Text,
                                password = PasswordInput.Text,
                                mac = HelperAppSettings.AndroidId,
                                logOutOfDevice = true
                            };
                            var json2 = JsonConvert.SerializeObject(member);
                            var contents = new StringContent(json2, Encoding.UTF8, "application/json");

                            HttpClient _client = new HttpClient();
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Token", HelperAppSettings.Token);
                            var LoginEndpoint = Helper.LoginUrl;

                            var resultee = await client.PostAsync(LoginEndpoint, contents);

                            string responseeee = await resultee.Content.ReadAsStringAsync();
                            if (result.IsSuccessStatusCode)
                            {
                                var profile = JsonConvert.DeserializeObject<LoginProfileModel>(responseeee);

                                Helper.userprofile = profile;

                                HelperAppSettings.Token = profile.token;
                                HelperAppSettings.firstname = profile.firstname;
                                HelperAppSettings.lastname = profile.lastname;
                                HelperAppSettings.username = profile.username;
                                HelperAppSettings.email = profile.email;
                                HelperAppSettings.phonenumber = profile.phonenumber;
                                HelperAppSettings.marketing_consultant_id = profile.marketing_consultant_id;
                                HelperAppSettings.loss_assessor_id = profile.loss_assessor_id;
                                HelperAppSettings.agentType = profile.agentType;
                                HelperAppSettings.fcm_token = profile.fcm_token;
                                HelperAppSettings.capName = profile.capitalizedname;
                                HelperAppSettings.bankname = profile.bankname;
                                HelperAppSettings.address = profile.address;
                                HelperAppSettings.gender = profile.gender;
                                HelperAppSettings.Name = profile.name;
                                HelperAppSettings.account_number = profile.account_number;

                                if (HelperAppSettings.agentType.Contains("Loss Assessor"))
                                {
                                    AppShell fpm = new AppShell();
                                    Application.Current.MainPage = fpm;
                                }

                                else if (HelperAppSettings.agentType.Contains("Marketing Consultant"))
                                {
                                    AppShellMkt fpm1 = new AppShellMkt();
                                    Application.Current.MainPage = fpm1;
                                }

                                await PopupNavigation.Instance.PopAsync(true);
                            }
                            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                await PopupNavigation.Instance.PopAsync(true);
                                await DisplayAlert("Login Failed", "Username or Password is Incorrect, Please Try Again!", "Ok");

                            }
                        }
                    }

                    else
                    {
                        string responsee = await result.Content.ReadAsStringAsync();
                        if (responsee == null)
                        {
                            await PopupNavigation.Instance.PopAsync(true);
                            await DisplayAlert("Oops!", "Check your connection and try Again", "Ok");
                            return;
                        }
                        else if (result.IsSuccessStatusCode == false)
                        {
                            await PopupNavigation.Instance.PopAsync(true);
                            await DisplayAlert("Alert", "Incorrect Username or Password", "Retry");
                            return;
                        }

                        else if (result.IsSuccessStatusCode)
                        {
                            var profile = JsonConvert.DeserializeObject<LoginProfileModel>(responsee);
                            Helper.userprofile = profile;

                            HelperAppSettings.Token = profile.token;
                            HelperAppSettings.firstname = profile.firstname;
                            HelperAppSettings.lastname = profile.lastname;
                            HelperAppSettings.username = profile.username;
                            HelperAppSettings.email = profile.email;
                            HelperAppSettings.phonenumber = profile.phonenumber;
                            HelperAppSettings.marketing_consultant_id = profile.marketing_consultant_id;
                            HelperAppSettings.loss_assessor_id = profile.loss_assessor_id;
                            HelperAppSettings.agentType = profile.agentType;
                            HelperAppSettings.fcm_token = profile.fcm_token;
                            HelperAppSettings.capName = profile.capitalizedname;
                            HelperAppSettings.bankname = profile.bankname;
                            HelperAppSettings.address = profile.address;
                            HelperAppSettings.gender = profile.gender;
                            HelperAppSettings.Name = profile.name;
                            HelperAppSettings.account_number = profile.account_number;

                            if (HelperAppSettings.agentType.Contains("Loss Assessor"))
                            {
                                AppShell fpm = new AppShell();
                                Application.Current.MainPage = fpm;
                            }

                            else if (HelperAppSettings.agentType.Contains("Marketing Consultant"))
                            {
                                AppShellMkt fpm1 = new AppShellMkt();
                                Application.Current.MainPage = fpm1;
                            }

                            await PopupNavigation.Instance.PopAsync(true);
                            try
                            {
                                await SecureStorage.SetAsync("token", PasswordInput.Text);
                                await SecureStorage.SetAsync("username", UsernameInput.Text);
                            }
                            catch (Exception)
                            {
                                return;
                            }
                        }
                        else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                          
                            await PopupNavigation.Instance.PopAsync(true);
                            await DisplayAlert("Login Failed", "Username or Password is Incorrect, Please Try Again!", "Ok");

                        }

                    }

                }
                else
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    await DisplayAlert("Login", "Invalid Login details", "ok");

                }

            }
            catch (Exception)
            {
                return;
            }
        }


        public bool RememberMe
        {
            get => Preferences.Get(nameof(RememberMe), false);
            set
            {
                Preferences.Set(nameof(RememberMe), value);
                if (!value)
                {
                    Preferences.Set(nameof(Username), string.Empty);
                }
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        string username = Preferences.Get(nameof(Username), string.Empty);
        public string Username
        {
            get => username;
            set
            {
                username = value;
                if (RememberMe)
                    Preferences.Set(nameof(Username), value);
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (toggleSwitch.IsToggled == true)
            {
                try
                {
                    var password = await SecureStorage.GetAsync("token");
                    PasswordInput.Text = password;
                    var username = await SecureStorage.GetAsync("username");
                    UsernameInput.Text = username;
                }
                catch (Exception)
                {
                    // Possible that device doesn't support secure storage on device.
                }
            }
        }

    }
}