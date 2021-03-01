using System.Text.Json.Serialization; 
namespace Husky_sTestBot.RESTServices.Weather{ 

    public class Clouds    {
        [JsonPropertyName("all")]
        public int All { get; set; } 
    }

}