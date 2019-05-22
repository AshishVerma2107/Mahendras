using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace ImageSlider.MyTest
{
    class InstructionCustomDialog : Dialog
    {
        Activity context;
        string instructiontitle, instruction;
        ItestInstructionCallBack testinstructioncallback;
        
        public InstructionCustomDialog(Activity ac, string instructiontitle, string instruction,ItestInstructionCallBack testinstructioncallback) : base(ac)
        {
            this.instructiontitle = instructiontitle;
            this.instruction = instruction;
            context = ac;
            SetCanceledOnTouchOutside(false);
            this.testinstructioncallback = testinstructioncallback;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Instruction);
            this.Window.Attributes.WindowAnimations = Resource.Style.DialogAnimation;
            TextView txttitle = (TextView)FindViewById(Resource.Id.instructiontitle);
            TextView cancel = (TextView)FindViewById(Resource.Id.cancel);
            TextView submit = (TextView)FindViewById(Resource.Id.instructionok);
            WebView webinstruction = (WebView)FindViewById(Resource.Id.testinstruction);
            txttitle.Text = instructiontitle;
            webinstruction.SetBackgroundColor(Color.Transparent);
            webinstruction.LoadData(instruction, "text/html",null);
            submit.Click += Submit_Click;
            cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            context.Finish();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
           
            Dismiss();
            testinstructioncallback.TestinstructionCallBack();
        }
    }
}