using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using ImageSlider.Model;
using Refit;
using ImageSlider.Connection;

namespace ImageSlider.MyTest
{
    public class DoMockTestFragment : Fragment
    {
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        List<AllTestModelData> AllTestList;
        public static string question;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            View v = inflater.Inflate(Resource.Layout.DoOnlineTestFragment, container, false);
           // var AllTestlistserlizeRecord = Arguments.GetString("AllTestList");

            AllTestList = JsonConvert.DeserializeObject<List<AllTestModelData>>(MyTest.myFinalMOckTestlistserilize);

            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.onlinetestlist);
            mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecycleView.SetLayoutManager(mLayoutManager);
            DoOnlineTestAapter mAdapter = new DoOnlineTestAapter(this.Activity, mRecycleView, AllTestList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);


            return v;
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            int photoNum = e + 1;
            //Toast.MakeText(this.Activity, "This is item number " + photoNum, ToastLength.Short).Show();
            if (!AllTestList[e].Text.Equals("Taken"))
            {
                var intent = new Intent(this.Activity, typeof(TestInstruction));
                intent.PutExtra("TestID", AllTestList[e].ID);
                intent.PutExtra("TestName", AllTestList[e].Title);
                intent.PutExtra("TestDuration", AllTestList[e].Duration);
                intent.PutExtra("testtype", "mock");
                Activity.StartActivityForResult(intent, 101);
                Activity.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
            }
            else
            {


                try
                {
                    ISharedPreferences pref = Activity.GetSharedPreferences(AllTestList[e].ID + "", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();

                    string path = pref.GetString("path", String.Empty);
                    if (path.Length > 0)
                    {
                        FetchSumaryFromServer(AllTestList[e].ID);
                        //string item = pref.GetString("item", String.Empty);
                        //question = pref.GetString("TestRecord", String.Empty);
                        //string startingquestionposition = pref.GetString("startingquestionposition", String.Empty);
                        //var intent = new Intent(Activity, typeof(TestSummary));


                        //intent.PutExtra("path", path);
                        //intent.PutExtra("item", item);
                        //intent.PutExtra("startingquestionposition", startingquestionposition);
                        //Activity.StartActivityForResult(intent, 102);
                        //Activity.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
                    }
                    else
                    {
                        FetchSumaryFromServer(AllTestList[e].ID);
                        //Toast.MakeText(Activity, "You had already given this test", ToastLength.Short).Show();
                    }
                }
                catch (Exception)
                {
                    FetchSumaryFromServer(AllTestList[e].ID);
                    //Toast.MakeText(Activity, "You had already given this test", ToastLength.Short).Show();
                }
                //Toast.MakeText(Activity, "You had already given this test", ToastLength.Short).Show();
            }

        }
        async void FetchSumaryFromServer(int testid)
        {
            CustomProgressDialog cpd = new CustomProgressDialog(Activity);
            cpd.SetCancelable(false);
            cpd.Show();
            var apiresponse = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
            var responseforsummary = await apiresponse.GetTestSummary(MainActivity1.userid + "", testid + "");
            List<TestSummaryDataModel> summarylist = JsonConvert.DeserializeObject<List<TestSummaryDataModel>>(responseforsummary.Data);

            cpd.DismissDialog();
            var intent = new Intent(Activity, typeof(TestSummary));
            intent.PutExtra("path", "portal");
            intent.PutExtra("testsummarylist", responseforsummary.Data);
            Activity.StartActivityForResult(intent, 102);
            Activity.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);

        }
        public override void OnSaveInstanceState(Bundle outState)
        {

        }
    }
}