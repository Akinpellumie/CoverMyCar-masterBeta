using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CoverMyCar
{
    public partial class AppShell : Shell
    {
        public AppShell()
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
