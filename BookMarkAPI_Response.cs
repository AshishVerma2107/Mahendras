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
    class BookMarkAPI_Response
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string totalCount { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<BookMarkData> res_data { get; set; }

        public override string ToString()
        {
            return totalCount;
        }

    }
  public  class BookMarkData
    {
        [JsonProperty("content_id")]
        public string content_id { get; set; }

        [JsonProperty("content_title")]
        public string content_title { get; set; }

        [JsonProperty("content_date")]
        public string content_date { get; set; }

        [JsonProperty("content_file")]
        public string content_file { get; set; }



    }
}