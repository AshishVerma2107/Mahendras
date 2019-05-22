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
    public class STPReg
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        public string Email { get; set; }  // either email or mobile     
        public string Mobile { get; set; } // one must be provided     
        public string LoginType { get; set; }       // stportal, google, facebook     
        public string hash { get; set; }
        public string ts { get; set; }  // timestamp as yyyyMMddHHmmss } 

    }
}