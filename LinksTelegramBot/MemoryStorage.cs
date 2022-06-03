namespace LinksTelegramBot
{
    public class MemoryStorage : IStorage
    {
        private Dictionary<string,string>? _storageLink;

        public string GetEntity(string category)
        {
            return $"Some entity with category {category}";
        }

        public string GetEntityList()
        {
            return $"All entities";
        }

        public string StoreEntity(string category, string url)
        {
            _storageLink = new Dictionary<string,string>();
            _storageLink.Add(category, url);
            return $"URL {_storageLink[category]} with category {_storageLink[url]} successfully added.";
        } 
    }
}
