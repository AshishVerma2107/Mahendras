using System;
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using ImageSlider.Model;
using Android.Webkit;
using Refit;
using Android.Content;
using ImageSlider.API_Interface;
using Android.Widget;
using System.Threading;

namespace ImageSlider.Fragments
{
    public class Facilities_Fragment : Fragment
    {
        WebView mWebView;
        Facilities_API facilitiesapi;

        string Faci_content = "";
        Android.App.ProgressDialog progress;
        string facility;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("FacilitiesInfo", FileCreationMode.Private);

            facility = pref.GetString("facilitycontent", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Facility_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webView_faci);


            facilitiesapi = RestService.For<Facilities_API>("http://mg.mahendras.org");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000); // 10000 ms
           

            if (facility.Length <= 0)
            {
                getFacilities();
            }
            else
            {

                getFacilities();

                //mWebView.Settings.JavaScriptEnabled = true;

                //mWebView.SetWebViewClient(new MyWebViewClientFacilities());

                //mWebView.LoadData("<html>" + facility + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getFacilities()
        {
            progress.Show();

            try
            {

                FacilitiesModel response = await facilitiesapi.GetFacilitiesList();

                ResDataFacilities resfaci = response.res_data;
                Faci_content = resfaci.content;

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientFacilities());

                mWebView.LoadData("<html>" + Faci_content + "</html>", "text/html", "utf-8");



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                progress.Dismiss();

                if (facility.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("FacilitiesInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("facilitycontent", Faci_content);

                    edit.Apply();
                }


              //  Intent intent = new Intent(Activity, typeof(Facilities_Fragment));
               // Activity.StartActivity(intent);

            }
            catch (Exception e)
            {
                progress.Dismiss();
                Toast.MakeText(Activity, "" + e.Message, ToastLength.Short).Show();
            }


        }
    }

    public class MyWebViewClientFacilities : WebViewClient
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