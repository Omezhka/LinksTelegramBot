using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class TgBotApiChatHandler : IChat
    {
        public event EventHandler<NewMessageEventArgs> NewChatMessage;

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            NewMessageEventArgs eventArgs = new()
            {
                Message = update.Message,
                BotClient = botClient,
            };

            OnNewMessage(eventArgs);

            //PostMessageToChat(botClient, update.Message);

        }

        protected virtual void OnNewMessage(NewMessageEventArgs e) {
            EventHandler<NewMessageEventArgs> eventHandler = NewChatMessage;
            if (eventHandler != null)
                eventHandler(this, e);
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

        public async Task<Message> PostMessageToChat(ITelegramBotClient botClient, Message message)
        {
            //const string usage = "Usage:\n" +
            //                     "/inline   - send inline keyboard\n" +
            //                     "/keyboard - send custom keyboard\n" +
            //                     "/remove   - remove custom keyboard\n" +
            //                     "/photo    - send a photo\n" +
            //                     "/request  - request location or contact";
            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: message.Text,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }
        public static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/inline   - send inline keyboard\n" +
                                 "/keyboard - send custom keyboard\n" +
                                 "/remove   - remove custom keyboard\n" +
                                 "/photo    - send a photo\n" +
                                 "/request  - request location or contact";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }
    }
}
