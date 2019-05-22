using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageSlider.Model
{
    class GalleryAPI_Response
    
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string totalCount { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<GalleryData> res_data { get; set; }

        public override string ToString()
        {
            return totalCount;
        }

    }
   public class GalleryData
    {
        [JsonProperty(PropertyName = ("gallery_id"))]
        public string gallery_id { get; set; }

        [JsonProperty(PropertyName = "gallery_name")]
        public string gallery_name { get; set; }

        [JsonProperty(PropertyName = "gallery_url")]
        public string gallery_url { get; set; }

       



    }
}