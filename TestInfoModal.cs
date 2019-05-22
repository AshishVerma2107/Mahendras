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
    public class TestInfoModal
    {
        [JsonProperty("HTTPStatus")]
        public int HTTPStatus { get; set; }

        [JsonProperty("ErrCode")]
        public int ErrCode { get; set; }

        [JsonProperty("ErrText")]
        public string ErrText { get; set; }

        [JsonProperty("Data")]
        public string Data { get; set; }
    }
    public class TestDataRecord
    {
        [JsonProperty("Languages")]
        public string Languages { get; set; }

        [JsonProperty("TotalQuestions")]
        public int TotalQuestions { get; set; }
        
        [JsonProperty("TimesaveInterval")]
        public int TimesaveInterval { get; set; }

        [JsonProperty("TotalMarks")]
        public int TotalMarks { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }

        [JsonProperty("SubPattern")]
        public List<TestInfoListRecord> SubPattern { get; set; }


    }
    public class TestInfoListRecord
    {
        [JsonProperty("SubjectTitle")]
        public string SubjectTitle { get; set; }

        [JsonProperty("SubjectID")]
        public int SubjectID { get; set; }

        [JsonProperty("CorrectMarks")]
        public float CorrectMarks { get; set; }

        [JsonProperty("NegativeMarks")]
        public float NegativeMarks { get; set; }

        [JsonProperty("TotalMarks")]
        public float TotalMarks { get; set; }

        [JsonProperty("TotalQuestion")]
        public int TotalQuestion { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }

        [JsonProperty("SeqNo")]
        public int SeqNo { get; set; }
    }
}