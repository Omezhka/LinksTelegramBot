using LinksTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot.Commands
{
    public class CommandFactory
    {
        public static ICommand CreateCommand(string commandName)
        {
            return commandName switch
            {
                "/store_link" => new StoreLinkCommand(),
                "/get_links" => new GetLinksCommand(),
                "/register" => new RegisterCommand(),
                _ => throw new ArgumentException(message: "Non-existent command", commandName),
            };
        }

        public static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/store_link - сохранение URL-ссылки в персональную записную книжку\n" +
                                 "/get_links - вывод списка запомненных ссылок\n" +
                                 "/register - регистрация на сайте";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }
    }
}
