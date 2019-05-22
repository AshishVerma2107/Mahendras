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
   public class ApiResponse
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string totalCount { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string incompleteResults { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public Dictionary<string,AboutExamData> items { get; set; }

        public override string ToString()
        {
            return totalCount;
        }

    }
   public class AboutExamData {
        [JsonProperty(PropertyName = "exam_category_id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "exam_category_name")]
        public string name { get; set; }

    }
}