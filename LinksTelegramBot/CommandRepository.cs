using System.Linq;
using Telegram.Bot.Types;

namespace LinksTelegramBot
{
   public class CommandRepository
   {
        static Dictionary<long,ICommand> usersList = new();

        public static void AddPendingCommand(NewChatMessageEventArgs newChatMessageEventArgs, ICommand command)
        {
            usersList.Add(newChatMessageEventArgs.Message.From.Id, command);
        }

        public static void DeletePendingCommand(NewChatMessageEventArgs newChatMessageEventArgs/*, ICommand command*/)
        {
            usersList.Remove(newChatMessageEventArgs.Message.From.Id);
        }

        public static ICommand GetCommand(NewChatMessageEventArgs newChatMessageEventArgs) {
            return usersList[newChatMessageEventArgs.Message.From.Id];
        }

        public static bool HasPendingCommand(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            var userId = newChatMessageEventArgs.Message.From.Id;
           
            if (usersList.ContainsKey(userId))
            {
                // usersList.Remove(userId,out command);
                //usersList.Remove(command);
                
                return true;
            }
            else return false;
        }


    }
}
