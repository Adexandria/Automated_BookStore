using Newtonsoft.Json;

namespace Bookstore.Model.Payment
{
    public class RecieptData
    {
        [JsonProperty("Reference")]
        public string Reference { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("gateway_response")]
        public string Response { get; set; }
    }
}