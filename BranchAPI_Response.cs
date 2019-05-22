using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageSlider
{
    class BranchAPI_Response
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string totalCount { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<BranchData> res_data { get; set; }

        public override string ToString()
        {
            return totalCount;
        }

    }
    class BranchData
    {
        [JsonProperty(PropertyName = ("state_id"))]
        public string state_id { get; set; }

        [JsonProperty(PropertyName = "state_name")]
        public string state_name { get; set; }

        


    }
}