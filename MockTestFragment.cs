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

namespace ImageSlider
{
    public class MockTestFragment : Fragment
    {
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        List<MockTestBean> myList = new List<MockTestBean>();
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
             View v = inflater.Inflate(Resource.Layout.MockTestFragment, container, false);
            MockTestBean objbean = new MockTestBean
            {
                Id = "1",
                Coursename = "ST-544 SSCHSL(PRE) 2019",
                Date = "2019-04-10",
                Attempted = "7",
                Correct = "3",
                Obtaind = "4",
                Result = "N/A",
                Totalmarks = "100",
                Course = "testttttt"
            };

            for (int i = 0; i < 10; i++)
            {
                myList.Add(objbean);
            }

            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.mocktestlist);
            mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecycleView.SetLayoutManager(mLayoutManager);
            MockTestAdapter mAdapter = new MockTestAdapter(myList, this.Activity,mRecycleView);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            return v;
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            int photoNum = e + 1;
            Toast.MakeText(this.Activity, "This is item number " + photoNum, ToastLength.Short).Show();

        }
    }
}