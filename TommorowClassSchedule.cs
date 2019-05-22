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

namespace ImageSlider.MyClassSchedule
{
    public class TommorowClassSchedule : Fragment
    {
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        List<MyResultandSelectionBean> myList = new List<MyResultandSelectionBean>();
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
            View v = inflater.Inflate(Resource.Layout.TommorowClassSchedule, container, false);
            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.tommorowclassschedulelist);
            mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecycleView.SetLayoutManager(mLayoutManager);
            MyClassScheduleAdapter mAdapter = new MyClassScheduleAdapter(myList, this.Activity, mRecycleView);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);

            Spinner spinner = v.FindViewById<Spinner>(Resource.Id.tommorowclassscheduleSpinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            //var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.car_array, Android.Resource.Layout.SimpleSpinnerItem);
            var items = new List<string>() { "one", "two", "three" };
            var adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, items);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            return v;
        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Selected item is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            int photoNum = e + 1;
            Toast.MakeText(this.Activity, "This is item number " + photoNum, ToastLength.Short).Show();

        }
    }
}