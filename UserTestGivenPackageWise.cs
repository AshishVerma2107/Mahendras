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
using Newtonsoft.Json;
namespace ImageSlider.Model
{
    public class UserTestGivenPackageWise
    {

        [JsonProperty(PropertyName = "Data")]
        public string Data { get; set; }
    }

    public class UserTestGivenPackageWiseRecord
    {
        [JsonProperty(PropertyName = "UserID")]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "PackageID")]
        public int PackageID { get; set; }

        [JsonProperty(PropertyName = "ExamGiven")]
        public int ExamGiven { get; set; }

        [JsonProperty(PropertyName = "PracticalGiven")]
        public int PracticalGiven { get; set; }
    }
}