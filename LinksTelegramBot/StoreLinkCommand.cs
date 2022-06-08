using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class StoreLinkCommand : ICommand
    {
        string? category;
        string? url;
        
        public async Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
            await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
            await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Категория?", replyToMessageId: newChatMessageEventArgs.Message.MessageId, replyMarkup: new ForceReplyMarkup { Selective = true }); 
        }

        public async Task ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat, IStorage storage)
        {
            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?"))
            {
                category = newChatMessageEventArgs.Message.Text;

                await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "URL?", replyToMessageId: newChatMessageEventArgs.Message.MessageId, replyMarkup: new ForceReplyMarkup { Selective = true });  
            }

            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("URL?"))
            {
                url = newChatMessageEventArgs.Message.Text;

                await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                await chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.StoreEntity(url, category));

                //   chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, $"Смотри че ты написал: {category} и {url} ");
                CommandRepository.DeletePendingCommand(newChatMessageEventArgs);
            }

        }
    }
}
