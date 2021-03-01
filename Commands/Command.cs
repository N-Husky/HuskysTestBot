using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Husky_sTestBot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract void Execute(Message message, TelegramBotClient client);
        
        public bool Contains(string command)
        {
            return command.Contains(this.Name) && command.Contains(BotSettings.Name);
        }
    }
}
