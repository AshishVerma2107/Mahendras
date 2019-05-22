using System;
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using ImageSlider.Model;
using Android.Webkit;
using Refit;
using Android.Content;

namespace ImageSlider.Fragments
{
    public class AboutUsFragment : Fragment
    {
        WebView mWebView;
        AboutUsAPI aboutusapi;
        string content1 = "";
        Android.App.ProgressDialog progress;
        string about_us;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("AboutUsInfo", FileCreationMode.Private);

            about_us = pref.GetString("content", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.AboutUs_Layout, container, false);

             mWebView = v.FindViewById<WebView>(Resource.Id.webView1);


            aboutusapi = RestService.For<AboutUsAPI>("http://mg.mahendras.org");

            if (about_us.Length <= 0)
            {
                getAboutExam();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientAboutUs());

                mWebView.LoadData("<html>" + about_us + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getAboutExam()
        {
            progress.Show();

            try
            {

                AboutUsModel response = await aboutusapi.GetAboutExamList();

                ResData resddat =     response.res_data;
                content1 = resddat.content;

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientAboutUs());

                mWebView.LoadData("<html>" + content1 + "</html>", "text/html", "utf-8");



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                progress.Dismiss();

                if (about_us.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("AboutUsInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("content", content1);

                    edit.Apply();
                }


                Intent intent = new Intent(Activity, typeof(AboutUsFragment));
                Activity.StartActivity(intent);

            }
            catch (Exception e)
            {

            }


        }
    }
   
    public class MyWebViewClientAboutUs : WebViewClient
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