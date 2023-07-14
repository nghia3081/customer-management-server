

using Newtonsoft.Json;

namespace BusinessObject.Models
{
    public class ClientOdataResponseFormat<T>
    {
        public ClientOdataResponseFormat()
        {
        }
        [JsonProperty("pos")]
        public int Pos { get; set; }
        [JsonProperty("total_count")]
        public long Total { get; set; }
        [JsonProperty("data")]
        public IQueryable? Data { get; set; }
    }
}
