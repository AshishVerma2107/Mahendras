using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using Android.Content;
using System;
using ImageSlider.MyTest;

namespace ImageSlider.Fragments
{
    public class MyShop : Fragment
    {
         
        string myshop;
        WebView mWebView;
        CustomProgressDialog cpd;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

         

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyShopInfo", FileCreationMode.Private);

            myshop = pref.GetString("MyShop", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.myshop, container, false);
           

             mWebView = v.FindViewById<WebView>(Resource.Id.web);

            if (myshop.Length <= 0)
            {
                getMyShop();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;
                mWebView.Settings.DomStorageEnabled = true;

                mWebView.SetWebChromeClient(new WebChromeClient());

                mWebView.SetWebViewClient(new MyWebViewClient(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org");

            }


            // mWebView.LoadUrl("https://www.facebook.com/Emahendras/");

            return v;
        }

        public void getMyShop()
        {

            try
            {
                mWebView.Settings.JavaScriptEnabled = true;
                mWebView.Settings.DomStorageEnabled = true;

                mWebView.SetWebChromeClient(new WebChromeClient());

                mWebView.SetWebViewClient(new MyWebViewClient(Activity));

                mWebView.LoadUrl("https://myshop.mahendras.org");

                if (myshop.Length <= 0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyShopInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("MyShop", "https://myshop.mahendras.org");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(MyShop));
                Activity.StartActivity(intent);

            }
            catch
            {

            }
           
        }
    }

    public class MyWebViewClient : WebViewClient
    {
        Android.App.Activity ac;
        CustomProgressDialog cpd;
        public MyWebViewClient(Android.App.Activity ac)
        {
            this.ac = ac;
            cpd = new CustomProgressDialog(ac);
            cpd.Show();
        }
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
           // cpd.Show();
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