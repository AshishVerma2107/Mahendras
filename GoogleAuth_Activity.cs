using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Util;
using Android.Graphics;
using System.Net;
using System;
using Android.Content;

using Java.IO;
using Newtonsoft.Json;
using ImageSlider.Model;
using Android.Gms.Auth.Api;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Android.Content.PM;

namespace ImageSlider
{
    [Activity(Label = "GoogleAuth_Activity")]
    public class GoogleAuth_Activity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleApiClient mGoogleApiClient;
        private ConnectionResult mConnectionResult;
        // SignInButton mGsignBtn;
        Button mGsignBtn;
        TextView TxtName, TxtEmail;
        ImageView ImgProfile;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;
        ServiceHelper restService;

        ISharedPreferences pref;
        ISharedPreferencesEditor edit;

        public static string user_Name;
        public static string email_address;

        public static string alreadyregistered;


        public string TAG { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            restService = new ServiceHelper();

            SetContentView(Resource.Layout.GoogleAuth_Layout);
            pref = GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();

            // mGsignBtn = FindViewById<SignInButton>(Resource.Id.sign_in_button);

            mGsignBtn = FindViewById<Button>(Resource.Id.sign_in_button);
            TxtEmail = FindViewById<TextView>(Resource.Id.TxtGender);
            TxtName = FindViewById<TextView>(Resource.Id.TxtName);
            ImgProfile = FindViewById<ImageView>(Resource.Id.ImgProfile);
            Button Submit = FindViewById<Button>(Resource.Id.submit);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.GetAccounts) != (int)Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.GetAccounts))
                {



                }

                else
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.GetAccounts }, 115);
                }
            }


            mGsignBtn.Click += MGsignBtn_Click;
            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);
            //Build our IGoogleApiClient
            mGoogleApiClient = builder.Build();
        }

        private void MGsignBtn_Click(object sender, EventArgs e)
        {
            if (!mGoogleApiClient.IsConnecting)
            {
                mSignInClicked = true;
                ResolveSignInError();

            }
            else if (mGoogleApiClient.IsConnected)
            {


                PlusClass.AccountApi.ClearDefaultAccount(mGoogleApiClient);
                mGoogleApiClient.Disconnect();

            }
        }
        private void ResolveSignInError()
        {
            if (mGoogleApiClient.IsConnecting)
            {
                return;
            }
            if (mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }
                catch (Android.Content.IntentSender.SendIntentException io)
                {
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }

            }
        }
        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }


        public void OnConnected(Bundle connectionHint)
        {
            string emailAddress;
            try
            {

                 emailAddress = PlusClass.AccountApi.GetAccountName(mGoogleApiClient);
                
            }

            catch (Exception e)
            {

                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }

            var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
          //  DoAuthentication(emailAddress);





        }

        async void DoAuthentication(string email, string password)
        {

           // string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
           

            STPLoginSocial model = new STPLoginSocial
            {
                Username = email,
                Password = password,

            };

            model.hash = model.GetHashforSocial("");
            
            string response = "";
            try
            {
                response = await restService.ServiceAuthentication(model).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }

            try
            {
                STPReg userinfo = new STPReg();

               // userinfo = JsonConvert.DeserializeObject<STPReg>(response);

                if(response.Contains("ERROR"))
                {
                    userinfo.Username = email;
                    alreadyregistered = userinfo.Username;

                    var socialintent = new Intent(this, typeof(Registration_Activity));

                    StartActivityForResult(socialintent, 125);

                }

                else
                {

                    STPUserInfo userinfo2 = new STPUserInfo();

                    userinfo2 = JsonConvert.DeserializeObject<STPUserInfo>(response);
                    edit.Clear();
                    // login suceeded, parse json
                    edit.PutString("username", userinfo2.DisplayName);
                    edit.PutString("profilePath", userinfo2.ImageURL);
                    edit.PutInt("userid", userinfo2.UserID);

                    edit.Apply();

                    Finish();
                    Toast.MakeText(this, "Welcome " + userinfo.DisplayName, ToastLength.Long).Show();
                    
                    var registerintent = new Intent(this, typeof(MainActivity));

                    StartActivityForResult(registerintent, 123);

               

                }

              

               

                

               



               // Toast.MakeText(this, "Welcome " + userinfo.Username, ToastLength.Long).Show();
            }


            catch (Exception)
                {

                }


        }


        private Bitmap GetImageBitmapFromUrl(String url)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

                return imageBitmap;
            }
            catch (IOException e)
            {
                // Log.Debuge(TAG, "Error getting bitmap", e);
            }
            return null;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);

            if (requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    mSignInClicked = false;
                }
                mIntentInProgress = false;

                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }

            }
        }
        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                //Store the ConnectionResult so that we can use it later when the user clicks 'sign-in;
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    //The user has already clicked 'sign-in' so we attempt to resolve all
                    //errors until the user is signed in, or the cancel
                    ResolveSignInError();
                }
            }
        }

        public void OnConnectionSuspended(int cause)
        {

        }

    }

   

}




