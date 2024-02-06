using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackend.Models 
{
    public class Transaction {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("valor")]
        public int Valor { get; set; }

        [JsonProperty("tipo")]
        public Tipo Tipo { get; set; }

        [JsonProperty("descricao")]
        [StringLength(10, MinimumLength = 1)]
        public string Descricao { get; set; }

        public Transaction() { }
    }

    public enum Tipo {
        [Display(Name = "c")]
        C,

        [Display(Name = "d")]
        D
    }
}