using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class CommandHandler
    {
        public CommandHandler(IChat chat,IStorage storage)
        {
            chat.NewChatMessage += OnNewChatMessage;

            Console.WriteLine("CommandHadler awake");
                   
        }

        private void OnNewChatMessage(object sender, NewMessageEventArgs newMessageEventArgs)
        {
            Console.WriteLine($"Receive message type from CommandHandler: {newMessageEventArgs.Message.Type}");
            if (newMessageEventArgs.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return;
           // var action = RecognizeCommand(newMessageEventArgs.Message);
            var action = newMessageEventArgs.Message.Text!.Split(' ')[0];
            if (action[0] == '/' && action[1] != '/')
            {
                ICommand returnCommand = CommandFactory.CreateCommand(action);
                Console.WriteLine(returnCommand.Execute());
            }
            else Console.WriteLine($"this is message {action}");
        }

       
        string RecognizeCommand(Message message) {
            
            var action = message.Text!.Split(' ')[0];
            return action switch
            {
                "/" => message.Text, 
                _ => throw new ArgumentException(message: "this is just message", message.Text)
            };
        }

        
    }
}
