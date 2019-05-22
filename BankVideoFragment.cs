using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;
using ImageSlider.MyTest;

namespace ImageSlider.Fragments
{
   public class BankVideoFragment : Fragment
    {
        string you_tubebank;
        WebView mWebView;
        CustomProgressDialog cpd;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

          

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTubeBANK", FileCreationMode.Private);

            you_tubebank = pref.GetString("YOUTUBEBANK", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.BankVideoLayout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.youtubebank);

            if (you_tubebank.Length <= 0)
            {
                getMyYouTubebank();
                
            }
            else
            {
               

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientbankvideo(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org/Display/DisplayItem?icd=299");

                

            }




            return v;
        }

        public void getMyYouTubebank()
        {

            try
            {
               

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientbankvideo(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org/Display/DisplayItem?icd=299");

                if (you_tubebank.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyYouTubeBANK", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("YOUTUBEBANK", "https://myshop.mahendras.org/Display/DisplayItem?icd=299");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(BankVideoFragment));
                Activity.StartActivity(intent);

                
            }
            catch
            {
                

            }

        }
    }

    public class MyWebViewClientbankvideo : WebViewClient
    {
        Android.App.Activity ac;
        CustomProgressDialog cpd;

        public MyWebViewClientbankvideo(Android.App.Activity ac)
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