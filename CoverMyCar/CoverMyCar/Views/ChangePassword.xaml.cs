using CoverMyCar.Models;
using CoverMyCar.PopUps;
using CoverMyCar.Settings;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void ChangePassClicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
                return;
            }
            await PopupNavigation.Instance.PushAsync(new PopLoader());
            User update = new User()
            {
                username = HelperAppSettings.username,
                oldpassword = OldPasswordInput.Text,
                newpassword = NewPasswordInput.Text,
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var json = JsonConvert.SerializeObject(update);
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            if(!string.IsNullOrEmpty(HelperAppSettings.loss_assessor_id))
            {
                var response = await client.PostAsync(Helper.changeAssePswdUrl, result);
            
                if (response.IsSuccessStatusCode)
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    await DisplayAlert("Successful", "Kindly Login Again", "Ok");
                    ContentPage fpm = new LoginPage();
                    OldPasswordInput.Text = "";
                    NewPasswordInput.Text = "";
                    Application.Current.MainPage = fpm;
                    indicator.IsVisible = false;
                    indicator.IsRunning = false;

                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        await DisplayAlert("Oops!", "Server error! Kindly try again later.", "Ok");
                        indicator.IsVisible = false;
                        indicator.IsRunning = false;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        await DisplayAlert("Oops!", "Session timeout. Please login again!", "Ok");
                        Application.Current.MainPage = new NavigationPage(new LoginPage());
                    }
                    else
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        await DisplayAlert("Oops!", "Please try again later", "Ok");

                    }
                }
            }
            else
            {
                var response = await client.PostAsync(Helper.changeMrktPswdUrl, result);

                if (response.IsSuccessStatusCode)
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    await DisplayAlert("Successful", "Kindly Login Again", "Ok");
                    ContentPage fpm = new LoginPage();
                    OldPasswordInput.Text = "";
                    NewPasswordInput.Text = "";
                    Application.Current.MainPage = fpm;
                    indicator.IsVisible = false;
                    indicator.IsRunning = false;

                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        await DisplayAlert("Alert", "Whoopps! Kindly check your internet connection", "Ok");
                        indicator.IsVisible = false;
                        indicator.IsRunning = false;
                    }
                    else
                    {
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;
                        await PopupNavigation.Instance.PopAsync(true);
                        await DisplayAlert("Alert", "Please try again later", "Ok");

                    }
                }
            }
        }
    }
}