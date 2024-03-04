using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackend.Models 
{
    public class Transaction 
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("valor")]
        public int Valor { get; set; }

        [JsonProperty("tipo")]
        public Tipo Tipo { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("realizado_em")]
        public DateTime RealizadoEm { get; set; }

        public Transaction() { }
    }

    public enum Tipo {
        [Display(Name = "c")]
        C,

        [Display(Name = "d")]
        D
    }
}