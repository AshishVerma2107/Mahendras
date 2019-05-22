using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace ImageSlider
{
    public class Dashboard : Fragment
    {
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        readonly String[] CatagoryName = {"My Test","My Result & Selection", "Notice Board","My Class Schedule","My Membership","My Request","Request Seat(Lab)","Request Seat(SSC)","ST Results","Book MICA","Speed Test Schedule"};
        readonly int[] colorcode = { Resource.Color.mytest,Resource.Color.myselection,Resource.Color.noticeboard,Resource.Color.myschedule,Resource.Color.mymembership,Resource.Color.myrequest,Resource.Color.Requetlab,Resource.Color.RequestSSC,Resource.Color.stresult,Resource.Color.bookmica,Resource.Color.speedtestschedule };
        readonly int[] imagename = { Resource.Drawable.MyTests ,Resource.Drawable.MyResultAndSolution,Resource.Drawable.NoticeBoard,Resource.Drawable.MYCLASSSCHEDULE,Resource.Drawable.CWS,Resource.Drawable.MYREQUEST,Resource.Drawable.ReserveSeat,Resource.Drawable.RESERVESEATSSC,Resource.Drawable.STResult,Resource.Drawable.BOOKMICA,Resource.Drawable.STSchedule};
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
             View v = inflater.Inflate(Resource.Layout.dashboard, container, false);
            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.dashboardlist);
            mLayoutManager = new GridLayoutManager(this.Activity,2);
            mRecycleView.SetLayoutManager(mLayoutManager);
            DashboardAdapter  mAdapter = new DashboardAdapter(CatagoryName,colorcode,imagename,this.Activity,mRecycleView);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            return v;
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            int photoNum = e + 1;

            if (e == 0)
            {


                ISharedPreferences pref;
                ISharedPreferencesEditor edit;
                pref = Activity.GetSharedPreferences("login", FileCreationMode.Private);
                edit = pref.Edit();
                string username = pref.GetString("username", String.Empty);
                if (username.Length > 0)
                {
                    var intent = new Intent(Activity, typeof(MyTest.TestSelection));
                    StartActivityForResult(intent, 102);
                    Activity.OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);
                }
                else
                {
                    //var intent = new Intent(Activity, typeof(MyTest.TestSelection));
                    //StartActivityForResult(intent, 102);
                    //Activity.OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);
                    var taskIntentActivity2 = new Intent(Activity, typeof(LoginActivity_STPortal));
                    Activity.StartActivityForResult(taskIntentActivity2, 402);
                }




            }
            else {
                Toast.MakeText(Activity, "Working for you..., go to stportal.mahendras.org", ToastLength.Short).Show();
                var uri = Android.Net.Uri.Parse("https://stportal.mahendras.org/");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            }
        }



       
    }
   
}