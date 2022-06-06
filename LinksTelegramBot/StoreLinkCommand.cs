using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class StoreLinkCommand : ICommand
    {
        string? category;
        string? url;
        readonly IStorage storage = new MemoryStorage();
        //public  void Execute(NewChatMessageEventArgs newChatMessageEventArgs,IChat chat)
        //{
        //    // return "StoreLinkCommandExecute";
        //    chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, "StoreLinkCommandExecute");
        //}

        //public void ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        //{
        //    chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, "StoreLinkCommandExecuteNext");
        //}
        
        public async Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
            // IStorage storage = new MemoryStorage();

            //await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Введите категорию", replyMarkup: new ForceReplyMarkup { Selective = true });
            // await CommandHandler.AskUser(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);



            await CommandHandler.AskUser(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);

           
                //    Console.WriteLine(newChatMessageEventArgs.Message.Text);
                //    category = newChatMessageEventArgs.Message.Text; 

                //    }
                //if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("URL?")) //или текст, который вы отправляли
                //{
                //    newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                //    chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.StoreEntity(category,newChatMessageEventArgs.Message.Text));
                //}

            }

            public void ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
            //IStorage storage = new MemoryStorage();
            // string url = null;



            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?")) //или текст, который вы отправляли
            {
                newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                CommandHandler.AskUserAboutUrl(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);
                category = newChatMessageEventArgs.Message.Text;
            }

            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("URL?")) //или текст, который вы отправляли
            {
                url = newChatMessageEventArgs.Message.Text;
                newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
               // chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.StoreEntity(category, url));
               // category += " " + newChatMessageEventArgs.Message.Text;
                chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, $"я сделяль - смотри че ты написал: {category} и {url} ");
                CommandRepository.DeletePendingCommand(newChatMessageEventArgs);
            }

        }
    }
}
