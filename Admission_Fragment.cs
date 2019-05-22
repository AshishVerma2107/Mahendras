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
    public class Admission_Fragment : Fragment
    {
        WebView mWebView;
        Admission_API  admissionapi;
        string contentadmission = "";
        Android.App.ProgressDialog progress;
        string Admission;
        List<AdmissionModel> finalAdmission;

        List<string> Admission_List = new List<string>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("AdmissionInfo", FileCreationMode.Private);

            Admission = pref.GetString("Admissioncontent", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Admission, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webViewadmisson);


            admissionapi = RestService.For<Admission_API>("http://mg.mahendras.org");

            if (Admission.Length <= 0)
            {
                getAdmissionData();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientAdmission());

                mWebView.LoadData("<html>" + Admission + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getAdmissionData()
        {
            progress.Show();

            try
            {

                AdmissionModel response = await admissionapi.GetCareerList();

                List<ResDataAdmission> resddat = response.res_data;
                Admission = resddat[0].content_text + resddat[0].content_title;



                //finalAdmission = new List<AdmissionModel>();


                //for (int i = 0; i < resddat.Count; i++)
                //{
                //    Admission_List.Add(resddat[i].content_id);
                //    Admission_List.Add(resddat[i].content_text);
                //    Admission_List.Add(resddat[i].content_title);
                //    Admission_List.Add(resddat[i].content_date);

                   

                //}


                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientAdmission());

                mWebView.LoadData("<html>" + Admission + "</html>", "text/html", "utf-8");



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                progress.Dismiss();

                if (Admission.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("AdmissionInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Admissioncontent", Admission);

                    edit.Apply();
                }


                Intent intent = new Intent(Activity, typeof(Admission_Fragment));
                Activity.StartActivity(intent);

            }
            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientAdmission : WebViewClient
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