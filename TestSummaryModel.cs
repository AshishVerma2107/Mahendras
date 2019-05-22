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

namespace ImageSlider.MyTest
{
    public class TestSummaryModel
    {

        [JsonProperty(PropertyName = "ErrCode")]
        public int ErrCode { get; set; }

        [JsonProperty(PropertyName = "ErrText")]
        public string ErrText { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public string Data { get; set; }

    }

    public class TestSummaryDataModel
    {
        [JsonProperty(PropertyName = "TestType")]
        public string TestType { get; set; }

        [JsonProperty(PropertyName = "Rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "TotalQuestion")]
        public int TotalQuestion { get; set; }

        [JsonProperty(PropertyName = "Answered")]
        public int Answered { get; set; }

        [JsonProperty(PropertyName = "NotAnswered")]
        public int NotAnswered { get; set; }

        [JsonProperty(PropertyName = "NotVisited")]
        public int NotVisited { get; set; }

        [JsonProperty(PropertyName = "Correct")]
        public int Correct { get; set; }

        [JsonProperty(PropertyName = "Marks")]
        public float Marks { get; set; }


    }
}