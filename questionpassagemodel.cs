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
    public class questionpassagemodel
    {
        public int id { get; set; }
        private string Langcode { get; set; }
        private int GroupId { get; set; }
        private string Title { get; set; }
        public string Passage { get; set; }
        private int TopicId { get; set; }
        private int CretedBy { get; set; }
        private string CreatedOn { get; set; }
        private int UpdatedBy { get; set; }
        private string updatedOn { get; set; }
        
        
                
    }
}