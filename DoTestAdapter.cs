using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace ImageSlider.MyTest
{
    class DoTestAdapter : FragmentPagerAdapter
    {
        List<List<questionmodel>> questionlist = new List<List<questionmodel>>();
        List<questionpassagemodel> passagelist = new List<questionpassagemodel>();
        string path;
        public DoTestAdapter(FragmentManager fm, List<List<questionmodel>> questionlist, List<questionpassagemodel> passagelist ,string path) : base(fm)
        {
            this.questionlist = questionlist;
            this.passagelist = passagelist;
            this.path = path;
        }
        public override int Count
        {
            get { return questionlist.Count; }
        }

        public override Fragment GetItem(int position)
        {

          
            return DosTestFragment.NewInstance(JsonConvert.SerializeObject(questionlist[position]),JsonConvert.SerializeObject(passagelist),path,position+1,0,0,0,"","",false,0,false,"","","","",0,"",false);
        }
    
    }
}