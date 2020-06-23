using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoverMyCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShellMkt : Shell
    {
        public AppShellMkt()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}