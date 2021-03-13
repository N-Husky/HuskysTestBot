using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Husky_sTestBot.Commands
{
    class WeatherConfig : Command
    {
        public override string Name => "wconfigure";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            DbConnector connector = new DbConnector();
            connector.AddUserToDb(chatId, message.Chat.Username);
            await client.SendTextMessageAsync(chatId, "Specify weather configuration like so:\nyour city, default(which is Fahrenheit) or metric (whis is Celsius)" + message.Chat.Username, replyToMessageId: messageId);
            connector.Close();
        }

    }
}
