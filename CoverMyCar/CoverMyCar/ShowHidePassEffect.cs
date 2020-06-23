using Xamarin.Forms;

namespace CoverMyCar
{
    public class ShowHidePassEffect : RoutingEffect
    {
        public string Entry { get; set; }
        public ShowHidePassEffect() : base("Xamarin.ShowHidePassEffect") { }
    }
}