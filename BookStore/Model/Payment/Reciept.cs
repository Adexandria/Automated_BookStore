using Newtonsoft.Json;

namespace Bookstore.Model.Payment
{
    public class Reciept
    {
        [JsonProperty("data")]
        public RecieptData Data { get; set; }
    }
}