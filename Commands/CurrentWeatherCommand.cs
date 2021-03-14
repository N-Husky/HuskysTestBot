using Husky_sTestBot.RESTServices;
using Husky_sTestBot.RESTServices.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Husky_sTestBot.Commands
{
    public class CurrentWeatherCommand : Command
    {
        public override string Name => "weather";

        public readonly Dictionary<string, string> weatherIcon = new Dictionary<string, string>
        {
            {"01d", "☀️" },
            {"02d", "🌤" },
            {"03d", "☁️" },
            {"04d", "☁️" },
            {"09d", "🌨☔️" },
            {"10d", "🌦☔️" },
            {"11d", "🌩☔️" },
            {"13d", "❄️" },
            {"50d", "🌫" },
            {"01n", "🌖" },
            {"02n", "🌕☁️"},
            {"03n", "☁️" },
            {"04n", "☁️" },
            {"09n", "🌨☔️" },
            {"10n", "🌦☔️" },
            {"11n", "🌩☔️" },
            {"13n", "❄️" },
            {"50n", "🌫" }
        };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            DbConnector connector = new DbConnector();
            string[] conf = connector.GetWeatherConf(message.Chat.Id);
            string measurementSys = conf[1] == "default" ? "" : "&units=metric";
            Root root = await new CurrentWeatherRequest().RequestAsync(conf[0], measurementSys);
            StringBuilder response= new StringBuilder("Current weather: \n" +
                $"◽️🌡: {root.Main.Temp} \n◽️Feels like: {root.Main.FeelsLike} \n◽️Humidity: {root.Main.Humidity}\n◽️🌬: {root.Wind.Speed}\n" +
                $"________________________________\n" +
                $"Weather condition:\n" +
                $"{weatherIcon[root.Weather[0].Icon]} {root.Weather[0].Description}");
            await client.SendTextMessageAsync(chatId, response.ToString() , replyToMessageId: messageId);
        }
    }
}
