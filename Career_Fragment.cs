using System;
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using ImageSlider.Model;
using Android.Webkit;
using Refit;
using Android.Content;
using ImageSlider.API_Interface;
using System.Collections.Generic;

namespace ImageSlider.Fragments
{
    public class Career_Fragment : Fragment
    {
        WebView mWebView;
        CareerAPI careerapi;
        string content1 = "";
        Android.App.ProgressDialog progress;
        string Career;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("CareerInfo", FileCreationMode.Private);

            Career = pref.GetString("content_text", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Career_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webViewcareer);


            careerapi = RestService.For<CareerAPI>("http://mg.mahendras.org");

            if (Career.Length <= 0)
            {
                getCareerData();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientCareer());

                mWebView.LoadData("<html>" + Career + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getCareerData()
        {
            progress.Show();

            try
            {

                CareerModel response = await careerapi.GetCareerList();

                List<ResDataCareer> resddat = response.res_data;
                Career = resddat[0].content_text;

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientCareer());

                mWebView.LoadData("<html>" + Career + "</html>", "text/html", "utf-8");



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                progress.Dismiss();

                if (Career.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("CareerInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("content_text", Career);

                    edit.Apply();
                }


                Intent intent = new Intent(Activity, typeof(Career_Fragment));
                Activity.StartActivity(intent);

            }
            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientCareer : WebViewClient
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