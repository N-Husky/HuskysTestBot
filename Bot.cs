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
        public static MessageEventArgs PrevCommand;
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
            commandsList.Add(new GetOverclockersNews());
            commandsList.Add(new WeatherConfig());
            commandsList.Add(new NewsConfig());
            return client;
        }

        internal static void Stub(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Stub method, command sended by" + e.Message.Chat.Username + " at " + DateTime.Now);
        }

        public static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Bot received messag e at: " + DateTime.Now + "\nFrom " + e.Message.Chat.Username + "\nCommand: " + e.Message.Text);
            foreach (var command in Commands)
            {
                if (command.Contains(e.Message.Text))
                {
                    PrevCommand = e;
                    command.Execute(e.Message, Bot.Get());
                    break;
                }
                if(PrevCommand!=null && PrevCommand.Message.Text == "/wconfig@HuskyTestBot")
                {
                    string config = e.Message.Text.Replace(" ", "");
                    if(config.Split(new char[] { ',' }).Length !=2)
                    {
                        //send error message
                        PrevCommand = null;
                        return;
                    }
                    WeatherConfig.AddWeatherConf(PrevCommand.Message.Chat.Id, PrevCommand.Message.Chat.Username, config.Split(new char[] { ',' })[0], config.Split(new char[] { ',' })[1]);
                    PrevCommand = null;
                    break;
                }
                if(PrevCommand != null && PrevCommand.Message.Text == "/nconfig@HuskyTestBot")
                {
                    string config = e.Message.Text.Replace(" ", "");
                    int temp;
                    if (config.Split(new char[] { ',' }).Length != 1|| !Int32.TryParse(config,out temp))
                    {
                        //send error message
                        PrevCommand = null;
                        return;
                    }
                    NewsConfig.AddNewsConf(PrevCommand.Message.Chat.Id, Int32.Parse(config), PrevCommand.Message.Chat.Username);
                    PrevCommand = null;
                    break;
                }
            }
        }
    }
}
