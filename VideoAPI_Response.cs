using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageSlider
{
   public class VideoAPI_Response
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string totalCount { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<VideoData> res_data { get; set; }

        public override string ToString()
        {
            return totalCount;
        }

    }
  public  class VideoData
    {
        [JsonProperty(PropertyName = ("videoId"))]
        public string videoId { get; set; }

        [JsonProperty(PropertyName = "thumb")]
        public string thumb { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "videoURL")]
        public string videoURL { get; set; }



    }
}