using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;

namespace ImageSlider.Fragments
{
    public class ST_Portal_Fragment : Fragment
    {
        string myportal;
        WebView mWebView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //progress = new ProgressDialog(Activity);
            //progress.Indeterminate = true;
            //progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            //progress.SetCancelable(false);
            //progress.SetMessage("Please wait...");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyPortalInfo", FileCreationMode.Private);

            myportal = pref.GetString("MyPortal", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.ST_Portal_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webstportal);

            if (myportal.Length <= 0)
            {
                getMyPortal();
            }

            else
            {
                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClient(Activity));

                mWebView.LoadUrl("https://stportal.mahendras.org");

            }

            return v;
        }

        public void getMyPortal()
        {
            try
            {
                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClient(Activity));

                mWebView.LoadUrl("https://stportal.mahendras.org");

                if (myportal.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyPortalInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("MyPortal", "https://stportal.mahendras.org");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(ST_Portal_Fragment));
                Activity.StartActivity(intent);
            }
            catch
            {

            }

        }
    }

    public class MyWebViewClientSTPortal : WebViewClient
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