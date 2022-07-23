using LinksTelegramBot.EventArgs;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot.Interfaces
{
    public interface IChat
    {
        event EventHandler<NewChatMessageEventArgs> NewChatMessage;
        Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task<Message> PostMessageToChat(ITelegramBotClient botClient, ChatId chatId, string message);
        void Start();
        void Stop();
    }
}
