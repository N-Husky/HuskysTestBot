using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Husky_sTestBot.RESTServices.Weather;

namespace Husky_sTestBot.RESTServices
{
    public class CurrentWeatherRequest: RestService
    {
        public override string ApiKey => "84b802dd13a9a2bc58460391ba3ba2e4";

        public async Task<Root> RequestAsync(string cityName, string measurementsSys)
        {
            string requestStr = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid=84b802dd13a9a2bc58460391ba3ba2e4{1}",cityName,measurementsSys);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestStr);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Root myDeserializedClass = null;
            using (Stream stream = response.GetResponseStream())
            {
                    myDeserializedClass = await JsonSerializer.DeserializeAsync<Root>(stream);
            }
            return myDeserializedClass;
        }
    }
}
