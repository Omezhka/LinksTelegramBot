using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot
{
    public interface IChat
    {
        delegate void NewChatMessageDelegate(ITelegramBotClient botClient,Update update);
        event NewChatMessageDelegate NewChatMessage;
        void OnNewChatMessage(ITelegramBotClient botClient, Update update);
          
        void NewChatMessageReceiver();
        void PostMessageToChat();
        void Start();
        void Stop();
    }
}
