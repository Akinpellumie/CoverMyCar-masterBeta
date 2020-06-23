using Android.Graphics.Drawables;
using CoverMyCar.CustomControls;
using CoverMyCar.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Context = Android.Content.Context;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace CoverMyCar.Droid
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}