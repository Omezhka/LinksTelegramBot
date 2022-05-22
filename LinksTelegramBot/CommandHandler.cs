namespace LinksTelegramBot
{
    public class CommandHandler
    {
        public CommandHandler(IChat chat,IStorage storage)
        {
            chat.NewChatMessage += chat.OnNewChatMessage;

            Console.WriteLine("CommandHadler awake");
        }
        string RecognizeCommand(string message) { return message/*commandName*/; }
    }
}
