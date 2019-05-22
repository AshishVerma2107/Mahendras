using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
namespace ImageSlider.MyClassSchedule
{
    [Activity(Label = "MyClassSchedule", Theme = "@style/MyTheme")]
    public class MyClassSchedule : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyClassSchedule);
            ViewPager viewpager = FindViewById<ViewPager>(Resource.Id.viewpager);

            SetupViewPager(viewpager);

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewpager);

            // Create your application here
        }

        void SetupViewPager(ViewPager viewpager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new TodayClassSchedule(), "Today");
            adapter.AddFragment(new TommorowClassSchedule(), "Tommorow");
            adapter.AddFragment(new NextClassSchedule(), "Next");
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



    }
}