namespace LinksTelegramBot
{
    public class Entry
    {
        public void Run()
        {
            IChat chat = new TgBotApiChatHandler();
            IStorage storage = new MemoryStorage();
            chat.Start();
            CommandHandler commandHandler = new(chat,storage);
           
        }
        public void Stop()
        {
            IChat chat = new TgBotApiChatHandler();
            chat.Stop();
        }
    }
}
