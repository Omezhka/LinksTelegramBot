using System;
using System.Collections.Generic;

namespace WebApplicationLinksTelegramBot.Models
{
    public partial class Auth
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
