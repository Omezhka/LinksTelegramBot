namespace LinksTelegramBot
{
    public class CommandFactory
    {
        public static ICommand CreateCommand(string commandName) {
            return commandName switch
            {
                "/store_link" => new StoreLinkCommand(),
                "/get_links" => new GetLinksCommand(),
                _ => throw new ArgumentException(message: "Non-existent command", commandName),
            };
        }
    }
}
