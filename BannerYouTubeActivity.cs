//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Webkit;
//using Android.Widget;

//namespace ImageSlider
//{
//    [Activity(Label = "BannerYouTubeActivity")]
//    public class BannerYouTubeActivity : Activity
//    {

//        WebView mWebView;
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);

//            SetContentView(Resource.Layout.Youtube_Layout);

//            mWebView = FindViewById<WebView>(Resource.Id.youtube);



//            mWebView.Settings.JavaScriptEnabled = true;

//            mWebView.SetWebViewClient(new MyWebViewClientyouTube());

//            mWebView.LoadUrl("https://www.youtube.com");

//        }
//    }
//}