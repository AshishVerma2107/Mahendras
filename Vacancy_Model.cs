using Newtonsoft.Json;
using System.Collections.Generic;

namespace ImageSlider
{
   
    public class ResDataVacancy
    {
        [JsonProperty(PropertyName = "api_res_key")]
        public string totalCount { get; set; }

        [JsonProperty(PropertyName = "res_status")]
        public string res_status { get; set; }

        [JsonProperty(PropertyName = "res_data")]
        public List<Vacancy_Model> res_datavacancy { get; set; }

        public override string ToString()
        {
            return totalCount;
        }
    }

    public class Vacancy_Model
    {
        [JsonProperty(PropertyName = "content_id")]
        public string content_id { get; set; }

        [JsonProperty(PropertyName = "content_title")]
        public string content_title { get; set; }

        [JsonProperty(PropertyName = "content_file")]
        public string content_text { get; set; }

        [JsonProperty(PropertyName = "content_date")]
        public string content_date { get; set; }
    }
}