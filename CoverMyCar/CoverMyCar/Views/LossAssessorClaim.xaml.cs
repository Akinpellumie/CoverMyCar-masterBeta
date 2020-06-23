using CoverMyCar.Models;
using CoverMyCar.PopUps;
using CoverMyCar.Settings;
using Newtonsoft.Json;
using Plugin.FileUploader.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LossAssessorClaim : ContentPage
    {
        string myId;
        string quoName;
        string claimImage;
        string claimImag;
        string claimImg;
        private string fileName;

        public LossAssessorClaim(string id)
        {
            myId = id;
            InitializeComponent();
            LoadSingleClaim(id);
            CheckInternet();
        }

        async void CheckInternet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
            }
        }
        public async void LoadSingleClaim(string id)
        {
            var url = Helper.GetClaimsUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(url);
            var UsersList = JsonConvert.DeserializeObject<SingleClaimListModel>(result);
            LASingleClaimDetails.BindingContext = UsersList.claim[0];
            //LnName.Text = UsersList.claim[0].firstname + UsersList.claim[0].lastname;
            claimImage = UsersList.claim[0].claimImages[0].imgUrl;
            claimImg = UsersList.claim[0].claimImages[1].imgUrl;
            claimImag = UsersList.claim[0].claimImages[2].imgUrl;
            //ClaimantName.Text = Xname + Yname;

            if (claimImage != null)
            {
                claimImage1.Source = Helper.ImageUrl + claimImage;
            }
            if (claimImg != null)
            {
                claimImage2.Source = Helper.ImageUrl + claimImg;
            }
            if (claimImag != null)
            {
                claimImage3.Source = Helper.ImageUrl + claimImag;
            }

        }

        public async void OnImageTapped(object sender, EventArgs e)
        {
            if(claimImage != null)
            {
                await PopupNavigation.Instance.PushAsync(new PopImage(claimImage));
            }
            else
            {
                await DisplayAlert("Oops!", "Image not available", "Ok");
                return;
            }
        }

        public async void OnImage2Tapped(object sender, EventArgs e)
        {
            if (claimImg != null)
            {
                await PopupNavigation.Instance.PushAsync(new PopImage2(claimImg));
            }
            else
            {
                await DisplayAlert("Oops!", "Image not available", "Ok");
                return;
            }
        }

        public async void OnImage3Tapped(object sender, EventArgs e)
        {
            if (claimImag != null)
           {
                await PopupNavigation.Instance.PushAsync(new PopImage3(claimImag));
           }
            else
            {
                await DisplayAlert("Oops!","Image not available","Ok");
                return;
            }
        }

        public async void UploadDocTapped(object sender, EventArgs e)
        {
            Permission();
            await CrossMedia.Current.Initialize();
            try
            {


                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Warning", "Allow system permission for this App", "Ok");

                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    Name = "QuotationUpload.jpg",
                    CompressionQuality = 50,
                    SaveToAlbum = true
                });
                var mg3File = file.Path;
                if (string.IsNullOrEmpty(mg3File) == false)
                {
                    var upfilebytes = File.ReadAllBytes(mg3File);

                    ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                    var name = Path.GetFileName(mg3File);
                    FilePathItem filePath = new FilePathItem("fileName", file.Path);

                    if (file != null)
                    {
                        QuoDoc.Text = file.AlbumPath;

                    }

                    ImageSource.FromStream(() => file.GetStream());


                    FileUploadResponse k = null;
                    try
                    {
                        SubBtn.IsEnabled = false;
                        QuoDoc.IsVisible = false;
                        progressBar.Text = "Please wait while document is uploading....";
                        progressBar.IsVisible = true;
                        Indic.IsVisible=true;
                        k = await Plugin.FileUploader.CrossFileUploader.Current.UploadFileAsync(Helper.UploadUrl, filePath,
                            new Dictionary<string, string>() { { "Authorization", Helper.userprofile.token } }, new Dictionary<string, string>()
                            { { "fileName", this.fileName } });
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    string responseeee = k.Message;
                    if (k.StatusCode == 201)
                    {
                        await Indic.ProgressTo(0.9, 950, Easing.SpringIn);
                        quoName = responseeee;
                        Indic.IsVisible = false;
                        SubBtn.IsEnabled = true;

                    }
                    else if (k.StatusCode == 401)
                    {
                        await DisplayAlert("Ooops!", "Server error. Please try again later." , "ok");
                    } 
                    else
                    {
                        await DisplayAlert("Oops!", "Please try again later.", "ok");
                    }
                }
            }

            catch (Exception)
            {
                return;
            }
        }

        private async void UpdateClaimClicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
                return;
            }
            await PopupNavigation.Instance.PushAsync(new PopLoader());

            LossAssModel update = new LossAssModel()
            {
                status = "Loss Assessor Recommended Amount",
                lossAssessorUsername = HelperAppSettings.username,
                claimId = myId,
            };
            var pt = double.Parse(RecInput.Text.Replace
                   ("₦", CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol),
                   NumberStyles.Currency).ToString();

            update.lossAssessorAmount = pt;
            update.quotationDocUrl = quoName;



            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var json = JsonConvert.SerializeObject(update);
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(Helper.GetClaimsUrl, result);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("InHub", "Recommendation Successful", "Ok");
                await Shell.Current.Navigation.PushAsync(new Dashboard());
                await PopupNavigation.Instance.PopAsync(true);

            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await DisplayAlert("Oops!", "Server error, Please try again later" , "Ok");
                    await PopupNavigation.Instance.PopAsync(true);
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await DisplayAlert("Oops!","Session timeout, please login again.","Ok");
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    await DisplayAlert("Oops!", "Please try again later", "Ok");

                }
            }
        }

        public void CostInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((Entry)sender).Text))
            {
                var t = double.Parse(((Entry)sender).Text.Replace("₦", CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol), NumberStyles.Currency).ToString("C", CultureInfo.GetCultureInfo("en-us")).Replace("$", "₦");
                ((Entry)sender).Text = t;
                ((Entry)sender).CursorPosition = t.Length - 3;
            }
        }

        async void Permission()
        {
            await Permissions.RequestAsync<Permissions.Camera>();
            await Permissions.RequestAsync<Permissions.StorageRead>();
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            await Permissions.RequestAsync<Permissions.LocationAlways>();
            await Permissions.RequestAsync<Permissions.NetworkState>();
        }
    }
}