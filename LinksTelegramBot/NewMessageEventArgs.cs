using Telegram.Bot;
using Telegram.Bot.Types;

namespace LinksTelegramBot
{
    public class NewMessageEventArgs
    {
        public Message Message { get; set; }
        public ITelegramBotClient BotClient  {get;set;}
    }
}
