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
    public class JoinTelegramFragment : Fragment
    {
        WebView mWebView;
        JoinTelegramAPI telegramapi;
        string jointelegram = "";
        Android.App.ProgressDialog progress;
        string join_Telegram;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("TelegramInfo", FileCreationMode.Private);

            join_Telegram = pref.GetString("joincontent", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.JoinTelegramLayout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webViewjoin);


            telegramapi = RestService.For<JoinTelegramAPI>("http://mg.mahendras.org");

            if (join_Telegram.Length <= 0)
            {
                getJoinTelegram();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientJoinTelegram(Activity));

                mWebView.LoadData("<html>" + join_Telegram + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getJoinTelegram()
        {
            progress.Show();

            try
            {

                JoinTelegramModel response = await telegramapi.GetTelegramList();

                ResDataJoinTelegram resddat = response.res_data;

                jointelegram = resddat.content;

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientJoinTelegram(Activity));

                mWebView.LoadData("<html>" + jointelegram + "</html>", "text/html", "utf-8");



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                progress.Dismiss();

                if (join_Telegram.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("TelegramInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("joincontent", jointelegram);

                    edit.Apply();
                }


               // Intent intent = new Intent(Activity, typeof(JoinTelegramFragment));
               // Activity.StartActivity(intent);

            }
            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientJoinTelegram : WebViewClient
    {
        Android.App.Activity ac;
        public MyWebViewClientJoinTelegram(Android.App.Activity ac)
        {
            this.ac = ac;
        }
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {

            //view.LoadUrl(url);

            var uri = Android.Net.Uri.Parse(url);
            var intent = new Intent(Intent.ActionView, uri);
            ac.StartActivity(intent);
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