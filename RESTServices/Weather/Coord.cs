using System.Text.Json.Serialization; 
namespace Husky_sTestBot.RESTServices.Weather{ 

    public class Coord    {
        [JsonPropertyName("lon")]
        public double Lon { get; set; } 

        [JsonPropertyName("lat")]
        public double Lat { get; set; } 
    }

}