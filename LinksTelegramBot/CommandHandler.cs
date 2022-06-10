using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class CommandHandler
    {
        public CommandHandler(IChat chat,IStorage storage)
        {
            Console.WriteLine("CommandHadler awake");

            string? action = null;
            ICommand? returnCommand = null;
            string? resultCommand = null;

            //chat.NewChatMessage += OnNewChatMessage;
            chat.NewChatMessage += async (object? sender, NewChatMessageEventArgs newMessageEventArgs) =>
            {
                Console.WriteLine($"Receive message type from CommandHandler: {newMessageEventArgs.Message.Type}");
                if (newMessageEventArgs.Message.Type != MessageType.Text)
                    return;

                try 
                {
                    if (CommandRepository.HasPendingCommand(newMessageEventArgs))
                    {
                        returnCommand = CommandRepository.GetCommand(newMessageEventArgs);                     
                        await returnCommand.ExecuteNext(newMessageEventArgs, chat, storage);              
                        //Console.WriteLine(resultCommand);     
                    }
                    else
                    {
                        action = RecognizeCommand(newMessageEventArgs.Message);           
                        returnCommand = CommandFactory.CreateCommand(action);
                        CommandRepository.AddPendingCommand(newMessageEventArgs, returnCommand);
                        // resultCommand = returnCommand.Execute(newMessageEventArgs);
                        await returnCommand.Execute(newMessageEventArgs, chat);
                        Console.WriteLine(resultCommand);                    
                    }                 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    if (newMessageEventArgs.Message.Text[0] != '/')
                        chat.PostMessageToChat(newMessageEventArgs.BotClient, newMessageEventArgs.ChatId, $"Я увидел текст: {newMessageEventArgs.Message.Text}");
                    else CommandFactory.Usage(newMessageEventArgs.BotClient, newMessageEventArgs.Message);
                }
            };                      
        }

        static string RecognizeCommand(Message message) 
        {  
            var action = message.Text!.Split(' ')[0];
            if (action[0] == '/' && action[1] != '/')
                return message.Text;       
            else throw new ArgumentException(message: "this is just message", message.Text);     
        }
    }
}
