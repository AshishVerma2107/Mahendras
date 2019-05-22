using System;
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using ImageSlider.Model;
using Android.Webkit;
using Refit;
using Android.Content;
using ImageSlider.API_Interface;

namespace ImageSlider.Fragments
{
    public class JoinUs_Fragment : Fragment
    {
        WebView mWebView;
        JoinUs_API joinusapi;
        string JoinUscontent = "";
        Android.App.ProgressDialog progress;
        string Join_us;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("JoinUsInfo", FileCreationMode.Private);

            Join_us = pref.GetString("Joincontent", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.JoinUs_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webViewjoin);


            joinusapi = RestService.For<JoinUs_API>("http://mg.mahendras.org");

            if (Join_us.Length <= 0)
            {
                getJoinUs();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientJoin());

                mWebView.LoadData("<html>" + Join_us + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getJoinUs()
        {
            progress.Show();

            try
            {

                JoinUsModel response = await joinusapi.GetJoinUsList();

                ResDataJoinUs resddat = response.res_data;
                JoinUscontent = resddat.content;

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientJoin());

                mWebView.LoadData("<html>" + JoinUscontent + "</html>", "text/html", "utf-8");



                

                progress.Dismiss();

                if (Join_us.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("JoinUsInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Joincontent", JoinUscontent);

                    edit.Apply();
                }


                Intent intent = new Intent(Activity, typeof(JoinUs_Fragment));
                Activity.StartActivity(intent);

            }
            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientJoin : WebViewClient
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