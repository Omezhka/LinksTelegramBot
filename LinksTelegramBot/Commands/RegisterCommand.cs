using LinksTelegramBot.EventArgs;
using LinksTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot.Commands
{
    internal class RegisterCommand : ICommand
    {
        string? login;
        string? password;
        public async Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat)
        {
            await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
            await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Логин?", replyToMessageId: newChatMessageEventArgs.Message.MessageId, replyMarkup: new ForceReplyMarkup { Selective = true });
        }

        public async Task ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat, IStorage storage)
        {
            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Логин?"))
            {
                login = newChatMessageEventArgs.Message.Text;

                await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                await newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.Message.Chat.Id, "Пароль?", replyToMessageId: newChatMessageEventArgs.Message.MessageId, replyMarkup: new ForceReplyMarkup { Selective = true });
            }

            if (newChatMessageEventArgs.Message.ReplyToMessage != null && newChatMessageEventArgs.Message.ReplyToMessage.Text.Contains("Пароль?"))
            {
                password = newChatMessageEventArgs.Message.Text;

                await newChatMessageEventArgs.BotClient.SendChatActionAsync(newChatMessageEventArgs.Message.Chat.Id, ChatAction.Typing);
                await chat.PostMessageToChat(newChatMessageEventArgs.BotClient, newChatMessageEventArgs.ChatId, storage.StoreAuth(login, password, newChatMessageEventArgs.Message.From.Id));

                CommandRepository.DeletePendingCommand(newChatMessageEventArgs);
            }
        }
    }
}