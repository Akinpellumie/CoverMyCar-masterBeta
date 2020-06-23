using CoverMyCar.Models;
using System.Collections.Generic;

namespace CoverMyCar.Settings
{
    public static class HelperAppSettings
    {
        public static string firstname { get; set; }
        public static string lastname { get; set; }
        public static string Token { get; set; }
        public static string AndroidId { get; set; }
        public static string id { get; set; }
        public static string Status { get; set; }
        public static string fcm_token { get; set; }
        public static string AppToken { get; set; }
        public static string loss_assessor_id { get; set; }
        public static string marketing_consultant_id { get; set; }
        public static string username { get; set; }
        public static string priviledges { get; set; }
        public static string capName { get; set; }
        public static string added_by { get; set; }
        public static string Name { get; set; }
        public static string email { get; set; }
        public static string phonenumber { get; set; }
        public static string gender { get; set; }
        public static string isActive { get; set; }
        public static string community_code { get; set; }
        public static string community_name { get; set; }
        public static string department { get; set; }
        public static string address { get; set; }
        public static string bankname { get; set; }
        public static string agentType { get; set; }
        public static string account_number { get; set; }
        public static string account_name { get; set; }

        public static string profile_img_url { get; set; }
        public static string date_created { get; set; }
        public static string commCode { get
            {
                return LoginProfileModel.CommCode;
            }
            set { commCode = value; }
        }
        public static List<ComModel> communities { get; set; }
    }

    public static class TransHelper
    {
        public static string referenceNumber { get; set; }
        public static string transactionId { get; set; }
        public static string inHubRefNum { get; set; }
    }
}