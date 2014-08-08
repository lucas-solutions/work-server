using Newtonsoft.Json;

namespace Lucas.Solutions.Network
{
    public class LogglyResponse
    {
        [JsonProperty("eventstamp")]
        public int TimeStamp { get; set; }
        public bool Success { get; set; }
    }
}