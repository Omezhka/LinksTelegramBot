using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot
{
    public class TgBotApiChatHandler : IChat
    {
        public event IChat.NewChatMessageDelegate? NewChatMessage;

        void IChat.OnNewChatMessage(ITelegramBotClient botClient, Update update)
        {
            if (NewChatMessage != null)
                NewChatMessage(botClient, update);
            Console.WriteLine(update.Message.Text);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            IChat chat = new TgBotApiChatHandler();
            chat.OnNewChatMessage(botClient, update);
        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public async Task PostMessageToChat()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {

            var bot = new TelegramBotClient(Config.botToken);
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();

            bot.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions: new ReceiverOptions()
                    {
                        AllowedUpdates = { }
                    },
                    cancellationToken: cts.Token
                );
            // NewChatMessageReceiver();
        }

        public void Stop()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
        }

        void IChat.PostMessageToChat()
        {
            throw new NotImplementedException();
        }

      

        public void NewChatMessageReceiver()
        {
            throw new NotImplementedException();
        }
    }
}
