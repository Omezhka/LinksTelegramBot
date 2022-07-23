using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinksTelegramBot.Database
{
    internal class Link
    {
        public Link(string category, string url, long userId)
        {
            Category = category;
            Url = url;
            UserId = userId;
        }
        public int Id { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public long UserId { get; set; }
    }
}
