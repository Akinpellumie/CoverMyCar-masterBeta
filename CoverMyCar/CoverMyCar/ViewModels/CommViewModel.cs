using System.Collections.Generic;
using System.Linq;

namespace CoverMyCar.ViewModels
{
    public class CommViewModel
    {
        public List<Communities> CommunityList { get; set; }

        public CommViewModel()
        {
            CommunityList = GetStatus().OrderBy(t => t.Value).ToList();
        }
        public List<Communities> GetStatus()
        {
            var communities = new List<Communities>()
        {
            new Communities(){key = 1, Value = "Zenith Bank Of Nigeria"},
            new Communities(){key = 2, Value = "First Bank of Nigeria"},
            new Communities(){key = 3, Value = "MTN Nigeria"}

        };
            return communities;
        }

    }
    public class Communities
    {
        public int key { get; set; }
        public string Value { get; set; }
    }
}
