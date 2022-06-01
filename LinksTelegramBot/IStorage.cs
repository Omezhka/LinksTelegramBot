namespace LinksTelegramBot
{
    public interface IStorage
    {
        string GetEntity();
        void StoreEntity(string category, string url);
        string GetEntityList();
    }
}