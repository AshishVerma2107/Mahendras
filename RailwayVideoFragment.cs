using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;
using ImageSlider.MyTest;

namespace ImageSlider.Fragments
{
    public class RailwayVideoFragment : Fragment
    {
        string you_tuberail;
        WebView mWebView;
        CustomProgressDialog cpd;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //progress = new ProgressDialog(Activity);
            //progress.Indeterminate = true;
            //progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            //progress.SetCancelable(false);
            //progress.SetMessage("Please wait...");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTubeRAIL", FileCreationMode.Private);

            you_tuberail = pref.GetString("YOUTUBERAIL", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.RailVideoLayout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.youtuberail);

            if (you_tuberail.Length <= 0)
            {
                getMyYouTuberail();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientrail(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org/Display/DisplayItem?icd=303");

            }




            return v;
        }

        public void getMyYouTuberail()
        {

            try
            {
                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientrail(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org/Display/DisplayItem?icd=303");

                if (you_tuberail.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTubeRAIL", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("YOUTUBERAIL", "https://myshop.mahendras.org/Display/DisplayItem?icd=303");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(RailwayVideoFragment));
                Activity.StartActivity(intent);

            }
            catch
            {

            }

        }
    }

    public class MyWebViewClientrail : WebViewClient
    {
        Android.App.Activity ac;
        CustomProgressDialog cpd;

        public MyWebViewClientrail(Android.App.Activity ac)
        {
            this.ac = ac;
            cpd = new CustomProgressDialog(ac);
            cpd.Show();
        }

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }

        public override void OnPageStarted(WebView view, string url, Android.Graphics.Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);
            cpd.DismissDialog();
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);
            cpd.DismissDialog();
        }

        public override void OnReceivedError(WebView view, ClientError errorCode, string description, string failingUrl)
        {
            base.OnReceivedError(view, errorCode, description, failingUrl);
            cpd.DismissDialog();
        }
    }
}