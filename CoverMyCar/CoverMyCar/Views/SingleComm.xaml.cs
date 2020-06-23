using CoverMyCar.Models;
using CoverMyCar.PopUps;
using CoverMyCar.Settings;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleComm : ContentPage
    {
        public SingleComm(string community_id)
        {
            InitializeComponent();
            GetComDetails(community_id);
            GetMembers(community_id);
            CheckInternet();
        }
        async void CheckInternet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await PopupNavigation.Instance.PushAsync(new PopNoInternet());
            }
        }

        public void SubHider_clicked(object sender, EventArgs e)
        {
            if (actSubHide.IsVisible == true)
            {
                actSubHide.IsVisible = false;
                Frm2.IsVisible = false;
                Frm2b.IsVisible = true;
                chvUp.Source = "chevronP.png";
            }
            else if (actSubHide.IsVisible == false)
            {
                actSubHide.IsVisible = true;
                Frm2.IsVisible = true;
                Frm2b.IsVisible = false;
                chvUp.Source = "chevronDn.png";
            }

        }

        public async void GetMembers(string community_id)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;


            HttpClient client = new HttpClient();
            var dashboardEndpoint = Helper.GetSingleCommunity + community_id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(dashboardEndpoint);
            var MemsList = JsonConvert.DeserializeObject<ComListModel>(result);
            MemList.ItemsSource = MemsList.community.members;


            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        public async void GetComDetails(string community_id)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;


            HttpClient client = new HttpClient();
            var dashboardEndpoint = Helper.GetSingleCommunity + community_id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(dashboardEndpoint);
            var MemsList = JsonConvert.DeserializeObject<ComListModel>(result);
            CommSingleStack.BindingContext = MemsList.community;


            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }
    }
}