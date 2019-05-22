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
    public class ResDataCareer
    {
        [JsonProperty(PropertyName = "content_id")]
        public string content_id { get; set; }

        [JsonProperty(PropertyName = "content_title")]
        public string content_title { get; set; }

        [JsonProperty(PropertyName = "content_text")]
        public string content_text { get; set; }

        [JsonProperty(PropertyName = "content_date")]
        public string content_date { get; set; }

        [JsonProperty(PropertyName = "menu_id")]
        public string menu_id { get; set; }

        [JsonProperty(PropertyName = "catid")]
        public string catid { get; set; }
    }

    public class CareerModel
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string api_res_key { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<ResDataCareer> res_data { get; set; }
    }
}

