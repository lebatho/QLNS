namespace QLNS.Models
{
    public class GeneralSettingModel
    {
        public string[]? AllowedAuthOrigins { get; set; }
        public string BotToken { get; set; }
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
        public long ChatId { get; set; }
    }
}
