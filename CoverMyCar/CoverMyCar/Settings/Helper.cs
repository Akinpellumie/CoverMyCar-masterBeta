﻿namespace CoverMyCar.Settings
{
    public class Helper
    {
        public static string domainurl
        {
            get
            {
                return "https://cover-my-car.herokuapp.com";
            }
        }
        public static string astraUrl
        {
            get
            {
                return "http://192.168.1.147:5501/?referenceNumber=";
            }
        }
        public static string CardPayUrl
        {
            get
            {
                return "http://192.168.1.147:5501/payWithCard.html?ref=";
            }
        }
        public static string bankUrl
        {
            get
            {
                return "http://192.168.1.106:8080/asteroid/webapi/secure/account_detail";
            }
        }

        public static string flutterUrl
        {
            get
            {
                return "https://api.ravepay.co/flwv3-pug/getpaidx/api/v2/hosted/pay";
            }
        }

        public static string ImageUrl
        {
            get
            {
                return "https://res.cloudinary.com/dmnghlyic/image/upload/v1587479916/";
            }
        }
        public static string bankCodesUrl { get { return Helper.bankUrl + "/bank_codes/"; } }
        public static string bankDetails { get { return Helper.bankUrl + "/other_banks/"; } }
        public static string fetchBankDetails { get { return Helper.domainurl + "/account/detail/"; } }
        public static string SignUpUrl { get { return Helper.domainurl + "/member/"; } }
        public static string CommPickerUrl { get { return Helper.domainurl + "/assessor/"; } }
        public static string LoginUrl { get { return Helper.domainurl + "/agent_auth/"; } }
        public static string MarkLogUrl { get { return Helper.domainurl + "/marketers_auth/"; } }
        public static string RegTokenUrl { get { return Helper.domainurl + "/assessor/registrationToken"; } }
        public static string RegMktTokenUrl { get { return Helper.domainurl + "/marketers/registrationToken"; } }
        public static string TransactionUrl { get { return Helper.domainurl + "/transaction?username="; } }

        public static string getMembersUrl { get { return Helper.domainurl + "/members/"; } }
        public static string getBanksUrl { get { return Helper.domainurl + "/banks/"; } }
        public static string BankNamesUrl { get { return Helper.domainurl + "/bank/bankCodes/"; } }
        public static string forgotMrktPswdUrl { get { return Helper.domainurl + "/marketers/forgotpassword/"; } }
        public static string changeMrktPswdUrl { get { return Helper.domainurl + "/marketers/resetPassword/"; } }
        public static string forgotAgntPswdUrl { get { return Helper.domainurl + "/agent/forgotpassword/"; } }
        public static string changeAssePswdUrl { get { return Helper.domainurl + "/assessor/resetPassword/"; } }
        public static string getMemberDetails { get { return Helper.domainurl + "/members?community_code="; } }
        public static string GetSingleCommunity { get { return Helper.domainurl + "/community/"; } }
        public static string EditAssessorUrl { get { return Helper.domainurl + "/assessor/"; } }
        public static string EditMrktnUrl { get { return Helper.domainurl + "/marketers/"; } }
        public static string getMemberFilter { get { return "&username="; } }
        public static string getRequestFilter { get { return "&status="; } }
        public static string msgReadFilter { get { return "&isRead=0"; } }
        public static string msgUnreadFilter { get { return "&isRead=1"; } }

        public static string GetClaimsUrl { get { return Helper.domainurl + "/claims/"; } }
        public static string getCommClaimsUrl { get { return Helper.domainurl + "/claims?communityCode="; } }
        public static string GetClaimByIdUrl { get { return Helper.domainurl + "/claims?policyHolder="; } }
        public static string GetPlansUrl { get { return Helper.domainurl + "/plans/"; } }
        public static string GetNotificationsUrl { get { return Helper.domainurl + "/notifications?username="; } }
        public static string GetNotificationsById { get { return Helper.domainurl + "/notifications/"; } }
        public static string GetCommsUrl { get { return Helper.domainurl + "/marketers/communities/"; } }
        public static string AssessorComSearchUrl { get { return Helper.domainurl + "/assessor/communities/" + HelperAppSettings.loss_assessor_id + "?name="; } }
        public static string MarketerComSearchUrl { get { return Helper.domainurl + "/marketers/communities/" + HelperAppSettings.marketing_consultant_id + "?name="; } }

        public static string NewPlanUrl { get { return Helper.domainurl + "/subscriptions/"; } }
        public static string UpdateRequestUrl { get { return Helper.domainurl + "/requests/"; } }
        public static string UpdateRolesUrl { get { return Helper.domainurl + "/members/roles/"; } }
        public static string getActiveSubUrl { get { return Helper.domainurl + "/subscriptions?username="; } }
        public static string GetRequestsUrl { get { return Helper.domainurl + "/requests?communityCode="; } }
        public static string UploadUrl { get { return Helper.domainurl + "/files/"; } }
        public static string UsernameAvailability { get { return Helper.domainurl + "/members/name/availability/"; } }


        public static Models.LoginProfileModel userprofile { get; set; }
        //public static Models.TransResModel transResponse { get; set; }
        //public static Models.PlansListModel plan_Id { get; set; }
        public static string TransUrl { get { return Helper.domainurl + "/transaction/"; } }

        public static string NoInternetText = "Check Internet Connection.";
    }
}