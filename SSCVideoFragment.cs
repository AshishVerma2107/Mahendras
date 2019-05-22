using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;
using ImageSlider.MyTest;

namespace ImageSlider.Fragments
{
    public class SSCVideoFragment : Fragment
    {
        string you_tubessc;
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

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTubeSSC", FileCreationMode.Private);

            you_tubessc = pref.GetString("YOUTUBESSC", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.SSCVideoLayout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.youtubessc);

            if (you_tubessc.Length <= 0)
            {
                getMyYouTubessc();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientssc(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org/Display/DisplayItem?icd=300");

            }




            return v;
        }

        public void getMyYouTubessc()
        {

            try
            {
                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientssc(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org/Display/DisplayItem?icd=300");

                if (you_tubessc.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTubeSSC", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("YOUTUBESSC", "https://myshop.mahendras.org/Display/DisplayItem?icd=300");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(SSCVideoFragment));
                Activity.StartActivity(intent);

            }
            catch
            {

            }

        }
    }

    public class MyWebViewClientssc : WebViewClient
    {
        Android.App.Activity ac;
        CustomProgressDialog cpd;

        public MyWebViewClientssc(Android.App.Activity ac)
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