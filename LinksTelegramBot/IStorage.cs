namespace LinksTelegramBot
{
    public interface IStorage
    {
        void GetEntity();
        void StoreEntity(string category, string url);
        void GetEntityList();
    }
}