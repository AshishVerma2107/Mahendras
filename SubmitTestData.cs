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
    class SubmitTestData
    {
        public int TestID { get; set; }
        public int userid { get; set; }
        public string deflanguage { get; set; }
        public List<UserResponse> UserResponseData { get; set; }
    }
}