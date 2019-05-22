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

namespace ImageSlider.Fragments
{
    public class RateAppFragment : Fragment
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
            View v = inflater.Inflate(Resource.Layout.RateApp_Layout, container, false);

            WebView mWebView = v.FindViewById<WebView>(Resource.Id.rateapp);

            mWebView.Settings.JavaScriptEnabled = true;

            mWebView.SetWebViewClient(new MyWebViewClientInvite());


            string appId = "com.makemedroid.keye5bb63c9";
            mWebView.LoadUrl($"https://play.google.com/store/apps/details?id=com.makemedroid.keye5bb63c9" + appId);

            return v;
        }
    }
    public class MyWebViewClientRateApp : WebViewClient
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