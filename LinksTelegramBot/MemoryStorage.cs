namespace LinksTelegramBot
{
    public class MemoryStorage : IStorage
    {
        private Dictionary<string,string>? _storageLink;

        public string GetEntity()
        {
            return $"Some entity";
        }

        public string GetEntityList()
        {
            return $"All entities";
        }

        public void StoreEntity(string category, string url)
        {
            throw new NotImplementedException();
        }
    }
}
