using System.Text.Json.Serialization; 
namespace Husky_sTestBot.RESTServices.Weather{ 

    public class Wind    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; } 

        [JsonPropertyName("deg")]
        public int Deg { get; set; } 

        [JsonPropertyName("gust")]
        public double Gust { get; set; } 
    }

}