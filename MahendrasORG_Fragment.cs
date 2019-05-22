using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;

namespace ImageSlider.Fragments
{
    public class MahendrasORG_Fragment : Fragment
    {
        // ProgressDialog progress;
        string myORG;
        WebView mWebView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //progress = new ProgressDialog(Activity);
            //progress.Indeterminate = true;
            //progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            //progress.SetCancelable(false);
            //progress.SetMessage("Please wait...");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyORGInfo", FileCreationMode.Private);

            myORG = pref.GetString("MyORG", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.MahendrasORG_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.weborg);

            if (myORG.Length <= 0)
            {
                getMyORG();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientorg());

                mWebView.LoadUrl("https://www.mahendras.org");

            }


           

            return v;
        }

        public void getMyORG()
        {

            try
            {
                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientorg());

                mWebView.LoadUrl("https://www.mahendras.org");

                if (myORG.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyShopInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("MyORG", "https://www.mahendras.org");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(MahendrasORG_Fragment));
                Activity.StartActivity(intent);

            }
            catch
            {

            }

        }
    }

    public class MyWebViewClientorg : WebViewClient
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