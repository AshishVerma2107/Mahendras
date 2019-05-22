using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;

using Android.OS;

using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using ImageSlider.Connection;
using ImageSlider.Model;
using ImageSlider.MyTest;
using Newtonsoft.Json;
using Refit;

namespace ImageSlider
{
    [Activity(Label = "Registration_Activity")]
    public class Registration_Activity : Activity
    {
        InternetConnection con;
        DBHelper dba;
        ServiceHelper restService;
        ProgressDialog progress;
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        TextInputEditText M_obile, pass_word, confrirm_password, Dateofbirth, Gender;
        TextView U_Name, email_add;
        string other_user, other_mobile, other_email, other_pass, otherconfirm_pass;
        Button Registration;
        string loginid;
        CustomProgressDialog cpd;
        protected override void OnCreate(Bundle savedInstanceState)
        {


            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            StrictMode.ThreadPolicy.Builder builder1 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder1.Build());

            base.OnCreate(savedInstanceState);

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            con = new InternetConnection();
            dba = new DBHelper();
            restService = new ServiceHelper();
            SetContentView(Resource.Layout.Registration_Layout);
            other_email = Intent.GetStringExtra("email");
            other_user = Intent.GetStringExtra("username");
            pref = GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();
            U_Name = FindViewById<TextView>(Resource.Id.user);
            M_obile = FindViewById<TextInputEditText>(Resource.Id.mobile);
            email_add = FindViewById<TextView>(Resource.Id.email);
            pass_word = FindViewById<TextInputEditText>(Resource.Id.pass);
            confrirm_password = FindViewById<TextInputEditText>(Resource.Id.confirmpassword);
            Registration = FindViewById<Button>(Resource.Id.submit);
            U_Name.Text = other_user;
            email_add.Text = other_email;
            Dateofbirth = FindViewById<TextInputEditText>(Resource.Id.birth);
            Gender = FindViewById<TextInputEditText>(Resource.Id.gender);



            //U_Name.Text = GoogleAuth_Activity.user_Name;
            //U_Name.SetCursorVisible(false);
            //U_Name.SetFadingEdgeLength(10);
            //U_Name.Enabled = false;


            //email_add.Text = GoogleAuth_Activity.alreadyregistered;

            //email_add.SetCursorVisible(false);
            //email_add.SetFadingEdgeLength(10);
            //email_add.Enabled = false;


            Dateofbirth.Click += DateSelect_OnClick; ;



            Registration.Click += async delegate
            {
                other_user = U_Name.Text.ToString();
                other_mobile = M_obile.Text.ToString();
                other_email = email_add.Text.ToString();
                other_pass = pass_word.Text.ToString();
                otherconfirm_pass = confrirm_password.Text.ToString();
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMiniMaxChars = new Regex(@".{6,25}");
                var hasLowerChar = new Regex(@"[a-z]+");
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
                if (other_user.Equals(""))
                {
                    Toast.MakeText(this, "Please Enter UserName", ToastLength.Short).Show();
                    return;
                }
                if (other_mobile.Equals("") || other_mobile.Length < 10)
                {
                    Toast.MakeText(this, "Please Enter 10 digit Mobile Number", ToastLength.Short).Show();
                    return;
                }
                if (other_email.Equals("") || !other_email.Contains("@"))
                {
                    Toast.MakeText(this, "Please Enter valid email address", ToastLength.Short).Show();
                    return;
                }
                if (other_pass.Equals("") || !hasNumber.IsMatch(other_pass) || !hasUpperChar.IsMatch(other_pass) || !hasMiniMaxChars.IsMatch(other_pass) || !hasLowerChar.IsMatch(other_pass) || !hasSymbols.IsMatch(other_pass))
                {
                    Toast.MakeText(this, "Password Should have minimum 6 character, minumum one special char, one uppercase letter,one lowercase letter", ToastLength.Short).Show();
                    return;
                }
                if (other_mobile.Length < 6)
                {
                    Toast.MakeText(this, "Please Enter minimum 6 digit password", ToastLength.Short).Show();
                    return;
                }
                if (!other_pass.Equals(otherconfirm_pass))
                {
                    Toast.MakeText(this, "Password and confirm password should be same", ToastLength.Short).Show();
                    return;
                }

                //if (con.connectivity())
                //{
                //    RegistrationData();
                //}

                else
                {
                    cpd = new CustomProgressDialog(this);
                    cpd.SetCancelable(false);
                    cpd.Show();
                    DoRegistration(other_user, other_pass, other_mobile, other_email);
                   

                }

            };


        }
        async void DoRegistration(string username, string pasword, string mobilenumber, string emailadress)
        {
            //string hash = new LoginModel().GetHash(pasword, username);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            LoginModel objmodel = new LoginModel
            {
                Username = emailadress,
                Password = pasword,
                Mobile = mobilenumber,
                Email = emailadress,
                DisplayName = username,

                LoginType = "stportal",
                ts = timestamp

            };
            objmodel.hash = objmodel.GetHash();
            string test = JsonConvert.SerializeObject(objmodel);
            string response = "";
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
                response = await restService.PostServiceMethodRegistration(objmodel).ConfigureAwait(false);
            }
            catch (Exception we)
            {
                cpd.DismissDialog();
                //var errresponse = (HttpWebResponse)we.Response;
                //using (StreamReader errreader = new StreamReader(errresponse.GetResponseStream()))
                //{
                //    responseString = errreader.ReadToEnd();
                //}
                ////Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }

            if (response.StartsWith("Error"))
            {
                cpd.DismissDialog();
                Toast.MakeText(this, response, ToastLength.Long).Show();
                // login failed
            }
            else
            {
                try
                {
                    cpd.DismissDialog();
                    STPUserInfo userinfo = new STPUserInfo();

                    userinfo = JsonConvert.DeserializeObject<STPUserInfo>(response);


                    if (response.Contains("UserAlreadyRegistered"))
                    {
                        //var registerintent = new Intent(this, typeof(MainActivity));

                        //StartActivityForResult(registerintent, 123);

                        //Toast.MakeText(this, "Welcome " + userinfo.DisplayName, ToastLength.Long).Show();

                    }

                    else
                    {



                        STPUserInfo userinfo2 = new STPUserInfo();

                        userinfo = JsonConvert.DeserializeObject<STPUserInfo>(response);
                        edit.Clear();
                        // login suceeded, parse json
                        edit.PutString("username", userinfo.DisplayName);
                        edit.PutString("profilePath", userinfo.ImageURL);
                        edit.PutInt("userid", userinfo.UserID);
                        edit.PutBoolean("pluscard", userinfo.PlusCard);
                        edit.PutBoolean("banned", userinfo.Banned);

                        edit.Apply();

                        Finish();
                        Toast.MakeText(this, "Welcome " + userinfo.DisplayName, ToastLength.Long).Show();



                      //  var registerintent = new Intent(this, typeof(MainActivity));

                       // StartActivityForResult(registerintent, 123);

                    }





                }
                catch (Exception e)
                {
                    cpd.DismissDialog();
                    Toast.MakeText(this, e.Message, ToastLength.Long).Show();

                }


            }



        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                Dateofbirth.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 106)
            {
                Finish();
            }

        }


    }

    public class DatePickerFragment : DialogFragment,
        DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.  
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
        // Initialize this value to prevent NullReferenceExceptions.  
        Action<DateTime> _dateSelectedHandler = delegate { };
        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month, currently.Day);
            return dialog;
        }
        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!  
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}
