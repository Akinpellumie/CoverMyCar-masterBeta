using Android.Content;
using Android.Graphics.Drawables;
using CoverMyCar.CustomControls;
using CoverMyCar.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearch), typeof(CustomSearchRenderer))]
namespace CoverMyCar.Droid
{
    public class CustomSearchRenderer : SearchBarRenderer
    {
        public CustomSearchRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}