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
    public class ResDataJoinUs
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string content { get; set; }
    }

    public class JoinUsModel
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string api_res_key { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public ResDataJoinUs res_data { get; set; }
    }
}