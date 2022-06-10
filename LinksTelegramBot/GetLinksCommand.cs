using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class GetLinksCommand : ICommand
    {
        public async Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
            await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
            await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Категория?", replyToMessageId: newChatMessageEventArgs.Message.MessageId, replyMarkup: new ForceReplyMarkup { Selective = true });
        }

        public async Task ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat, IStorage storage)
        {
            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?"))
            {
                if (newChatMessageEventArgs.Message.Text.ToLower() == "все" || newChatMessageEventArgs.Message.Text.ToLower() == "всё" || newChatMessageEventArgs.Message.Text.ToLower() == "all")
                    storage.GetEntityList(newChatMessageEventArgs);
                else  
                    storage.GetEntity(newChatMessageEventArgs, newChatMessageEventArgs.Message.Text);
                CommandRepository.DeletePendingCommand(newChatMessageEventArgs);
            }     
        }      
    }
}
