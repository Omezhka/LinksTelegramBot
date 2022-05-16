namespace LinksTelegramBot
{
    public class CommandHandler
    {
        public CommandHandler(IChat chat,IStorage storage)
        {
            chat.OnNewChatMessage += () =>
            {

            };           
        }
        string RecognizeCommand(string message) { return message/*commandName*/; }
    }
}
