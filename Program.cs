using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot;
using System.Net;
using System.IO;
using Husky_sTestBot.RESTServices.Weather;
using System.Text.Json;

namespace Husky_sTestBot
{
    class Program
    {
        public static TelegramBotClient client = new TelegramBotClient("1518555654:AAFfSOqwFPbstNTICgqg9WlTJWIXudInqQY");
        static void Main(string[] args)
        {
            Console.WriteLine("Bot started: " + DateTime.Now);
            Bot.Get().StartReceiving();
            Bot.Get().OnMessage += Bot.Bot_OnMessage;
            //Bot.Get().OnMessage += Bot.Stub;
            Console.ReadLine();
        }
    }
}

//private static void Bot_OnMessage(object sender, MessageEventArgs e)
//{
//    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
//    {
//        Bot.Get().SendTextMessageAsync(e.Message.Chat.Id, "Hello, the dog\n" + e.Message.Chat.Username);
//    }
//}

//client.StartReceiving();
//client.OnMessage += Bot_OnMessage;
//HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=Chernihiv&appid=84b802dd13a9a2bc58460391ba3ba2e4&units=metric");
//HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
//Root myDeserializedClass = null;
//using (Stream stream = response.GetResponseStream())
//{

//    myDeserializedClass = await JsonSerializer.DeserializeAsync<Root>(stream);
//}
//Weather weather = myDeserializedClass.Weather[0];
//Console.WriteLine(weather.Description);