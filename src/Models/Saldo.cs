using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackend.Models
{
    public class Saldo 
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        
        [JsonProperty("total")]
        public int Total { get; set; }
        
        [JsonProperty("limite")]
        public int Limite { get; set; }

        public Saldo() { }
    }
}