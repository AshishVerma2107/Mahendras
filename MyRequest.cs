using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Support.V7.App;

namespace ImageSlider
{
    [Activity(Label = "MyResult", Theme = "@style/MyTheme")]
    public class MyRequest : AppCompatActivity
    {
        Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.myrequest);



            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //For showing back button  
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            toolbar.SetTitle(Resource.String.MyRequest);
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
    }

  
}