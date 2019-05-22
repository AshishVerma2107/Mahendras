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
    public delegate void TickEvent(long millisUntilFinished);
    public delegate void FinishEvent();

    class CountDown : CountDownTimer
    {
        public event TickEvent Tick;
        public event FinishEvent Finish;
        TextView txttimecounter;
        ICountdownInterface countdownInterface;

        public CountDown(ICountdownInterface countdownInterface) : base(0, 0)
        {
            
            this.countdownInterface = countdownInterface;
        }
        public CountDown(long totaltime, long interval,TextView txttimecounter ,ICountdownInterface countdownInterface): base(totaltime, interval)
        {
            this.txttimecounter = txttimecounter;
            this.countdownInterface = countdownInterface;
        }

        public override void OnTick(long millisUntilFinished)
        {
            long minutreaminig = (millisUntilFinished / 1000) / 60;
            long secondsreamaing = (millisUntilFinished / 1000) % 60;
           // long hourremaing = ((millisUntilFinished / (1000 * 60 * 60)) % 24);
           // string stringhour = hourremaing+"";
            string stringminute = minutreaminig+"";
            string stringseconds = secondsreamaing+"";
            //if (stringhour.Length == 1)
            //{
            //    stringhour = "0" + stringhour;

            //}
            //else
            //{
            //    stringhour = stringhour + "";
            //}
            if (stringminute.Length == 1)
            {
                stringminute = "0" + stringminute;
            }
            else
            {
                stringminute = stringminute + "";
            }
            if (stringseconds.Length == 1)
            {
                stringseconds = "0" + stringseconds;
            }
            else
            {
                stringseconds = stringseconds + "";
            }
            txttimecounter.Text = 00+":"+stringminute+":"+stringseconds;
            if (Tick != null)
                Tick(millisUntilFinished);
        }

        public override void OnFinish()
        {
            countdownInterface.Countdowncallback();
            if (Finish != null)
                Finish();
        }
    }
}