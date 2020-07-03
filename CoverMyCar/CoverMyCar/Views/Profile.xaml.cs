using CoverMyCar.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            string lblName = HelperAppSettings.firstname.Substring(0, 1);
            string shName = HelperAppSettings.lastname.Substring(0, 1);
            //PrName.Text = lblName.ToUpper() + " " + shName.ToUpper();
            PageName.Text = HelperAppSettings.Name.ToUpper();
            UserName.Text = HelperAppSettings.username;
            gender.Text = HelperAppSettings.gender;
            addrLbl.Text = HelperAppSettings.address;
            emailTxt.Text = HelperAppSettings.email;
          
        }

        async void NotifyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notification());
        }
        async void EditProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfile());
        }

        public async void changePassClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (HelperAppSettings.gender.Contains("Male"))
            {
                profileImg.Source = "undrawMale.svg";
            }
            else
            {
                profileImg.Source = "undrawFemale.svg";
            }
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
            HelperAppSettings.loss_assessor_id = "";
            HelperAppSettings.agentType = "";
            HelperAppSettings.loss_assessor_id = "";
            HelperAppSettings.marketing_consultant_id = "";
            HelperAppSettings.phonenumber = "";
            HelperAppSettings.Name = "";
            Application.Current.MainPage = fpm;
        }
    }
}