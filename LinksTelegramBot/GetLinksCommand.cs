using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class GetLinksCommand : ICommand
    {
        readonly IStorage storage = new MemoryStorage();
        public async Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
            await CommandHandler.AskUser(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);
        }

        public void ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
           
            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?"))
            {
                chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.GetEntity(newChatMessageEventArgs.Message.Text));
               
            }
            
        }
    }
}
