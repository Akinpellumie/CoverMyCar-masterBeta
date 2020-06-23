using System;
using System.Collections.Generic;
using System.Text;

namespace CoverMyCar.Models
{
    public class AssessorModel
    {
        public string loss_assessor_id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string bankname { get; set; }
        public string bankcode { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string date_created { get; set; }
        public string fcm_token { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string added_by { get; set; }
        public string name { get; set; }
        public List<CommsModel> communities { get; set; }
        public string agentType { get; set; }
    }

    public class AssessModel
    {
        public List<AssessorModel> assessor { get; set; }
        public List<AssessorModel> marketer { get; set; }
    }
    public class CommsModel
    {
            public string id { get; set; }
            public string marketing_consultant_id { get; set; }
            public string loss_assessor_id { get; set; }
            public string community_code { get; set; }
            public string community_id { get; set; }
        public string Initial { get
            {
                var ini = community_name.Substring(0, 1);
                var nit = ini.ToUpper();
                return nit;
            }
            set { Initial = value; }
        }
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

    public class CommunityModel
    {
        public string community_id { get; set; }
        public string community_name { get; set; }
        public string community_description { get; set; }
        public string no_of_members { get; set; }
        public string date_created { get; set; }
        public string community_code { get; set; }
        public string address { get; set; }
        public string website_url { get; set; }
        public string email { get; set; }
        public string contact_no { get; set; }
        public string created_by { get; set; }
        public List<ClaimsModel> claims { get; set; }
        public List<getMembersModel> members { get; set; }
    }
    public class ComListModel
    {
        public List<CommsModel> communities { get; set; }
        public CommunityModel community { get; set; }
    }
    }
