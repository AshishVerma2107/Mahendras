using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using AndroidHUD;

namespace ImageSlider.Fragments
{
    public class LikeFragment : Fragment
    {

        string like;
        WebView mWebView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("LikeInfo", FileCreationMode.Private);

            like = pref.GetString("MyLike", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Like_Layout, container, false);

             mWebView = v.FindViewById<WebView>(Resource.Id.like);

            if (like.Length <= 0)
            {
                getMyLike();
            }
            {

                mWebView.Settings.JavaScriptEnabled = true;

                AndHUD.Shared.Show(Activity, "Please Wait....\n We are working for your request...", -1, MaskType.Clear);

                mWebView.SetWebViewClient(new MyWebViewClientLike());



                mWebView.LoadUrl("https://www.facebook.com/Emahendras/");

            }

            return v;

            
        }

        public void getMyLike()
        {
            try
            {
                mWebView.Settings.JavaScriptEnabled = true;

              

                mWebView.SetWebViewClient(new MyWebViewClientLike());



                mWebView.LoadUrl("https://www.facebook.com/Emahendras/");

                if (like.Length <=0)
                {
                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("LikeInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("MyLike", "https://www.facebook.com/Emahendras/");

                    edit.Apply();
                }
                Intent intent = new Intent(Activity, typeof(LikeFragment));
                Activity.StartActivity(intent);
            }
            catch
            {

            }
        }
    }
    public class MyWebViewClientLike : WebViewClient
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

            AndHUD.Shared.Dismiss();
        }

        public override void OnReceivedError(WebView view, ClientError errorCode, string description, string failingUrl)
        {
            base.OnReceivedError(view, errorCode, description, failingUrl);
        }
    }
}