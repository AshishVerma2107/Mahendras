using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ImageSlider.Connection;
using ImageSlider.Model;
using Newtonsoft.Json;

using Refit;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace ImageSlider.MyTest
{
    [Activity(Label = "MyTest" ,Theme ="@style/MyTheme" )]
    public class MyTest : AppCompatActivity,View.IOnClickListener
    {
        TextView txtOnlinetest, txtMockTest;
        Toolbar toolbar;
        CustomProgressDialog cp;
        public static string SerilizeString;
        UserPackageModel objPackageMOdel;
        List<AllTestModelData> MyFinalTestListMock;
        List<AllTestModelData> MyFinalTestList;
        public static string question;
        public static Activity activity;
        public static string myFinalTestlistserilize;
        public static string myFinalMOckTestlistserilize;
        List<AllTestModelData> AllTestlist;
        List<AllTestModelData> AllTestlistMock;
        List<AllTestModelData> GivenTestlist;
        string packageid, packageidMock;
        string testtype = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyTest);
            activity = this;
            SerilizeString = Intent.GetStringExtra("serilizeObj");
            objPackageMOdel = JsonConvert.DeserializeObject<UserPackageModel>(SerilizeString);
            Utility.intalizejson();
            txtOnlinetest = FindViewById<TextView>(Resource.Id.tabonlinetest);
            txtMockTest = FindViewById<TextView>(Resource.Id.tabmocktest);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //For showing back button  
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            toolbar.SetTitle(Resource.String.MyRequest);
            //SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testlistfragment,new DoOnlineTestFragment()).Commit();

            txtOnlinetest.SetOnClickListener(this);
            txtMockTest.SetOnClickListener(this);
            
            cp = new CustomProgressDialog(this);
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


        }
      
        async void Callapi()
        {
            //========================================Fetch All Test List=======================================//
            var testapi = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
            var testresponse = await testapi.Activetestlist();
            AllTestlist = JsonConvert.DeserializeObject<List<AllTestModelData>>(testresponse.Data);
            packageid = objPackageMOdel.PackageID + "";
         


            var testresponseMock = await testapi.ActivetestlistMock();
            AllTestlistMock = JsonConvert.DeserializeObject<List<AllTestModelData>>(testresponseMock.Data);
            packageidMock = objPackageMOdel.PackageID + "";
          


            //===================================================================================================//

          

            //=====================================Fetch Given Test Record=======================================//
            var giventestapi = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
            var giventestresponse = await giventestapi.GetGivenTest(MainActivity1.userid+"",objPackageMOdel.ValidUptoDays+"");
            GivenTestlist = JsonConvert.DeserializeObject<List<AllTestModelData>>(giventestresponse.Data);
            PrepareRecord();

           


        }
       public  void PrepareRecord()
        {
            //================================remove those test which not belonged to loogedin user=============//
            MyFinalTestList = new List<AllTestModelData>();
            for (int i = 0; i < AllTestlist.Count; i++)
            {
                if (AllTestlist[i].Packages.Equals(packageid))
                {
                    MyFinalTestList.Add(AllTestlist[i]);
                }
                else
                {
                    string[] pacjagearray = AllTestlist[i].Packages.Split(",");
                    for (int y = 0; y < pacjagearray.Length; y++)
                    {
                        if (packageid.Equals(pacjagearray[y]))
                        {
                            MyFinalTestList.Add(AllTestlist[i]);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            MyFinalTestListMock = new List<AllTestModelData>();
            for (int i = 0; i < AllTestlistMock.Count; i++)
            {

                if (AllTestlistMock[i].Packages.Equals(packageidMock))
                {
                    MyFinalTestListMock.Add(AllTestlistMock[i]);
                }
                else
                {
                    string[] pacjagearray = AllTestlistMock[i].Packages.Split(",");
                    for (int y = 0; y < pacjagearray.Length; y++)
                    {
                        if (packageidMock.Equals(pacjagearray[y]))
                        {
                            MyFinalTestListMock.Add(AllTestlistMock[i]);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
              
            }
           

            List<AllTestModelData> GivenExamTest = new List<AllTestModelData>();
            List<AllTestModelData> GivenPracticalTest = new List<AllTestModelData>();

            try
            {
                for (int i = 0; i < GivenTestlist.Count; i++)
                {
                    if (GivenTestlist[i].TestType.Equals("Exam"))
                    {
                        GivenExamTest.Add(GivenTestlist[i]);
                    }
                    else
                    {
                        GivenPracticalTest.Add(GivenTestlist[i]);
                    }
                }
            }
            catch (Exception)
            {

            }

            for (int i = 0; i < MyFinalTestList.Count; i++)
            {
                ISharedPreferences pref;
                ISharedPreferencesEditor edit;
                pref = GetSharedPreferences(MyFinalTestList[i].ID + "", FileCreationMode.Private);
                edit = pref.Edit();
                string question = pref.GetString("TestRecord", String.Empty);
                if (question.Length > 0)
                {
                    MyFinalTestList[i].Text = "Resume";
                    MyFinalTestList[i].background = Resource.Drawable.greenRectangle;
                }
                else
                {
                    MyFinalTestList[i].Text = "Start Test";
                    MyFinalTestList[i].background = Resource.Drawable.mytestTextview;
                }
                for (int y = 0; y < GivenExamTest.Count; y++)
                {

                    if (MyFinalTestList[i].ID == GivenExamTest[y].TestID)
                    {
                        MyFinalTestList[i].Text = "Taken";
                        MyFinalTestList[i].background = Resource.Drawable.myorangetextview;
                    }

                    else
                    {

                        continue;
                    }
                }
            }

            for (int i = 0; i < MyFinalTestListMock.Count; i++)
            {
                ISharedPreferences pref;
                ISharedPreferencesEditor edit;
                pref = GetSharedPreferences(MyFinalTestListMock[i].ID + "", FileCreationMode.Private);
                edit = pref.Edit();
                string question = pref.GetString("TestRecord", String.Empty);
                if (question.Length > 0)
                {
                    MyFinalTestListMock[i].Text = "Resume";
                    MyFinalTestListMock[i].background = Resource.Drawable.greenRectangle;
                }
                else
                {
                    MyFinalTestListMock[i].Text = "Start Test";
                    MyFinalTestListMock[i].background = Resource.Drawable.mytestTextview;
                }
                for (int y = 0; y < GivenPracticalTest.Count; y++)
                {

                    if (MyFinalTestListMock[i].ID == GivenPracticalTest[y].TestID)
                    {
                        MyFinalTestListMock[i].Text = "Taken";
                        MyFinalTestListMock[i].background = Resource.Drawable.myorangetextview;
                    }

                    else
                    {

                        continue;
                    }
                }
            }

            //===================================================================================================//
            txtOnlinetest.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
            txtOnlinetest.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));

            txtMockTest.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
            txtMockTest.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));

            myFinalTestlistserilize = JsonConvert.SerializeObject(MyFinalTestList);
            myFinalMOckTestlistserilize = JsonConvert.SerializeObject(MyFinalTestListMock);
            cp.Dismiss();
            DoOnlineTestFragment obj = new DoOnlineTestFragment();
            Bundle bundle = new Bundle();
            // bundle.PutString("AllTestList", myFinalTestlistserilize);
            obj.Arguments = bundle;
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testlistfragment, obj).Commit();
            //==================================================================================================//
        }
        void SetupViewPager(ViewPager viewpager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new DoOnlineTestFragment(), "Online Speed Test");
            adapter.AddFragment(new DoMockTestFragment(), "Mock Test");
            viewpager.Adapter = adapter;
            viewpager.Adapter.NotifyDataSetChanged();
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
       
            public void OnClick(View v)
        {

            if (v.Id == Resource.Id.tabonlinetest)
            {
                txtOnlinetest.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
                txtOnlinetest.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));

                txtMockTest.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                txtMockTest.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                var myFinalTestlistserilize = JsonConvert.SerializeObject(MyFinalTestList);
                cp.Dismiss();
                DoOnlineTestFragment obj = new DoOnlineTestFragment();
               // Bundle bundle = new Bundle();
               // bundle.PutString("AllTestList", myFinalTestlistserilize);
               // obj.Arguments = bundle;
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testlistfragment, obj).Commit();
            }
            else
            {
                txtOnlinetest.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                txtOnlinetest.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));

                txtMockTest.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
                txtMockTest.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                var myFinalTestlistserilize = JsonConvert.SerializeObject(MyFinalTestListMock);
                cp.Dismiss();
                DoMockTestFragment obj = new DoMockTestFragment();
               // Bundle bundle = new Bundle();
                //bundle.PutString("AllTestList", myFinalTestlistserilize);
                //obj.Arguments = bundle;
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testlistfragment, obj).Commit();
            }

            
        }

        class Adapter : Android.Support.V4.App.FragmentPagerAdapter
        {
            List<Fragment> fragments = new List<Fragment>();
            List<string> fragmentTitles = new List<string>();
            public Adapter(FragmentManager fm) : base(fm) { }
            public void AddFragment(Fragment fragment, String title)
            {
                fragments.Add(fragment);
                fragmentTitles.Add(title);
            }
            public override Fragment GetItem(int position)
            {
                return fragments[position];
            }
            public override int Count
            {
                get
                {
                    return fragments.Count;
                }
            }
            public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(fragmentTitles[position]);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 101)
            {
                //testtype = data.GetStringExtra("testtype");
                PrepareRecord();
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {

        }


    }
}