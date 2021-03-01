using System.Text.Json.Serialization; 
namespace Husky_sTestBot.RESTServices.Weather{ 

    public class Coord    {
        [JsonPropertyName("lon")]
        public int Lon { get; set; } 

        [JsonPropertyName("lat")]
        public int Lat { get; set; } 
    }

}