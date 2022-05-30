using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot
{
    public interface IChat
    {
        //delegate void NewChatMessageDelegate(object sender, EventArgs eventArgs);
        event EventHandler<NewChatMessageEventArgs> NewChatMessage;
        // void OnNewChatMessage(object sender, EventArgs eventArgs);

        // void NewChatMessageReceiver();
        Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task<Message> PostMessageToChat(ITelegramBotClient botClient, ChatId chatId, string message);

        void Start();
        void Stop();
    }
}
