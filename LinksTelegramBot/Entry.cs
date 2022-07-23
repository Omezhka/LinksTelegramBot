using LinksTelegramBot.Commands;
using LinksTelegramBot.Interfaces;
using LinksTelegramBot.Storage;
using LinksTelegramBot.TgApiHandler;

namespace LinksTelegramBot
{
    public class Entry
    {
        public void Run()
        {
            IChat chat = new TgBotApiChatHandler();
            //IStorage storage = new MemoryStorage();
            IStorage storage = new DatabaseStorage();
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
