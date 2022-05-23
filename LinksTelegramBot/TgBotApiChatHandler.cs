using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot
{
    public class TgBotApiChatHandler : IChat
    {

      //  event EventHandler<NewMessageEventArgs> IChat.NewChatMessage;

        public event EventHandler<NewMessageEventArgs> NewChatMessage;


        //void IChat.OnNewChatMessage(object sender, EventArgs eventArgs)
        //{
        //    if (NewChatMessage != null)
        //        NewChatMessage(this,new EventArgs());
        //   // Console.WriteLine($"from TgBotApiChatHandler {message.Text}");
        //    // NewChatMessage.Invoke(botClient,message);
        //}

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            NewMessageEventArgs eventArgs = new()
            {
                Message = update.Message
            };

            OnNewMessage(eventArgs);

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
