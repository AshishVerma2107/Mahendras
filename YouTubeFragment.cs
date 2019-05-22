using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;
using ImageSlider.MyTest;

namespace ImageSlider
{
    public class YouTubeFragment : Fragment
    {
        string you_tube;
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

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTube", FileCreationMode.Private);

            you_tube = pref.GetString("YOUTUBE", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Youtube_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.youtube);

           


            if (you_tube.Length <= 0)
            {
                getMyYouTube();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientvideo(Activity));

                mWebView.LoadUrl("https://www.youtube.com/results?search_query=mahendra+guru");

            }


            

            return v;
        }

        public void getMyYouTube()
        {

            try
            {
                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientvideo(Activity));

                mWebView.LoadUrl("https://www.youtube.com/results?search_query=mahendra+guru");

                if (you_tube.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTube", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("YOUTUBE", "https://www.youtube.com/results?search_query=mahendra+guru");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(YouTubeFragment));
                Activity.StartActivity(intent);

            }
            catch
            {

            }

        }
    }

    public class MyWebViewClientvideo : WebViewClient
    {
        Android.App.Activity ac;
        CustomProgressDialog cpd;

        public MyWebViewClientvideo(Android.App.Activity ac)
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