using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
namespace Husky_sTestBot.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            DbConnector connector = new DbConnector();
            connector.AddUserToDb(chatId, message.Chat.Username);
            await client.SendTextMessageAsync(chatId, "Hello, " + message.Chat.Username, replyToMessageId: messageId );
            connector.Close();
        }
    }
}
