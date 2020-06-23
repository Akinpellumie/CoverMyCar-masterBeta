using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopNoInternet
    {
        public PopNoInternet()
        {
            InitializeComponent();
        }

        public async void ClosePopUp_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}