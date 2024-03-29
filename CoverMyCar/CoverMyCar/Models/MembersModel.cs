﻿using CoverMyCar.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CoverMyCar.Models
{
    public class AddMemberModel
    {
        public string fullname
        {
            get
            {
                return firstname + "  " + lastname;
            }
        }
        public string email { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("phonenumber")]
        public string phonenumber { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("communityCode")]
        public string communityCode { get; set; }
        public AddMemberModel(string username, string firstname, string email, string lastname, string phonenumber, string password, string communityCode)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.phonenumber = phonenumber;
            this.email = email;
            this.communityCode = communityCode;
            this.password = password;

        }
        public bool CheckInformation()
        {
            if (!this.username.Equals("") && !this.firstname.Equals("") && !this.communityCode.Equals("") && !this.password.Equals("") && !this.lastname.Equals("") && !this.email.Equals("") && !this.phonenumber.Equals(""))
                return true;
            else
                return false;
        }
    }

   
    public class getMembersModel
    {
        public string id { get; set; }
        public string lossAssessorId { get; set; }
        public string marketingConsultantId { get; set; }
        public string username { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string bankName { get; set; }
        public string bankname { get; set; }
        public string bankCode { get; set; }
        public string bankcode { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string isActive { get; set; }
        public string community_code { get; set; }
        public string department { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string Image { get; set; }

        public string profileImgUrl { get; set; }
        public ImageSource MemImage
        {

            get
            {
                if (string.IsNullOrEmpty(profile_img_url))
                {
                    var source1 = ImageSource.FromFile("placeholder.png");
                    return source1;
                }
                else
                {
                    Uri source = new Uri(Helper.ImageUrl + profile_img_url);
                    return source;

                }

            }
            set { MemImage = value; }
        }
        public List<string> priviledges { get; set; }
        //public List<string> policyDetails { get; set; }
        public string date_created { get; set; }
        public string fullname
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
                return this.fullname.ToUpperInvariant();
            }
        }
        //public string NameSort => fullname[0].ToString();
        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(firstname) || firstname.Length == 0)
                    return "?";

                return firstname[0].ToString().ToUpper();
            }
        }
    }

    public class MembersListModel
    {
        public List<getMembersModel> members { get; set; }
        public static ObservableCollection<getMembersModel> Members { get; set; }
    }

    public class SinMemberModel
    {
        public List<getMembersModel> member { get; set; }
    }

    public class TxnCardModel
    {
        public string email { get; set; }
        public string txref { get; set; }
        public string PBFPubKey { get; set; }
        public string customer_email { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string redirect_url { get; set; }
    }
}
