
using Android.Support.V4.App;

using System;
using Android.OS;
using Android.Views;
using ImageSlider.Model;
using Android.Webkit;
using Refit;
using Android.Content;

namespace ImageSlider.Fragments
{
    public class InviteFragment : Fragment
    {
        Android.App.ProgressDialog progress;
        string inviteFriends;
        string LoadURL;
        WebView mWebView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");


            ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("InviteFriends", FileCreationMode.Private);

            inviteFriends = pref.GetString("invitefriend", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View v = inflater.Inflate(Resource.Layout.Invite_Layout, container, false);

             mWebView = v.FindViewById<WebView>(Resource.Id.invitefriends);

            mWebView.Settings.JavaScriptEnabled = true;

            mWebView.SetWebViewClient(new MyWebViewClientInvite());


            string appId = "com.makemedroid.keye5bb63c9";

             LoadURL = "https://play.google.com/store/apps/details?id=" + appId;

            //  mWebView.LoadUrl("https://play.google.com/store/apps/details?id="+ appId);

            if (inviteFriends.Length <= 0)
            {
                Invite_Friends();
            }
            else
            {

                mWebView.Settings.JavaScriptEnabled = true;

                mWebView.SetWebViewClient(new MyWebViewClient(Activity));

                mWebView.LoadUrl(LoadURL);
            }


           


            return v;
        }

        private async void Invite_Friends()
        {
            progress.Show();

            try
            {



                mWebView.LoadUrl(LoadURL);





                progress.Dismiss();

                if (inviteFriends.Length <= 0)
                {

                    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("InviteFriends", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("invitefriend", inviteFriends);

                    edit.Apply();
                }


                Intent intent = new Intent(Activity, typeof(ContactUsFragment));
                Activity.StartActivity(intent);

            }



            catch (Exception e)
            {

            }


        }
    }

    public class MyWebViewClientInvite : WebViewClient
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
