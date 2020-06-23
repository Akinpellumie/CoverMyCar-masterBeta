using System.Collections.Generic;
using System.Linq;

namespace CoverMyCar.ViewModels
{
    public class GenderViewModel
    {
        public List<Genders> GenderList { get; set; }

        public GenderViewModel()
        {
            GenderList = GetBanks().OrderBy(t => t.Value).ToList();
        }
        public List<Genders> GetBanks()
        {
            var genders = new List<Genders>()
        {
            new Genders(){key = 1, Value = "Male"},
            new Genders(){key = 2, Value = "Female"},

        };
            return genders;
        }

    }
    public class Genders
    {
        public int key { get; set; }
        public string Value { get; set; }
    }
}