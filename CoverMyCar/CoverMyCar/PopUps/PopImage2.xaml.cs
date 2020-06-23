using CoverMyCar.Settings;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopImage2
    {
        string image2;
        Stream imageData;
        //string image2;
        //string image3;
        public PopImage2(string claimImg)
        {
            //image1 = claimImage;
            image2 = claimImg;
            //image3 = claimImag;
            InitializeComponent();
            LoadSingleClaim(image2);
        }

        public async void LoadSingleClaim(string image2)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            await Task.Delay(100);
            if (image2 != null)
            {
                showImage2.Source = Helper.ImageUrl + image2;
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
                LblImg2.IsVisible = true;
                LblImg2.Text = "Unable to fetch image!!! Please check your internet connection!";
            }
        }

        public async void ClosePopUp_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        public void DownloadImage_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFileService>().SavePicture("ImageName.jpg", imageData, "imagesFolder");
        }
    }
}