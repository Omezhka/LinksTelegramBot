namespace WebApplicationLinksTelegramBot.Models
{
    public partial class Link
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string Url { get; set; } = null!;
        public long UserId { get; set; }
    }
}
