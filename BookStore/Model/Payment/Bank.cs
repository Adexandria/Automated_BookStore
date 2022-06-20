using Newtonsoft.Json;

namespace Bookstore.Model.Payment
{
    public class Bank
    {
        [JsonProperty("account_number")]
        public string AccountNo { get; set; } = "0000000000";
        [JsonProperty("code")]
        public string CVV { get; set; } = "057";
    }
}