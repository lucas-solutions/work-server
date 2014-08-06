using Newtonsoft.Json;

namespace Lucas.Solutions.Diagnostics.Responses
{
    public class LogResponse
    {
        [JsonProperty("eventstamp")]
        public int TimeStamp { get; set; }
        public bool Success { get; set; }
    }
}