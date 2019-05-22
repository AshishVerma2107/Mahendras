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

namespace ImageSlider
{
    class StudyMaterialAPI_Response
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string api_res_key { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<StudyMaterialData> res_data { get; set; }
    }

    public class StudyMaterialData
    {
        [JsonProperty(PropertyName = ("content_id"))]
        public string content_id { get; set; }

        [JsonProperty(PropertyName = "content_title")]
        public string content_title { get; set; }

        [JsonProperty(PropertyName = "click_url")]
        public string click_url { get; set; }

        [JsonProperty(PropertyName = "ordering")]
        public string ordering { get; set; }
    }
}