using LinksTelegramBot.EventArgs;
using LinksTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot.TgApiHandler
{
    public class TgBotApiChatHandler : IChat
    {
        public event EventHandler<NewChatMessageEventArgs>? NewChatMessage;
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            NewChatMessageEventArgs eventArgs = new()
            {
                Message = update.Message,
                BotClient = botClient,
                ChatId = update.Message.Chat.Id
            };
            await Task.CompletedTask;
            OnNewChatMessage(eventArgs);

        }

        protected virtual void OnNewChatMessage(NewChatMessageEventArgs e)
        {
            EventHandler<NewChatMessageEventArgs> eventHandler = NewChatMessage;
            if (eventHandler != null)
                eventHandler(this, e);
        }

        public async Task<Message> PostMessageToChat(ITelegramBotClient botClient, ChatId chatId, string message)
        {
            return await botClient.SendTextMessageAsync(chatId: chatId,
                                                        text: message,//message.Text,
                                                        replyMarkup: new ReplyKeyboardRemove());
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
        }

        public void Stop()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
        }
    }
}
