using LinksTelegramBot.EventArgs;

namespace LinksTelegramBot.Interfaces
{
    public interface ICommand
    {
        Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat);
        Task ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat, IStorage storage);
    }
}
