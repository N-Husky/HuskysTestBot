using Husky_sTestBot.RSSFeed;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Husky_sTestBot.Commands
{
    class GetOverclockersNews : Command
    {
        public override string Name => "news";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            List<OverclockersRss> rssFeed = OverclockersRss.OverclockersRssFactory();
            StringBuilder response = new StringBuilder("");
            foreach(OverclockersRss rssItem in rssFeed)
            {
                response = new StringBuilder("◽️" + rssItem.Category + "◽️\n---" + rssItem.Title + "---\n" + rssItem.Link + "\n" + rssItem.Description + "\n____________________\n");
                await client.SendTextMessageAsync(chatId, "It is " + DateTime.Today + ". Here is your daily news: \n" + response, replyToMessageId: messageId);
            }
        }
    }
}
