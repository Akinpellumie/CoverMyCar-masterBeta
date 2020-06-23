using CoverMyCar.Settings;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using FFImageLoading;

namespace CoverMyCar.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopImage
    {
        string image1;
        Stream imageData;
        //string image2;
        //string image3;
        public PopImage(string claimImage)
        {
            image1 = claimImage;
            //image2 = claimImg;
            //image3 = claimImag;
            InitializeComponent();
            LoadSingleClaim(image1);
        }

        public async void LoadSingleClaim(string image1)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            await Task.Delay(100); 
            if (image1 != null)
            {
                showImage.Source = Helper.ImageUrl + image1;
                indicator.IsRunning = false;
                indicator.IsVisible = false;
            }

           //else if (image2 != null)
           // {
           //     showImage.Source = Helper.ImageUrl + image2;
           //     indicator.IsRunning = false;
           //     indicator.IsVisible = false;
           // }

           // else if (image3 != null)
           // {
           //     showImage.Source = Helper.ImageUrl + image3;
           //     indicator.IsRunning = false;
           //     indicator.IsVisible = false;
           // }
            else
            {
                LblImg.IsVisible = true;
                LblImg.Text = "Unable to fetch image!!! Please check your internet connection!";
            }
        }

        public async void ClosePopUp_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }


        public async void DownloadImage(object sender, EventArgs args)
        {
            var uri = Helper.ImageUrl + image1;
            var url = new Uri(uri);
            WebClient webClient = new WebClient();
         //webClient.BaseAddress = Helper.ImageUrl + image1;

            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images", "temp");
            string fileName = uri.ToString().Split('/').Last();
            string filePath = System.IO.Path.Combine(folderPath, fileName);

            webClient.DownloadDataCompleted += (s, e) =>
            {
                Directory.CreateDirectory(folderPath);

                File.WriteAllBytes(filePath, e.Result);
            };

            webClient.DownloadDataAsync(url);
            await DisplayAlert("Yoo!","Image downloaded successfully!","Ok");

            //return filePath;
        }
        public async void DownloadImage_Clicked(object sender, EventArgs e)
        {
            //string xamrainImageUrl = Helper.ImageUrl + image1;
            //const string xamarinImageName = "claimVehicle.jpg";

            //var downloadedImage = await ImageService.DownloadImage(xamrainImageUrl);

            //ImageService.SaveToDisk(xamarinImageName, downloadedImage);

            //_downloadedImage.Source = ImageService.GetFromDisk(xamarinImageName);
            DependencyService.Get<IFileService>().SavePicture("ImageName.jpg", imageData, "imagesFolder");
        }
    }
}