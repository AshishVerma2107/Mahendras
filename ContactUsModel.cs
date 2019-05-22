using Newtonsoft.Json;

namespace ImageSlider
{
    public class ResDataContact
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string content { get; set; }
    }

    public class ContactUsModel
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string api_res_key { get; set; }
        [JsonProperty(PropertyName = "res_data")]
        public ResData res_data { get; set; }
    }
}