using Newtonsoft.Json;

namespace Bookstore.Model.Payment
{
    public class Data
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
       /* [JsonProperty("ussd_code")]
        public string UssdCode { get; set; }*/
    }
}