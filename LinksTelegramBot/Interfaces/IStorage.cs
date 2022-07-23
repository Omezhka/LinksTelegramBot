using LinksTelegramBot.EventArgs;

namespace LinksTelegramBot.Interfaces
{
    public interface IStorage
    {
        void GetEntity(NewChatMessageEventArgs newChatMessageEventArgs, string category);
        string StoreEntity(string category, string url, long userId);
        string StoreAuth(string login, string password, long userId);
        void GetEntityList(NewChatMessageEventArgs newChatMessageEventArgs);
    }
}