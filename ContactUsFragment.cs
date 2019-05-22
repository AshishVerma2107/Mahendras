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
    public class ContactUsFragment : Fragment
    {
        WebView mWebView;
        ContactUsAPI contactusapi;
        string content = "";
        Android.App.ProgressDialog progress;

        string contactus;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");


            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);

             contactus = pref.GetString("contentt", String.Empty);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.ContactUs_Layout, container, false);

            mWebView = v.FindViewById<WebView>(Resource.Id.webcontact);


            contactusapi = RestService.For<ContactUsAPI>("http://mg.mahendras.org");

            if (contactus.Length <= 0)
            {
                getContactUs();

               // Activity.FragmentManager.PopBackStack();
            }
            else {


                getContactUs();

                //mWebView.Settings.JavaScriptEnabled = true;

                //mWebView.SetWebViewClient(new MyWebViewClient(Activity));

                //mWebView.LoadData("<html>" + contactus + "</html>", "text/html", "utf-8");

                // Activity.FragmentManager.PopBackStack();
            }



            

            return v;
        }

        private async void getContactUs()
        {
            progress.Show();

            try
            {

                ContactUsModel response = await contactusapi.GetAboutExamList();

                ResData resddat = response.res_data;
                content = resddat.content;

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClientContactUs());

                mWebView.LoadData("<html>" + content + "</html>", "text/html", "utf-8");





                progress.Dismiss();

                if (contactus.Length <= 0)
                { 
               
                ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("contentt", content);

                edit.Apply();
            }


                Intent intent = new Intent(Activity, typeof(ContactUsFragment));
                Activity.StartActivity(intent);

                //Activity.FragmentManager.PopBackStack();

        }
            


            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientContactUs : WebViewClient
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