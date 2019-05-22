using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using ImageSlider.Model;
using ImageSlider.MyTest;
using Newtonsoft.Json;

namespace ImageSlider
{
    [Activity(Label = "Login")]
    public class LoginActivity_STPortal : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleApiClient mGoogleApiClient;
        private ConnectionResult mConnectionResult;
        EditText user, pass;
        Button log, register;
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        ServiceHelper restService;
        InternetConnection ic;
        ProgressDialog progress;
        string licenceid;
        LoginModel detail;
        CustomProgressDialog cpd;
        SignInButton mGsignBtn;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            StrictMode.VmPolicy.Builder builder1 = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder1.Build());
            StrictMode.ThreadPolicy.Builder builder2 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder2.Build());
            base.OnCreate(savedInstanceState);

            restService = new ServiceHelper();

            SetContentView(Resource.Layout.LoginLayout);
          
            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);
            //Build our IGoogleApiClient  
            mGoogleApiClient = builder.Build();
          
            // mGoogleApiClient.Disconnect();

            mGsignBtn = FindViewById<SignInButton>(Resource.Id.sign_in_button);
            TextView txtenglish = FindViewById<TextView>(Resource.Id.english);
            TextView txthindi = FindViewById<TextView>(Resource.Id.hindi);
            txtenglish.SetText(Resource.String.stportal);
            txthindi.SetText(Resource.String.stportalhindi);
            pref = GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();
            user = FindViewById<EditText>(Resource.Id.username);
            pass = FindViewById<EditText>(Resource.Id.pass);
            log = FindViewById<Button>(Resource.Id.login);
            register = FindViewById<Button>(Resource.Id.regis);

            log.Click += delegate
            {
                UserLogin();
            };

            register.Click += delegate
            {
                var intent6 = new Intent(this, typeof(Registration_Activity));
                StartActivityForResult(intent6, 106);
            };
            mGsignBtn.Click += delegate
            {

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
                else
                {

                    // DoAuthentication("deepanshu.misra@gmail.com", "", "Deepanshu Mishra");
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


              
            };



        }

        public void UserLogin()
        {

            if (Utility.IsNetworkConnected(this))
            {
                Validate();
            }
            else
            {

                Toast.MakeText(this, "Check your internet connection", ToastLength.Short).Show();
            }


        }

        public void Validate()
        {

            var errorMsg = "";
            if (user.Text.Length == 0 && pass.Text.Length == 0)
            {
                if (user.Text.Length == 0 || pass.Text.Length == 0)
                {
                    errorMsg = "Please Enter User Name ";


                }
                if (pass.Text.Length == 0 || pass.Text.Length == 0)
                {
                    errorMsg = errorMsg + "Please Enter Password";
                }

                Toast.MakeText(this, errorMsg, ToastLength.Long).Show();
                return;
            }
            else
            {

                cpd = new CustomProgressDialog(this);
                cpd.SetCancelable(false);
                cpd.Show();
                DoLogin(user.Text, pass.Text);
            }
        }

        async void DoLogin(string username, string pasword)
        {
            cpd.Show();
            LoginModel model = new LoginModel();
            model.Username = username;
            model.Password = pasword;
            string ret = "";
            try
            {

                var thread = new System.Threading.Thread(new ThreadStart(async delegate
                {
                    cpd.Show();
                }));
                thread.Start();

                while (thread.ThreadState == ThreadState.Running)
                {
                    await Task.Delay(100);
                }
                ret = await restService.PostServiceMethod(model).ConfigureAwait(false);
                if (ret.StartsWith("Error"))
                {
                    cpd.DismissDialog();
                    if (ret.Contains("403"))
                    {
                        Toast.MakeText(this, "Invalis Username/Password", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, ret, ToastLength.Long).Show();
                    }
                    
                    // login failed
                }
                else
                {
                    try
                    {
                        STPUserInfo userinfo = new STPUserInfo();

                        userinfo = JsonConvert.DeserializeObject<STPUserInfo>(ret);
                        edit.Clear();
                        // login suceeded, parse json
                        edit.PutString("username", userinfo.DisplayName);
                        edit.PutInt("userid", userinfo.UserID);
                        edit.PutString("profilePath", userinfo.ImageURL);
                        edit.PutBoolean("pluscard", userinfo.PlusCard);
                        edit.PutBoolean("banned", userinfo.Banned);
                        edit.Apply();
                        cpd.DismissDialog();
                        Finish();
                        Toast.MakeText(this, "Welcome " + userinfo.DisplayName, ToastLength.Long).Show();
                    }
                    catch (Exception e)
                    {
                        cpd.DismissDialog();
                        Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception e)
            {
                cpd.DismissDialog();

                if (e.Message.Contains("403"))
                {
                    Toast.MakeText(this, "Invalis Username/Password", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, ret, ToastLength.Long).Show();
                }
               
            }


        }

        async Task ParseAndDisplay(JsonValue json, String login_Id)
        {


            LoginModel model = new LoginModel();
            if (detail.Username != null && detail.Password != "")
            {
                try
                {
                    string isRegistered = await restService.PostServiceMethod(model).ConfigureAwait(false);

                    progress.Dismiss();

                    if (isRegistered.Contains("Success"))
                    {



                        Intent intent = new Intent(this, typeof(MainActivity));
                        intent.AddFlags(ActivityFlags.NewTask);
                        StartActivity(intent);
                        Finish();
                    }
                    else
                    {
                        progress.Dismiss();
                        Toast.MakeText(ApplicationContext, "Try after some time", ToastLength.Short).Show();
                    }
                }
                catch (Exception ex)
                {
                    progress.Dismiss();
                    Toast.MakeText(ApplicationContext, "Try after some time", ToastLength.Short).Show();
                }
            }
            else
            {
                progress.Dismiss();
                Toast.MakeText(ApplicationContext, "Invalid User name or Password", ToastLength.Short).Show();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 106||requestCode==125)
            {
                Finish();
            }
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
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (grantResults.Length > 0
                  && grantResults[0] == Permission.Granted)
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
            else
            {
                Toast.MakeText(this, "Please give get contact permission from App Setting for gmail login ", ToastLength.Long).Show();
                // permission denied, boo! Disable the
                // functionality that depends on this permission.
            }
            return;
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
        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                mConnectionResult = result;
                if (mSignInClicked)
                {
                    ResolveSignInError();
                }
            }
        }
        private void ResolveSignInError()
        {
            try
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
            catch (Exception)
            {
            }
        }
        public void OnConnected(Bundle connectionHint)
        {
            string emailAddress="";
            try
            {

                 emailAddress = PlusClass.AccountApi.GetAccountName(mGoogleApiClient);
                 var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
                // Toast.MakeText(this, emailAddress + "  " + person.DisplayName, ToastLength.Long).Show();
                //CustomProgressDialog cpd = new CustomProgressDialog(this);
                //cpd.SetCancelable(false);
                //cpd.Show();
                // cpd.DismissDialog();
                //var thread = new System.Threading.Thread(new ThreadStart(async delegate
                //{
                //    cpd.Show();
                //}));
                //thread.Start();

                //while (thread.ThreadState == ThreadState.Running)
                //{
                //     Task.Delay(100);
                //}
                DoAuthentication(emailAddress, "", person.DisplayName);

            }

            catch (Exception e)
            {

                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }

          
           
        }

        public void OnConnectionSuspended(int cause) { }


        async void DoAuthentication(string email, string password,string username)
        {

            // string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            string myts = DateTime.Now.ToString("yyyyMMddHHmmss");
            STPLoginSocial model = new STPLoginSocial
            {
                Username = email,
                Password = password,
                ts = myts
            };
          
            model.hash = model.GetHashforSocial(myts);
           
            string response = "";
            try
            {
                response = await restService.ServiceAuthentication(model).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //cpd.DismissDialog();
                mGoogleApiClient.Disconnect();
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }
            mGoogleApiClient.Disconnect();
            Toast.MakeText(this, response, ToastLength.Long).Show();
            try
            {
                STPReg userinfo = new STPReg();

                 userinfo = JsonConvert.DeserializeObject<STPReg>(response);

                if (response.Contains("ERROR"))
                {
                    //cpd.DismissDialog();
                  
                    var socialintent = new Intent(this, typeof(Registration_Activity));
                    socialintent.PutExtra("email", email);
                    socialintent.PutExtra("username", username);
                    StartActivityForResult(socialintent, 125);

                }

                else
                {
                    //cpd.DismissDialog();
                    STPUserInfo userinfo2 = new STPUserInfo();

                    userinfo2 = JsonConvert.DeserializeObject<STPUserInfo>(response);
                    edit.Clear();
                    // login suceeded, parse json
                    edit.PutString("username", userinfo2.DisplayName);
                    edit.PutInt("userid", userinfo2.UserID);
                    edit.PutString("profilePath", userinfo2.ImageURL);
                    edit.PutBoolean("pluscard", userinfo2.PlusCard);
                    edit.PutBoolean("banned", userinfo2.Banned);
                    edit.Apply();
                    //cpd.DismissDialog();
                   
                    Finish();
                    Toast.MakeText(this, "Welcome " + userinfo.DisplayName, ToastLength.Long).Show();

                }

            }

            catch (Exception)
            {
                //cpd.DismissDialog();
            }


        }
    }


}
