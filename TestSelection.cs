using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ImageSlider.Connection;
using ImageSlider.Model;
using Refit;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using Android.Support.V4.Content;

namespace ImageSlider.MyTest
{
    [Activity(Label = "My Test", Theme ="@style/MyTheme")]
    public class TestSelection : AppCompatActivity
    {
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        
        readonly String[] CatagoryName = { "*Plus*", "1_SG_ALL_IN_ONE", "ALL IN ONE ST MEMBERSHIP", "AP POLICE", "BANKPO_CLERK", "BANK SSC RLY", "BIHAR POLICE", "CCC", "CDP", "CG POLICE", "CG VYAPAM","COMPUTER CONCEPT","CWS","DELHI POLICE","HIGH COURT" };
        readonly int[] colorcode = { Resource.Color.mytest, Resource.Color.myselection, Resource.Color.noticeboard, Resource.Color.myschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.stresult, Resource.Color.bookmica, Resource.Color.speedtestschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.mytest, Resource.Color.myselection, Resource.Color.noticeboard, Resource.Color.myschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.stresult, Resource.Color.bookmica, Resource.Color.speedtestschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.mytest, Resource.Color.myselection, Resource.Color.noticeboard, Resource.Color.myschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.stresult, Resource.Color.bookmica, Resource.Color.speedtestschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.mytest, Resource.Color.myselection, Resource.Color.noticeboard, Resource.Color.myschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC, Resource.Color.stresult, Resource.Color.bookmica, Resource.Color.speedtestschedule, Resource.Color.mymembership, Resource.Color.myrequest, Resource.Color.Requetlab, Resource.Color.RequestSSC };
        readonly int[] imagename = { Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest, Resource.Drawable.plustest };
        Toolbar toolbar;
        List<STCourseRecord> StCourseList = new List<STCourseRecord>();
        List<UserPackageModel> listresponse, listresponseforuser;
        CustomProgressDialog cp;
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        bool banned;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TestSelection);
            pref = GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();
            banned = pref.GetBoolean("banned", false);


            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            //{
            //    if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage))
            //    {

            //    }
            //    else
            //    {
            //        ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage }, 101);
            //    }
            //}
            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            //{
            //    if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.ReadExternalStorage))
            //    {

            //    }
            //    else
            //    {
            //        ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadExternalStorage }, 102);
            //    }
            //}


            Utility.intalizejson();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
        
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //For showing back button  
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            toolbar.SetTitle(Resource.String.MyRequest);
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.mytestselectionlist);
            mLayoutManager = new GridLayoutManager(this, 3);
            mRecycleView.SetLayoutManager(mLayoutManager);
            cp = new CustomProgressDialog(this);
            cp.SetCancelable(false);
            cp.Show();

            if (Utility.IsNetworkConnected(this))
            {
                Callapi();
            }
            else
            {
                cp.Dismiss();
                Toast.MakeText(this, "Check your internet connection", ToastLength.Short).Show();
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage))
                {

                }
                else
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage }, 101);
                }
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.ReadExternalStorage))
                {

                }
                else
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadExternalStorage }, 102);
                }
            }

            // Create your application here
        }


        async void Callapi()
        {
            try
            {
                var apiresponse = RestService.For<ApiConnection>(Utility.mibsapiBaseUrl);
                UserStPackageModel response = await apiresponse.GetOnlineSTCoursesr();
                listresponse = JsonConvert.DeserializeObject<List<UserPackageModel>>(response.Data);

                var apiresponseforuser = RestService.For<ApiConnection>(Utility.mibsapiBaseUrl);
                UserStPackageModel responseforuser = await apiresponse.GetUserSTPackages(MainActivity1.userid + "");
                listresponseforuser = JsonConvert.DeserializeObject<List<UserPackageModel>>(responseforuser.Data);
                List<UserTestPostObject> mylist = new List<UserTestPostObject>();
                for (int i = 0; i < listresponseforuser.Count; i++)
                {
                    UserPackageModel userpackagemodel = listresponseforuser[i];
                    string iDate = userpackagemodel.StartsFrom;
                    DateTime oDate = Convert.ToDateTime(iDate);
                    UserTestPostObject obj = new UserTestPostObject
                    {
                        UserId = MainActivity1.userid,
                        PackageID = userpackagemodel.PackageID,
                        StartsFrom = oDate,
                    };
                    mylist.Add(obj);
                }
                string mytem = JsonConvert.SerializeObject(mylist);
                var apiresponseforgiventestpackagewise = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
                var responseforgiventestpackagewise = await apiresponseforgiventestpackagewise.Getusertestgivenpackagewise(mylist);
                var listforgiventestpackagewise = JsonConvert.DeserializeObject<List<UserTestGivenPackageWiseRecord>>(responseforgiventestpackagewise.Data);
                //if (listforgiventestpackagewise.Count > 0)
                //{
                //    var l1 = listforgiventestpackagewise.Count;
                //    var l2 = listresponseforuser.Count;
                //    if (l1 > l2)
                //        l1 = l2;
                //    for (int i = 0; i < listresponseforuser.Count; i++)
                //    {
                       
                //        listresponseforuser[i].ExamGiven = listforgiventestpackagewise[i].ExamGiven;
                //        try
                //        {
                //            listresponseforuser[i].PracticalGiven = listforgiventestpackagewise[i].PracticalGiven;
                //        }
                //        catch (Exception)
                //        {
                //            listresponseforuser[i].ExamGiven = listforgiventestpackagewise[i].ExamGiven;
                //        }
                //    }
                //}


                TestSelectionAdapter mAdapter = new TestSelectionAdapter(listresponse, listresponseforuser, colorcode, imagename, this, mRecycleView);
                mAdapter.ItemClick += MAdapter_ItemClick;
                mRecycleView.SetAdapter(mAdapter);
                cp.DismissDialog();
                try
                {

                    listresponseforuser.Add(TestSelectionAdapter.finallist[listresponseforuser.Count]);
                }
                catch (Exception e)
                {
                   // Toast.MakeText(this, "mahendras-->"+e.Message, ToastLength.Short).Show();
                }
            }
            catch (Exception e)
            {
                cp.DismissDialog();
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }
           
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            int photoNum = e + 1;
            UserPackageModel obj = null;
            if (!banned)
            {
                if (TestSelectionAdapter.finallist[e].disable_enable == 1)
                {
                   
                    for (int i = 0; i < listresponseforuser.Count; i++)
                    {
                        if (TestSelectionAdapter.finallist[e].CourseName.Equals(listresponseforuser[i].CourseName))
                        {
                            obj = listresponseforuser[i];
                            break;
                        }
                    }
                   
                    var serilizeObj = JsonConvert.SerializeObject(obj);
                    var intent = new Intent(this, typeof(MyTest));
                    intent.PutExtra("serilizeObj", serilizeObj);
                    StartActivityForResult(intent, 102);
                    OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);
                }
                else
                {

                    if (listresponseforuser.Count == 0)
                    {

                        Toast.MakeText(this, "You are not subscribed to any service. Kindly attach OTP into ST Portal Account for activation of services.", ToastLength.Short).Show();
                        var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "You have not subscribed this package, Let us walk myshop.org", ToastLength.Short).Show();
                        var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);

                    }
                   
                }
            }
            else
            {
                Toast.MakeText(this, "Your candidature is banned/expired, for unban kindly contact to branch", ToastLength.Short).Show();
            }
           
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:


                    Finish();
                    OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_down);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                Finish();
                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_down);
                return true;
            }

            return true;
        }
    }
}