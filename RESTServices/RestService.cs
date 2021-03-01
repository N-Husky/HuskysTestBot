using System;
using System.Collections.Generic;
using System.Text;

namespace Husky_sTestBot.RESTServices
{
    //these type of classes contains logic related to JSON parsing, HTTP requests etc... Command useus interfaces such as GetWeather() etc
    public abstract class RestService
    {
        public abstract string ApiKey { get; }
        
    }
}
