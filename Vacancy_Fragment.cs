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
using Android.App;
using Android.Support.V7.Widget;
using Android.Support.V7.App;

namespace ImageSlider.Fragments
{
    [Activity(Label = "Mahendras", Theme = "@style/AppTheme")]
    public class Vacancy_Fragment : AppCompatActivity
    {
        WebView mWebView;
        List<Vacancy_API> vacancyList;
        Vacancy_API vacancy_ApItitle;

        string vacancy_content = "";

        Android.App.ProgressDialog progress;

        string vacancy;
        List<Vacancy_Model> myvacancyList = new List<Vacancy_Model>();
        string title, text;
        int position = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Vacancy_Layout);
            Toolbar  toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            // SetSupportActionBar(toolbar);
            //View v = inflater.Inflate(Resource.Layout.Vacancy_Layout, container, false);
            position = Intent.GetIntExtra("position",0);
           // text = Intent.GetStringExtra("text");
            mWebView = FindViewById<WebView>(Resource.Id.webViewvacancy);

           // vacancy_ApI = RestService.For<Vacancy_API>("http://mg.mahendras.org");
            getVacancyList();
            //if (vacancy.Length <= 0)
            //{
            //    getVacancyList();
            //}
            //else
            //{

            //    mWebView.Settings.JavaScriptEnabled = true;

            //    mWebView.SetWebViewClient(new MyWebViewClient(Activity));

            //    mWebView.LoadData("<html>" + vacancy + "</html>", "text/html", "utf-8");
            //}


          
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                Finish();
            }
            return false;
        }


        private async void getVacancyList()
        {
           // progress.Show();

            try
            {

              

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientAboutUs());

                mWebView.LoadData("<html>" + VacancyFragment.myvacancyList[position].content_title + VacancyFragment.myvacancyList[position].content_text + "</html>", "text/html", "utf-8");
                
               

               

              

            }
            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientVacancy : WebViewClient
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