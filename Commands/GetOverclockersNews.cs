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
            List<OverclockersRss> rssFeed = OverclockersRss.OverclockersFactory();
            string[] conf = NewsConfig.GetNewsConf(chatId);
            int maxamount = Int32.Parse(conf[0]);
            var dateYMD = System.Text.RegularExpressions.Regex.Match(conf[1], @"\d*-\d*-\d*").Value.Split('-');
            var dateHMS = System.Text.RegularExpressions.Regex.Match(conf[1], @"\d*:\d*:\d*").Value.Split(':');
            DateTime lastMessageDate = new DateTime(Int32.Parse(dateYMD[0]), Int32.Parse(dateYMD[1]), Int32.Parse(dateYMD[2]), Int32.Parse(dateHMS[0]), Int32.Parse(dateYMD[1]), Int32.Parse(dateYMD[2]));
            
            NewsConfig.WriteLastMessage(rssFeed[0].PubDate, chatId);
            StringBuilder response = new StringBuilder("");
            for (int i = 0; i < maxamount; i++)
            {
                dateYMD = System.Text.RegularExpressions.Regex.Match(rssFeed[i].PubDate, @"\d*-\d*-\d*").Value.Split('-');
                dateHMS = System.Text.RegularExpressions.Regex.Match(rssFeed[i].PubDate, @"\d*:\d*:\d*").Value.Split(':');
                DateTime currentMessageDate = new DateTime(Int32.Parse(dateYMD[0]), Int32.Parse(dateYMD[1]), Int32.Parse(dateYMD[2]), Int32.Parse(dateHMS[0]), Int32.Parse(dateYMD[1]), Int32.Parse(dateYMD[2]));
                if (currentMessageDate > lastMessageDate)
                {
                    response = new StringBuilder("◽️" + rssFeed[i].Category + "◽️\n---" + rssFeed[i].Title + "---\n" + rssFeed[i].Link + "\n" + rssFeed[i].Description + "\n____________________\n");
                    await client.SendTextMessageAsync(chatId, "It is " + DateTime.Today + ". Here is your daily news: \n" + response, replyToMessageId: messageId);
                }
                else
                {
                    await client.SendTextMessageAsync(chatId, "It is " + DateTime.Today + ". There is no news for you right now: \n", replyToMessageId: messageId);
                    break;
                }
            }
        }
    }
}
