using LinksTelegramBot.EventArgs;
using LinksTelegramBot.Interfaces;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinksTelegramBot.Storage
{
    public class MemoryStorage : IStorage
    {
        private static Dictionary<string, List<string>> _storageLink;
        public MemoryStorage()
        {
            _storageLink = new(StringComparer.InvariantCultureIgnoreCase);
        }

        public void GetEntity(NewChatMessageEventArgs newChatMessageEventArgs, string category)
        {
            List<string> list = new();

            if (_storageLink.Count == 0)
                newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"В хранилище пока пусто");
            
            if (_storageLink.Count != 0 && _storageLink.ContainsKey(category))
                list = _storageLink[category].ToList();

            if (!_storageLink.ContainsKey(category))
                newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"Такой категории нет");
            else
                foreach (var item in list)
                {
                    newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, item);
                }
        }

        public void GetEntityList(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            if (_storageLink.Count == 0)
                newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"В хранилище пока пусто");
            else
                foreach (var entity in _storageLink)
                {
                    foreach (var item in entity.Value)
                    {
                        newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"{item} с категорией {entity.Key}");
                    }

                }
        }

        public string StoreAuth(string login, string password, long userId)
        {
            throw new NotImplementedException();
        }

        public string StoreEntity(string category, string url, long userId)
        {
            List<string> list = new();

            if (_storageLink.Count != 0 && _storageLink.ContainsKey(category) && _storageLink[category].Contains(url))
                return $"URL {_storageLink[category].Find(x => x == url)} существует. Воспользуйтесь командой /get_links";
            else if (_storageLink.ContainsKey(category))
            {
                _storageLink[category].Add(url);
                return $"{_storageLink[category].LastOrDefault(url)} with category {_storageLink.FirstOrDefault(x => x.Key == category).Key} succ added. !!!!";
            }
            else
            {
                list.Add(url);
                _storageLink.Add(category, list);
                return $"{_storageLink[category].LastOrDefault(url)} with category {_storageLink.FirstOrDefault(x => x.Key == category).Key} succ added.";
            }
        }
    }
}
