using System.Collections.Generic;

namespace CoverMyCar.Models
{
    public class LoginProfileModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string token { get; set; }
        public string fcm_token { get; set; }
        public string marketing_consultant_id { get; set; }
        public string loss_assessor_id { get; set; }
        public string agentType { get; set; }
        public string username { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string is_active { get; set; }
        public string added_by { get; set; }
        public string address { get; set; }
        public string bankname { get; set; }
        public string bankcode { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }

        public string profile_img_url { get; set; }
        public string date_created { get; set; }
        public static List<ComModel> communities { get; set; }
        public static string commCode
        {
            get
            {
                var source = communities[0].community_code;
                return source;
            }
            set { commCode = value; }
        }

        public string name
        {
            get
            {
                return this.firstname + "  " + this.lastname;
            }
        }

        public string capitalizedname
        {
            get
            {
                return this.name.ToUpper();
            }
        }
        public static string CommCode { get
            {
                return commCode;
            } 
            set { CommCode = value; }
        }
    }

    public class LoginResponseModel
    {
        public List<LoginProfileModel> user { get; set; }
        public string message { get; set; }
    }

    public class ComModel
    {
        public string id { get; set; }
        public string marketing_consultant_id { get; set; }
        public string loss_assessor_id { get; set; }
        public string community_id { get; set; }
        public string community_code { get; set; }
        public string community_name { get; set; }
        public string community_description { get; set; }
        public string no_of_members { get; set; }
        public string date_created { get; set; }
        public string address { get; set; }

        public string website_url { get; set; }
        public string email { get; set; }
        public string contact_no { get; set; }
        public string created_by { get; set; }
        public string community { get; set; }

    }
    public class ProfileModel
    {
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string phonenumber { get; set; }


        public string username { get; set; }

        public string name
        {
            get
            {
                return this.firstname + "  " + this.lastname;
            }
        }

        public string capitalizedName
        {
            get
            {
                return this.name.ToUpper();
            }
        }

    }
}