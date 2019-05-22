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
    public class AllTestModel
    {
        [JsonProperty(PropertyName = "Data")]
        public string Data { get; set; }
    }

    public class AllTestModelData
    {
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "Duration")]
        public int Duration { get; set; }

        [JsonProperty(PropertyName = "Subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "Packages")]
        public string Packages { get; set; }

        [JsonProperty(PropertyName = "TotalQues")]
        public int TotalQues { get; set; }

        [JsonProperty(PropertyName = "TotalMarks")]
        public int TotalMarks { get; set; }

        [JsonProperty(PropertyName = "MaxAttempts")]
        public int MaxAttempts { get; set; }

        [JsonProperty(PropertyName = "TestDate")]
        public string TestDate { get; set; }

        [JsonProperty(PropertyName = "TestType")]
        public string TestType { get;set; }

        [JsonProperty(PropertyName = "TestID")]
        public int TestID { get; set; }

        
        public string Text { get; set; }

        public int background { get; set; }
    }
}