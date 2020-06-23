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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {
        string myGend;
        string myBnk;
        string bnkCd2;
        string bnkNm2;
        string bnkCde;
        string bnkNm;
        string ageGend;
        string oldGend;
        public EditProfile()
        {
            InitializeComponent();
            GetUserById();
            GetBanks();
            GenderPicker.BindingContext = new GenderViewModel();
            BankPicker.Title = HelperAppSettings.bankname;
           
        }

        public async void GetUserById()
        {
            await PopupNavigation.Instance.PushAsync(new PopLoader());
            if (string.IsNullOrEmpty(HelperAppSettings.loss_assessor_id))
            {
                GetMrktrById();
                await PopupNavigation.Instance.PopAsync(true);
            }

            else if (string.IsNullOrEmpty(HelperAppSettings.marketing_consultant_id))
            {
                GetAssessorById();
                await PopupNavigation.Instance.PopAsync(true);
            }

        }

        async void GetMrktrById()
        {
            HttpClient client = new HttpClient();
            var UserdetailEndpoint = Helper.EditMrktnUrl + HelperAppSettings.marketing_consultant_id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UserdetailEndpoint);
            var MemberDetails = JsonConvert.DeserializeObject<AssessModel>(result);

            FNInput.BindingContext = MemberDetails.marketer[0];
            LNInput.BindingContext = MemberDetails.marketer[0];
            EMInput.BindingContext = MemberDetails.marketer[0];
            PNInput.BindingContext = MemberDetails.marketer[0];
            ACNInput.BindingContext = MemberDetails.marketer[0];
            ANMInput.BindingContext = MemberDetails.marketer[0];
            ADRInput.BindingContext = MemberDetails.marketer[0];
            BankPicker.BindingContext = MemberDetails.marketer[0];
            BankPicker.Title = MemberDetails.marketer[0].bankname;
            GenderPicker.Title = MemberDetails.marketer[0].gender;
            bnkCde = MemberDetails.marketer[0].bankcode;
            bnkNm = MemberDetails.marketer[0].bankname;
            ageGend = MemberDetails.marketer[0].gender;
            if (string.IsNullOrEmpty(bnkNm))
            {
                BankPicker.Title = "--Select Bank--";
            }
            else
            {
                BankPicker.Title = bnkNm;
            }

            if (string.IsNullOrEmpty(ageGend))
            {
                GenderPicker.Title = "--Select Gender--";
            }
            else
            {
                GenderPicker.Title = ageGend;
            }
        }

        async void GetAssessorById()
        {
            HttpClient client = new HttpClient();
            var UserdetailEndpoint = Helper.EditAssessorUrl + HelperAppSettings.loss_assessor_id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UserdetailEndpoint);
            var MemberDetails = JsonConvert.DeserializeObject<AssessModel>(result);

            FNInput.BindingContext = MemberDetails.assessor[0];
            LNInput.BindingContext = MemberDetails.assessor[0];
            EMInput.BindingContext = MemberDetails.assessor[0];
            PNInput.BindingContext = MemberDetails.assessor[0];
            ACNInput.BindingContext = MemberDetails.assessor[0];
            ANMInput.BindingContext = MemberDetails.assessor[0];
            ADRInput.BindingContext = MemberDetails.assessor[0];
            BankPicker.BindingContext = MemberDetails.assessor[0];
            BankPicker.Title = MemberDetails.assessor[0].bankname;
            GenderPicker.Title = MemberDetails.assessor[0].gender;
            bnkCde = MemberDetails.assessor[0].bankcode;
            bnkNm = MemberDetails.assessor[0].bankname;
            ageGend = MemberDetails.assessor[0].gender;
            if (string.IsNullOrEmpty(bnkNm))
            {
                BankPicker.Title = "--Select Bank--";
            }
            else
            {
                BankPicker.Title = bnkNm;
            }

            if (string.IsNullOrEmpty(ageGend))
            {
                GenderPicker.Title = "--Select Gender--";
            }
            else
            {
                GenderPicker.Title = ageGend;
            }
        }

        public async void GetBanks()
        {
            HttpClient client = new HttpClient();
            var UserCountEndpoint = Helper.BankNamesUrl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UserCountEndpoint);
            var UsersCnt = JsonConvert.DeserializeObject<List<BanksModel>>(result);

            BankPicker.ItemsSource = UsersCnt;
        }

        public void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                ACNInput.TextColor = Color.Default;
            }
            if (BankPicker.SelectedItem != null && string.IsNullOrEmpty(ACNInput.Text) == false)
            {
                if (e.NewTextValue.Length == 10)
                {
                    Fetchdetails();
                }

            }
        }
        public void Input2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                ANMInput.TextColor = Color.Default;
            }
        }


        async void Fetchdetails()
        {

            await PopupNavigation.Instance.PushAsync(new PopAcctLoader());
            try
            {
                HttpClient client = new HttpClient();
                var BnkDetailsEndpoint = Helper.fetchBankDetails + (BankPicker.SelectedItem as BanksModel).code + "/" + ACNInput.Text;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Settings.Helper.userprofile.token);

                var result = await client.GetAsync(BnkDetailsEndpoint);
                string responsee = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var profile = JsonConvert.DeserializeObject<BanksModel>(responsee);
                    if (profile != null)
                    {
                        ANMInput.Text = profile.accountName;
                    }
                    else
                    {
                        await DisplayAlert("Alert", "An error occured, please try again later", "ok");
                    }

                    await PopupNavigation.Instance.PopAsync(true);
                }

                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Application.Current.MainPage = new LoginPage();
                }

                else
                {
                    await DisplayAlert("Alert", "An error occured, please re-enter account details", "ok");
                }
                //await PopupNavigation.Instance.PopAsync(true);
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PopAsync(true);
                return;
            }
        }


        private void BnkPck_SldIdxChanged(object sender, EventArgs e)
        {
            if (BankPicker.SelectedItem != null && string.IsNullOrEmpty(ACNInput.Text) == false)
            {
                Fetchdetails();
            }
            bnkNm2 = (BankPicker.SelectedItem as BanksModel).name;
            bnkCd2 = (BankPicker.SelectedItem as BanksModel).code;
        }

        private void GndPck_SldIdxChanged(object sender, EventArgs e)
        {
            oldGend = (GenderPicker.SelectedItem as Genders).Value;
        }

        private void UpdateAgentClicked(object sender, EventArgs e)
        {
            if(HelperAppSettings.agentType.Contains("Loss Assessor"))
            {
                UpdateAssessor();
            }
            else if(HelperAppSettings.agentType.Contains("Marketing Consultant"))
            {
                UpdateMarketers();
            }
        }

        async void UpdateMarketers()
        {
            try
            {

                if (string.IsNullOrEmpty(ACNInput.Text) || string.IsNullOrEmpty(ANMInput.Text))
                {
                    await DisplayAlert("Hey!", "Account Number or Account Name cannot be empty", "Ok");
                    ACNInput.PlaceholderColor = Color.Red;
                    ANMInput.PlaceholderColor = Color.Red;
                    return;
                }

                Indic.IsVisible = true;
                getMembersModel update = new getMembersModel()
                {
                    firstname = FNInput.Text,
                    lastname = LNInput.Text,
                    lossAssessorId = HelperAppSettings.marketing_consultant_id,
                    phonenumber = PNInput.Text,
                    email = EMInput.Text,
                    accountNumber = ACNInput.Text.Trim(),
                    address = ADRInput.Text,
                    accountName = ANMInput.Text,
                };

                if (!string.IsNullOrEmpty(bnkNm2) && !string.IsNullOrEmpty(bnkCd2))
                {
                    update.bankname = bnkNm2;
                    update.bankcode = bnkCd2;
                }
                else if (string.IsNullOrEmpty(bnkNm2) && string.IsNullOrEmpty(bnkCd2))
                {
                    update.bankname = bnkNm;
                    update.bankcode = bnkCde;
                }


                if (!string.IsNullOrEmpty(oldGend))
                {
                    update.gender = oldGend;
                }
                else if (string.IsNullOrEmpty(oldGend))
                {
                    update.gender = ageGend;
                }


                var clientee = new HttpClient();
                clientee.DefaultRequestHeaders.Clear();
                clientee.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var jsonUpd = JsonConvert.SerializeObject(update);
                HttpContent result = new StringContent(jsonUpd);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await clientee.PutAsync(Helper.EditMrktnUrl, result);

                if (response.IsSuccessStatusCode)
                {
                    await Indic.ProgressTo(0.9, 950, Easing.SpringIn);
                    await DisplayAlert("Success!", "Profile Updated", "Ok");
                    await Shell.Current.Navigation.PushAsync(new Profile());
                    Indic.IsVisible = false;
                    //indicator.IsVisible = false;
                    //indicator.IsRunning = false;

                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Oops!", "Server error. Please try again later" , "Ok");
                        Indic.IsVisible = false;
                        //indicator.IsVisible = false;
                        //indicator.IsRunning = false;
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await DisplayAlert("Oops!" , "Session timeout. Please login again" , "Ok");
                        Application.Current.MainPage = new NavigationPage(new LoginPage());
                    }
                    else
                    {
                        Indic.IsVisible = false;
                        //indicator.IsRunning = false;
                        //indicator.IsVisible = false;
                        await DisplayAlert("Oops!", "Please try again later", "Ok");

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        async void UpdateAssessor()
        {
            try
            {

                if (string.IsNullOrEmpty(ACNInput.Text) || string.IsNullOrEmpty(ANMInput.Text))
                {
                    await DisplayAlert("Hey!", "Account Number or Account Name cannot be empty", "Ok");
                    ACNInput.PlaceholderColor = Color.Red;
                    ANMInput.PlaceholderColor = Color.Red;
                    return;
                }

                Indic.IsVisible = true;
                getMembersModel update = new getMembersModel()
                {
                    firstname = FNInput.Text,
                    lastname = LNInput.Text,
                    lossAssessorId = HelperAppSettings.loss_assessor_id,
                    phonenumber = PNInput.Text,
                    email = EMInput.Text,
                    accountNumber = ACNInput.Text.Trim(),
                    address = ADRInput.Text,
                    accountName = ANMInput.Text,
                };

                if (!string.IsNullOrEmpty(bnkNm2) && !string.IsNullOrEmpty(bnkCd2))
                {
                    update.bankname = bnkNm2;
                    update.bankcode = bnkCd2;
                }
                else if (string.IsNullOrEmpty(bnkNm2) && string.IsNullOrEmpty(bnkCd2))
                {
                    update.bankname = bnkNm;
                    update.bankcode = bnkCde;
                }


                if (!string.IsNullOrEmpty(oldGend))
                {
                    update.gender = oldGend;
                }
                else if (string.IsNullOrEmpty(oldGend))
                {
                    update.gender = ageGend;
                }


                var clientee = new HttpClient();
                clientee.DefaultRequestHeaders.Clear();
                clientee.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var jsonUpd = JsonConvert.SerializeObject(update);
                HttpContent result = new StringContent(jsonUpd);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await clientee.PutAsync(Helper.EditAssessorUrl, result);

                if (response.IsSuccessStatusCode)
                {
                    await Indic.ProgressTo(0.9, 950, Easing.SpringIn);
                    await DisplayAlert("Success!", "Profile Updated", "Ok");
                    await Shell.Current.Navigation.PushAsync(new Profile());
                    Indic.IsVisible = false;
                    //indicator.IsVisible = false;
                    //indicator.IsRunning = false;

                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Oops!", "Server error. please try again later." , "Ok");
                        Indic.IsVisible = false;
                        //indicator.IsVisible = false;
                        //indicator.IsRunning = false;
                    }
                    else
                    {
                        Indic.IsVisible = false;
                        //indicator.IsRunning = false;
                        //indicator.IsVisible = false;
                        await DisplayAlert("Oops!", "Please try again later", "Ok");

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}