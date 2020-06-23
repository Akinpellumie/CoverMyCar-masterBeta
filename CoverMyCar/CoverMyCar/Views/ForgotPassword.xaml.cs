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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private async void ForgotPassClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopLoader());
            User update = new User()
            {
                username = pswdReset.Text,
            };
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var json = JsonConvert.SerializeObject(update);
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Helper.forgotAgntPswdUrl, result);

            if (response.IsSuccessStatusCode)
            {
                await PopupNavigation.Instance.PopAsync(true);
                await DisplayAlert("Success", "Kindly check your mailbox for new password", "Ok");
                ContentPage fpm = new LoginPage();
                Application.Current.MainPage = fpm;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    await DisplayAlert("InHub", "Whoopps! Invalid Username. Try Again!", "Ok");
                    
                }
                else
                {
                    await PopupNavigation.Instance.PopAsync(true);
                 
                    await DisplayAlert("InHub", "Please try again later", "Ok");

                }
            }
        }
    }
}