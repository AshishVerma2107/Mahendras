using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace ImageSlider.MyTest
{
    [Activity(Label = "TestSummary")]
    public class TestSummary : Activity
    {
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        List<List<questionmodel>> Allquestionlist;
        List<questionpassagemodel> passagelist;
        List<int> startingquestionposition;
        string path;
        List<string> items;
        LinearLayout llranklayout;
        TextView txtrankid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TestSummary);
            path = Intent.GetStringExtra("path");
            if (!path.Equals("portal"))
            {
                try
                {
                    Allquestionlist = JsonConvert.DeserializeObject<List<List<questionmodel>>>(DosTestFragment.myquestionlist);
                }
                catch (Exception)
                {
                    Allquestionlist = JsonConvert.DeserializeObject<List<List<questionmodel>>>(DoOnlineTestFragment.question);
                }
            }
            // passagelist = JsonConvert.DeserializeObject<List<questionpassagemodel>>(Intent.GetStringExtra("passage"));
            try
            {
               
               
               
                if (path.Equals("portal"))
                {
                    llranklayout = FindViewById<LinearLayout>(Resource.Id.ranklayout);
                    llranklayout.Visibility = ViewStates.Visible;
                    txtrankid = FindViewById<TextView>(Resource.Id.rankid);
                    string listuserrecord = Intent.GetStringExtra("testsummarylist");
                    mRecycleView = FindViewById<RecyclerView>(Resource.Id.testsummarylist);
                    mLayoutManager = new LinearLayoutManager(this);
                    mRecycleView.SetLayoutManager(mLayoutManager);
                    SummaryAdapter mAdapter = new SummaryAdapter(this, listuserrecord, mRecycleView,"portal",txtrankid);
                    mRecycleView.SetAdapter(mAdapter);
                }
                else
                {
                    llranklayout = FindViewById<LinearLayout>(Resource.Id.ranklayout);
                    llranklayout.Visibility = ViewStates.Gone;
                    items = JsonConvert.DeserializeObject<List<string>>(Intent.GetStringExtra("item"));
                    startingquestionposition = JsonConvert.DeserializeObject<List<int>>(Intent.GetStringExtra("startingquestionposition"));
                    mRecycleView = FindViewById<RecyclerView>(Resource.Id.testsummarylist);
                    mLayoutManager = new LinearLayoutManager(this);
                    mRecycleView.SetLayoutManager(mLayoutManager);
                    SummaryAdapter mAdapter = new SummaryAdapter(this, Allquestionlist, items, startingquestionposition, mRecycleView,"local");
                    mRecycleView.SetAdapter(mAdapter);
                }
            }
            catch (Exception)
            {

            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                Finish();
                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
                return true;
            }

            return true;
        }
    }
}