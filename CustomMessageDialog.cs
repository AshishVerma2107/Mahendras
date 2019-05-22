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

namespace ImageSlider.MyTest
{
    class CustomMessageDialog : Dialog
    {
        ICustomMessageInterface custommessageinterface;
        public CustomMessageDialog(Activity activity,ICustomMessageInterface custommessageinterface) : base(activity)
        {
            this.custommessageinterface = custommessageinterface;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.customMessageDialog);
            this.Window.Attributes.WindowAnimations = Resource.Style.DialogAnimation;
            TextView cancel = (TextView)FindViewById(Resource.Id.button_cancel);
            TextView submit = (TextView)FindViewById(Resource.Id.submit_button);
            cancel.Click += Cancel_Click;
            submit.Click += Submit_Click;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            Dismiss();
            custommessageinterface.CustomMessageCallBack();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }
}