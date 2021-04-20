using Newtonsoft.Json;

namespace Munters_Giphy
{
    public class GiphyResponse
    {
        [JsonProperty("data")]
        public GiphyRecord[] GiphyRecords { get; set; }
    }
}
