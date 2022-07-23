using System.ComponentModel.DataAnnotations;

namespace LinksTelegramBot.Database
{
    internal class Auth
    {
        public Auth(string login, string password, long userId)
        {
            UserId = userId;
            Login = login;
            Password = password;
        }

        public int Id { get; set; }
        public long UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
