using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Newtonsoft.Json;
using Refit;
using ImageSlider.Connection;
using ImageSlider.Model;
using Android.Support.V7.Widget;

namespace ImageSlider.MyTest
{
    [Activity(Label = "Test Instruction",Theme ="@style/MyTheme")]
    public class TestInstruction : AppCompatActivity,View.IOnClickListener
    {
        int mycounter = 0;
        Toolbar toolbar;
        Button StartTest;
        int Testid;
        public static Activity activity;
        CustomProgressDialog cp;
        TestDataRecord testDataRecord;
        public static List<TestInfoListRecord> testInfoList;
        TextView txtlanguage, txtnoofquestion, txttime, txttotalmarks;
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        int testduration;
        public  List<string> items = new List<string>();
        public  List<int> subjectid = new List<int>();
        public  List<int> startingquestionposition = new List<int>();
        public  List<int> subjecttotalquestion = new List<int>();
        Spinner Spnlanguage;
        string langcode;
        string testtype;
        TextView txtTestname;
        List<string> languagelist = new List<string>();
        List<string> languagecodelist = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TestInstruction);
            activity = this;
            Spnlanguage = FindViewById<Spinner>(Resource.Id.testlanguage);
           
           
            txtnoofquestion = FindViewById<TextView>(Resource.Id.noofquestion);
            txttime = FindViewById<TextView>(Resource.Id.questiontime);
            txttotalmarks = FindViewById<TextView>(Resource.Id.totalmarks);
            txtTestname = FindViewById<TextView>(Resource.Id.testname);
            Testid = Intent.GetIntExtra("TestID",0);
            testduration = Intent.GetIntExtra("TestDuration",0);
            testtype = Intent.GetStringExtra("testtype");
            string testname = Intent.GetStringExtra("TestName");
            txtTestname.Text = testname;
            StartTest = FindViewById<Button>(Resource.Id.startest);
            StartTest.StartAnimation(AnimationUtils.LoadAnimation(this,Resource.Animation.shake));
            StartTest.SetOnClickListener(this);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //For showing back button  
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetHomeButtonEnabled(true);
            toolbar.SetTitle(Resource.String.MyRequest);
            cp = new CustomProgressDialog(this);
            cp.Show();
            if (Utility.IsNetworkConnected(this))
            {
                CallApi();
            }
            else
            {
                cp.Dismiss();
                Toast.MakeText(this, "Check your internet connection", ToastLength.Short).Show();
            }
        }

        private void Spnlanguage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            langcode = languagecodelist[e.Position];
        }

        async void CallApi()
        {
            var testInfoApi = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
            var testInfoResponse = await testInfoApi.GetSTInfo(Testid + "");
            testDataRecord = JsonConvert.DeserializeObject<TestDataRecord>(testInfoResponse.Data);
            string language = testDataRecord.Languages;
            string[] languacodearray = language.Split(",");
            for (int i = 0; i < languacodearray.Count(); i++)
            {
                languagecodelist.Add(languacodearray[i]);
                languagelist.Add(languacodearray[i].ToUpper());
            }
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, languagelist);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Spnlanguage.Adapter = adapter;
            Spnlanguage.ItemSelected += Spnlanguage_ItemSelected;
            try
            {
                testInfoList = testDataRecord.SubPattern;
            }
            catch (Exception)
            {
                Intent resultIntent = new Intent();
                resultIntent.PutExtra("serilizeObj", MyTest.SerilizeString);
                resultIntent.PutExtra("testtype", testtype);
                SetResult(Result.Ok, resultIntent);

                Finish();
                //MyTest.activity.Finish();
                //var intent1 = new Intent(this, typeof(MyTest));
                //intent1.PutExtra("serilizeObj", MyTest.SerilizeString);
                //StartActivity(intent1);
                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
                return;
            }

            
            startingquestionposition.Add(mycounter);
            testInfoList.Sort((x, y) => x.SeqNo.CompareTo(y.SeqNo));
            for (int i = 0; i < testInfoList.Count; i++)
            {
                testInfoList[i].Duration = testInfoList[i].Duration*60*1000;
                //testInfoList[i].Duration = 20 * 1000;

                if (i != 0)
                {
                    mycounter = mycounter + testInfoList[i-1].TotalQuestion;
                    startingquestionposition.Add(mycounter);

                }
                items.Add(testInfoList[i].SubjectTitle);
                subjectid.Add(testInfoList[i].SubjectID);
                subjecttotalquestion.Add(testInfoList[i].TotalQuestion);
                


            }
          //  txtlanguage.Text = testDataRecord.Languages;
            txtnoofquestion.Text = testDataRecord.TotalQuestions + "";
            txttime.Text = testDataRecord.Duration + "";
            txttotalmarks.Text = testDataRecord.TotalMarks + "";
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.sectionalpatternlist);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            TestInstructionAdapter mAdapter = new TestInstructionAdapter(this, mRecycleView, testInfoList);
            mRecycleView.SetAdapter(mAdapter);
            cp.Dismiss();

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:


                    Finish();
                    OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {

                Intent resultIntent = new Intent();
                resultIntent.PutExtra("serilizeObj", MyTest.SerilizeString);
                resultIntent.PutExtra("testtype", testtype);
                SetResult(Result.Ok, resultIntent);
               
                Finish();
                //MyTest.activity.Finish();
                //var intent1 = new Intent(this, typeof(MyTest));
                //intent1.PutExtra("serilizeObj", MyTest.SerilizeString);
                //StartActivity(intent1);
                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
                return true;
            }

            return true;
        }

        public void OnClick(View v)
        {
            var intent = new Intent(this, typeof(DoTest));
            intent.PutExtra("TestID", Testid);
            intent.PutExtra("negativemarks", testInfoList[0].NegativeMarks);
            intent.PutExtra("totaltime", testduration);
            intent.PutExtra("items", JsonConvert.SerializeObject(items));
            intent.PutExtra("langcode", langcode);
            intent.PutExtra("startingquestionposition", JsonConvert.SerializeObject(startingquestionposition));
            intent.PutExtra("subjecttotalquestion", JsonConvert.SerializeObject(subjecttotalquestion));
            intent.PutExtra("testtype", testtype);
            StartActivityForResult(intent,101);
            OverridePendingTransition(Resource.Animation.slide_up1,Resource.Animation.hold);
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