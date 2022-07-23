using LinksTelegramBot.Database;
using LinksTelegramBot.EventArgs;
using LinksTelegramBot.Interfaces;
using Telegram.Bot;

namespace LinksTelegramBot.Storage
{
    internal class DatabaseStorage : IStorage
    {
        public void GetEntity(NewChatMessageEventArgs newChatMessageEventArgs, string category)
        {
            using (ApplicationDbContext db = new())
            {
                if (!db.Links.Where(l => l.UserId == newChatMessageEventArgs.Message.From.Id).Any())
                    newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"Еще не добавлено категорий");
                else
                {
                    List<Link> linkList = new();

                    if (db.Links.Where(l => l.Category == category && l.UserId == newChatMessageEventArgs.Message.From.Id).Any())
                        linkList = db.Links.Where(l => l.Category == category && l.UserId == newChatMessageEventArgs.Message.From.Id).ToList();
                    else newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"Такой категории нет");

                    foreach (var link in linkList)
                    {
                        newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"{link.Category} {link.Url}");
                    }
                }
            }
        }

        public void GetEntityList(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            using ApplicationDbContext db = new();
            bool userExists = db.Links.Where(l => l.UserId == newChatMessageEventArgs.Message.From.Id).Any();

            if (userExists)
                foreach (var link in db.Links.Where(l => l.UserId == newChatMessageEventArgs.Message.From.Id).ToList())
                {
                    newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"{link.Category} {link.Url}");
                }
            else
                newChatMessageEventArgs.BotClient.SendTextMessageAsync(newChatMessageEventArgs.ChatId, $"Еще не добавлено категорий");

        }

        public string StoreEntity(string category, string url, long userId)
        {
            Link link = new(category, url, userId);
            using (ApplicationDbContext db = new())
            {
                bool categoryExists = db.Links.Where(l => l.Category == category).Any();
                bool urlExists = db.Links.Where(l => l.Url == url).Any();

                if (categoryExists && urlExists)
                    return $"URL {db.Links.FirstOrDefault(l => l.Url == url).Url} существует. Воспользуйтесь командой /get_links";
                else
                {
                    db.Links.Add(link);
                    db.SaveChanges();
                    return $"URL {db.Links.FirstOrDefault(l => l.Url == url).Url} с категорией {db.Links.FirstOrDefault(l => l.Category == category).Category} успешно добавлена.";
                }
            }
        }

        public string StoreAuth(string login, string password, long userId)
        {
            Auth auth = new(login, password, userId);
            using (ApplicationDbContext db = new())
            {
                bool loginExists = db.Auths.Where(l => l.Login == login).Any();

                if (loginExists)
                    return $"Пользователь с логином {db.Auths.FirstOrDefault(l => l.Login == login).Login} уже существует.";       
                else
                {
                    db.Auths.Add(auth);
                    db.SaveChanges();
                    return $"Пользователь с логином {db.Auths.FirstOrDefault(l => l.Login == login).Login} и паролем {db.Auths.FirstOrDefault(l => l.Password == password).Password} успешно добавлен.";
                }
            }
        }
    }
}
