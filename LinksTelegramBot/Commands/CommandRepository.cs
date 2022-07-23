using LinksTelegramBot.EventArgs;
using LinksTelegramBot.Interfaces;

namespace LinksTelegramBot.Commands
{
    public class CommandRepository
    {
        static Dictionary<long, ICommand> usersList = new();

        public static void AddPendingCommand(NewChatMessageEventArgs newChatMessageEventArgs, ICommand command)
        {
            usersList.Add(newChatMessageEventArgs.Message.From.Id, command);
        }

        public static void DeletePendingCommand(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            usersList.Remove(newChatMessageEventArgs.Message.From.Id);
        }

        public static ICommand GetCommand(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            return usersList[newChatMessageEventArgs.Message.From.Id];
        }

        public static bool HasPendingCommand(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            var userId = newChatMessageEventArgs.Message.From.Id;

            if (usersList.ContainsKey(userId))
                return true;
            else return false;
        }
    }
}
