using Newtonsoft.Json;

namespace Munters_Giphy
{
    public class GiphyRecord
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
