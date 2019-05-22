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
    public class questionmodel
    {
       

        public int Id { get; set; }
        public string Correctans { get; set; }
        public int Correctmarks { get ; set ; }
        public string Optionnumbering { get ; set ; }
        public string Qdata { get ; set ; }
        public int Qid { get ; set ; }
        public string Qtype { get ; set; }
        public int Seqno { get ; set ; }
        public int Testqid { get; set; }
        public int Datatype { get ; set ; }
        public int Passageid { get ; set ; }
        public int Subjectid { get ; set ; }
        public int Optnseqno { get ; set ; }
        public int selectedoption { get; set; }
        public int colorcode { get;set; }
        public int textcolor { get; set; }
        public int markforreview { get; set; }
        public int rightorwrongColorCode { get; set; }
        public int rightorwrongTextColor { get; set; }
    }
}