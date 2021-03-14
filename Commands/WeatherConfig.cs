using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Husky_sTestBot.Commands
{
    class WeatherConfig : Command
    {
        public override string Name => "wconfig";
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId, "Specify weather configuration like so:\nyour_city, default(which is Fahrenheit) or metric (whis is Celsius)", replyToMessageId: messageId);
        }
    }
}
