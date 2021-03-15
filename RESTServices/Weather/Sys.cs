using System.Text.Json.Serialization; 
namespace Husky_sTestBot.RESTServices.Weather{ 

    public class Sys    {
        [JsonPropertyName("country")]
        public string Country { get; set; } 

        [JsonPropertyName("sunrise")]
        public double Sunrise { get; set; } 

        [JsonPropertyName("sunset")]
        public double Sunset { get; set; } 
    }

}