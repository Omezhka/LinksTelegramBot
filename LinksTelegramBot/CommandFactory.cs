namespace LinksTelegramBot
{
    public class CommandFactory
    {
        public static ICommand CreateCommand(string commandName) {
            switch (commandName)
            {
                case "store_link":
                    return new StoreLinkCommand();                  
                case "get_links":
                    return new GetLinksCommand();
                default: throw new ArgumentException(message: "Non-existent command", commandName);
            }
        }
    }
}
