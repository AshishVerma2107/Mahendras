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

namespace ImageSlider.Model
{
   
        public class STPUserInfo
        {
            public string Username { get; set; }
            public int UserID { get; set; }
            public bool IsAuth { get; set; }
            public string DisplayName { get; set; }
            public bool PlusCard { get; set; }
            public bool Banned { get; set; }
        public string ImageURL { get; set; }


    }
}