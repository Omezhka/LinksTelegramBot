namespace LinksTelegramBot
{
    public interface IStorage
    {
        string GetEntity(string category);
        string StoreEntity(string category, string url);
        string GetEntityList();
    }
}