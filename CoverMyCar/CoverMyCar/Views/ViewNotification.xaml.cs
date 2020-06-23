using CoverMyCar.Models;
using CoverMyCar.Settings;
using Newtonsoft.Json;
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
    public partial class ViewNotification : ContentPage
    {
        int isRd;
        string NotclaimId;
        string notifyId;
        string dateMate;
        public string DateFormat
        {
            get
            {

                var r = DateTime.Parse(this.dateMate.Replace("[UTC]", ""));
                return r.ToLocalTime().ToShortDateString();
            }

        }
        string LblDate => this.DateFormat;
        public ViewNotification(string id)
        {
            InitializeComponent();
            LoadSingleMsg(id);
        }

        public async void LoadSingleMsg(string id)

        {
            var url = Helper.GetNotificationsById + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(url);
            var MsgsList = JsonConvert.DeserializeObject<NotificationsModel>(result);
            SingleMsgDetails.BindingContext = MsgsList.notifications[0];
            NotclaimId = MsgsList.notifications[0].link_type_id;
            notifyId = MsgsList.notifications[0].id;
            isRd = MsgsList.notifications[0].is_read;
            dateMate = MsgsList.notifications[0].date_sent;
            //LblMsgdate.Text = LblDate;

            if (notifyId != null && isRd == 0)
            {
                UpdateNotification update = new UpdateNotification()
                {
                    username = HelperAppSettings.username,
                    isRead = 1,
                    notificationId = notifyId,
                };

                var clientee = new HttpClient();
                clientee.DefaultRequestHeaders.Clear();
                clientee.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(update);
                HttpContent resulted = new StringContent(json);
                resulted.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseee = client.PutAsync(Helper.GetNotificationsById, resulted);
                if (responseee.IsCompleted)
                {
                    //await DisplayAlert("InHub", "Message Read", "Ok");

                }

            }
            else
            {
                return;
            }
        }

        public async void LoadSingleClaim(object sender, EventArgs e)
        {
                var url = Helper.GetClaimsUrl + NotclaimId;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var result = await client.GetStringAsync(url);
                var UsersList = JsonConvert.DeserializeObject<SingleClaimListModel>(result);
              
            if (HelperAppSettings.agentType.Contains("Loss Assessor") && UsersList.claim[0].status.Contains("Reported"))
            {
                await Shell.Current.Navigation.PushAsync(new LossAssessorClaim(NotclaimId));
            }
            else
            {
                await Shell.Current.Navigation.PushAsync(new SingleClaim(NotclaimId));
            }


        }
    }
}