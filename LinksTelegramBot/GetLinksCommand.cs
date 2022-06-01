using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class GetLinksCommand : ICommand
    {

        public static async Task AskUser(ITelegramBotClient bot, NewChatMessageEventArgs newChatMessageEventArgs)
        {
            await bot.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
            await bot.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Категория?",replyToMessageId: newChatMessageEventArgs.Message.MessageId, replyMarkup: new ForceReplyMarkup { Selective = true });
        }

        public async Task<string> Execute(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            IStorage storage = new MemoryStorage();
            //await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Введите категорию", replyMarkup: new ForceReplyMarkup { Selective = true });
            await AskUser(newChatMessageEventArgs.BotClient, newChatMessageEventArgs);

            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?")) //или текст, который вы отправляли
            {
                await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                return storage.GetEntity();
            }
            return storage.GetEntity();
        }

        public string ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            MemoryStorage memory = new();
            string url = null;

            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Категория?"))
            {
                url = memory.GetEntity();
            }
            return url;
        }
    }
}
