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
    public class JoinClassRoom_Fragment : Fragment
    {
        WebView mWebView;
        List<ClassRoom_API> classroomList;
        ClassRoom_API classroom_ApI;

        string classroom_content = "";

        Android.App.ProgressDialog progress;

        string classRoom;
        List<ClassRoom_Model> myclassroomList = new List<ClassRoom_Model>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");

            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("ClassRoomInfo", FileCreationMode.Private);

            classRoom = pref.GetString("classroomcontent", String.Empty);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.ClassRoom_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webViewclassroom);


            classroom_ApI = RestService.For<ClassRoom_API>("http://mg.mahendras.org");

            if (classRoom.Length <= 0)
            {
                getclassroomList();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientClassRoom());

                mWebView.LoadData("<html>" + classRoom + "</html>", "text/html", "utf-8");
            }


            return v;
        }

        private async void getclassroomList()
        {
            progress.Show();

            try
            {

                ResDataClassRoom response = await classroom_ApI.GetClassRoomList();

                myclassroomList = response.res_datavacancy;



                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientClassRoom());

                mWebView.LoadData("<html>" + myclassroomList[0].content_title + myclassroomList[0].content_text + "</html>", "text/html", "utf-8");



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                progress.Dismiss();

                if (classRoom.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("ClassRoomInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("classroomcontent", myclassroomList[0].content_text);

                    edit.Apply();
                }


                Intent intent = new Intent(Activity, typeof(JoinClassRoom_Fragment));
                Activity.StartActivity(intent);

            }
            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientClassRoom : WebViewClient
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