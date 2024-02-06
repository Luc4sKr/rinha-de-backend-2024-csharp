using Newtonsoft.Json;

namespace RinhaDeBackend.Models {
    public class User 
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("saldo")]
        public int Saldo { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        public User() { }
    }
}