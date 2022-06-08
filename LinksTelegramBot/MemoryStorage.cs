using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot
{
    public class MemoryStorage : IStorage
    {
        private static Dictionary<string, string>? _storageLink;

        public MemoryStorage()
        {
            _storageLink = new(StringComparer.InvariantCultureIgnoreCase);
        }

        public string GetEntity(string category)
        {
            if (category == null || /*!_storageLink.Values.Contains(category) ||*/ !_storageLink.Any(i => i.Value.ToLower().Contains(category)))
                return "Такой категории нет";
            else 
                return $"{_storageLink.FirstOrDefault(x => x.Value == category.ToLower()).Key} с категорией {category}";
        }

        public void GetEntityList(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            if (_storageLink.Count == 0)
                newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"В хранилище пока пусто");
            else
            {
                foreach (var entity in _storageLink)
                { 
                    newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"{entity.Key} с категорией {entity.Value}");
                }
            }
        }

        public string StoreEntity(string url, string category)
        {
            if (_storageLink.Keys.Contains(url))
                return $"URL {_storageLink.FirstOrDefault(x => x.Key == url).Key} уже существует. Воспользуйтесь командой /get_links";
            //else if (_storageLink.Values.Contains(category))
            //    return $"Категория {_storageLink.FirstOrDefault(x => x.Value == category).Value} уже существует. Воспользуйтесь командой /get_links";
            else
            {
                _storageLink.Add(url, category);
                return $"URL {_storageLink.FirstOrDefault(x => x.Value == category).Key} с категорией {_storageLink.FirstOrDefault(x => x.Key == url).Value} успешно добавлена"; //{_storageLink[category]}
            }

            // return $"URL {_storageLink.FirstOrDefault(x => x.Key == category).Value} with category {_storageLink.FirstOrDefault(x => x.Value == url).Key} successfully added."; //{_storageLink[category]}
        }
    }
}
