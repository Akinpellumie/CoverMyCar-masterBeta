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
    public partial class ViewImage2 : ContentPage
    {
        string image2;
        //IDownloader downloader = DependencyService.Get<IDownloader>();
        public ViewImage2(string claimImg)
        {
            image2 = claimImg;
            InitializeComponent();
            //downloader.OnFileDownloaded += OnFileDownloaded;
            LoadSingleClaim(image2);
        }

        public async void LoadSingleClaim(string image2)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            await Task.Delay(100);
            if (image2 != null)
            {
                showImage.Source = Helper.ImageUrl + image2;
                indicator.IsRunning = false;
                indicator.IsVisible = false;
            }
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


        //File downloader class and method using IDownloader interface

    }
}