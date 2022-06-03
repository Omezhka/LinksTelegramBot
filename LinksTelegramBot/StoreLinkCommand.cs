using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class StoreLinkCommand : ICommand
    {
        string? category;
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
        
        public void Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
           // IStorage storage = new MemoryStorage();
            
            //await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Введите категорию", replyMarkup: new ForceReplyMarkup { Selective = true });
            CommandHandler.AskUser(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);

            //if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?")) //или текст, который вы отправляли
            //{
            //    newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
            //    //chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.GetEntity(newChatMessageEventArgs.Message.Text));


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
            CommandHandler.AskUserAboutUrl(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);
            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("URL?")) //или текст, который вы отправляли
            {
                newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.StoreEntity(category, newChatMessageEventArgs.Message.Text));
            }

        }
    }
}
