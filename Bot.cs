using Husky_sTestBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Husky_sTestBot
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }
        public static TelegramBotClient Get()
        {
            if (client != null)
                return client;
            client = new TelegramBotClient(BotSettings.Key);
            commandsList = new List<Command>();
            //Add bot`s commands
            commandsList.Add(new HelloCommand());
            commandsList.Add(new CurrentWeatherCommand());
            return client;
        }
        public static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Bot received message at: " + DateTime.Now + "\nFrom " + e.Message.Chat.Username + "\nCommand: " + e.Message.Text);
            foreach (var command in Commands)
            {
                if (command.Contains(e.Message.Text))
                {
                    command.Execute(e.Message, Bot.Get());
                    break;
                }
            }
        }
    }
}
