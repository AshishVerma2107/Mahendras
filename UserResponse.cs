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
    class UserResponse
    {

        public int ID { get; set; }
        public int TestID { get; set; }
        public int QID { get; set; }
        public int UserID { get; set; }
        public int TimeTaken { get; set; }
        
        public bool IsCorrect { get; set; }
        public float Marks { get; set; }
        public string Response { get; set; }
        public bool MarkForReview { get; set; }
        
    }
}