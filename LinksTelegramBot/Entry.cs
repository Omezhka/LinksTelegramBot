namespace LinksTelegramBot
{
    public class Entry
    {
        public void Run()
        {
            IChat chat = new TgBotApiChatHandler();
            IStorage storage = new MemoryStorage();
            CommandHandler commandHandler = new(chat,storage);
            chat.Start();
        }
        public void Stop()
        {
            IChat chat = new TgBotApiChatHandler();
            chat.Stop();
        }
    }
}
