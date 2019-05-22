using Android.Views;

namespace ImageSlider
{
    public class LocalOnClickListener : Java.Lang.Object, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            HandleOnClick();
        }
        public System.Action HandleOnClick { get; set; }
    }
}