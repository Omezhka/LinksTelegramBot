using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class CommandHandler
    {
        public CommandHandler(IChat chat,IStorage storage)
        {
            Console.WriteLine("CommandHadler awake");

            //chat.NewChatMessage += OnNewChatMessage;
            chat.NewChatMessage += (object sender, NewChatMessageEventArgs newMessageEventArgs) =>
            {
                Console.WriteLine($"Receive message type from CommandHandler: {newMessageEventArgs.Message.Type}");
                if (newMessageEventArgs.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                    return;
                
                try
                { 
                    var action = RecognizeCommand(newMessageEventArgs.Message);
                    ICommand returnCommand = CommandFactory.CreateCommand(action);
                    var resultCommand = returnCommand.Execute();
                    Console.WriteLine(returnCommand.Execute());

                    chat.PostMessageToChat(newMessageEventArgs.BotClient, newMessageEventArgs.ChatId, resultCommand);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //chat.PostMessageToChat(newMessageEventArgs.BotClient, newMessageEventArgs.Message);
                    Usage(newMessageEventArgs.BotClient, newMessageEventArgs.Message);
                }          
            };                      
        }

        static string RecognizeCommand(Message message) {
            
            var action = message.Text!.Split(' ')[0];
            return action switch
            {
                "/store_link" => message.Text, 
                "/get_links" => message.Text,
                _ => throw new ArgumentException(message: "this is just message", message.Text)
            };
        }
        static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/store_link - сохранение URL-ссылки в персональную записную книжку\n" +
                                 "/get_links - вывод списка запомненных ссылок\n";
                                

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }

    }
}
