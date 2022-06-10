using Telegram.Bot;
using Telegram.Bot.Types;

namespace LinksTelegramBot
{
    public class NewChatMessageEventArgs
    {
        public Message Message { get; set; }
        public ITelegramBotClient BotClient  {get;set;}
        public ChatId ChatId { get; set; }
    }
}
