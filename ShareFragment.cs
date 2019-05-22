
using Android.Support.V4.App;

using Android.Content;
using Android.OS;
using Android.Views;
using Android.Webkit;

namespace ImageSlider.Fragments
{
    public class ShareFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View v = inflater.Inflate(Resource.Layout.ShareLayout, container, false);

            WebView mWebView = v.FindViewById<WebView>(Resource.Id.share);

            mWebView.Settings.JavaScriptEnabled = true;

            mWebView.SetWebViewClient(new MyWebViewClientInvite());


            string appId = "com.makemedroid.keye5bb63c9";
            mWebView.LoadUrl("https://play.google.com/store/apps/details?id=" + appId);

            Intent sharingIntent = new Intent(Intent.ActionSend);
         
            sharingIntent.SetType("text/plain");
           
            sharingIntent.PutExtra(Intent.ExtraText, "https://play.google.com/store/apps/details?id=com.makemedroid.keye5bb63c9");


            StartActivity(Intent.CreateChooser(sharingIntent, "Share App Using"));

            return v;
        }
    }
    public class MyWebViewClientShare : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }

        public override void OnPageStarted(WebView view, string url, Android.Graphics.Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);
        }

        public override void OnReceivedError(WebView view, ClientError errorCode, string description, string failingUrl)
        {
            base.OnReceivedError(view, errorCode, description, failingUrl);
        }
    }
}