using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using ImageSlider.Fragments;

namespace ImageSlider
{
    [Activity(Label = "NoticeBoardDetail_Activity")]
    public class NoticeBoardDetail_Activity : Activity
    {
        TextView txtdate;
        WebView mywebview;
        int position = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NoticeBoardDetailsLayout);
            position = Intent.GetIntExtra("position",0);
            txtdate = FindViewById<TextView>(Resource.Id.dateId);
            txtdate.Text = NoticeBoardFinal.noticeList[position].content_date;
            mywebview = FindViewById<WebView>(Resource.Id.noticeboardwebview);
            mywebview.LoadData(NoticeBoardFinal.noticeList[position].content_text, "text/html", null);
            // Create your application here
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