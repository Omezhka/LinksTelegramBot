namespace LinksTelegramBot
{
    public interface IStorage
    {
        void GetEntity(NewChatMessageEventArgs newChatMessageEventArgs, string category);
        string StoreEntity(string category, string url);
        void GetEntityList(NewChatMessageEventArgs newChatMessageEventArgs);
    }
}