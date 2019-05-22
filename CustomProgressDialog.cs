using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ImageSlider.MyTest
{
    class CustomProgressDialog : Dialog
    {
        Activity context;
        private static ProgressBar circularbar;
        private static Button BtnStart;
        private int progressStatus = 0, progressStatus1 = 100;
        public CustomProgressDialog(Activity activity) : base(activity)
        {
            context = activity;
            this.SetCanceledOnTouchOutside(false);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.progressbar);
            circularbar = FindViewById<ProgressBar>(Resource.Id.circularProgressbar);
            circularbar.Max = 100;
            circularbar.Progress = 100;
            circularbar.SecondaryProgress = 100;
            new Thread(new ThreadStart(delegate {
                while (true)
                {
                    progressStatus += 1;
                    progressStatus1 -= 1;
                    circularbar.Progress = progressStatus1;
                    Thread.Sleep(10);
                }
            })).Start();

        }
        public void DismissDialog()
        {
            Dismiss();
        }
    }
}