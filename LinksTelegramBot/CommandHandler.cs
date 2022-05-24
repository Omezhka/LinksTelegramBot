using Telegram.Bot.Types;

namespace LinksTelegramBot
{
    public class CommandHandler
    {
        public CommandHandler(IChat chat,IStorage storage)
        {
            Console.WriteLine("CommandHadler awake");

            //chat.NewChatMessage += OnNewChatMessage;
            chat.NewChatMessage += (object? sender, NewChatMessageEventArgs newMessageEventArgs) =>
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
