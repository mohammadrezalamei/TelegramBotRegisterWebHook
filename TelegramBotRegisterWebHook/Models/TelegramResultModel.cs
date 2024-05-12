namespace TelegramBotRegisterWebHook.Models
{
    public class TelegramResultModel : System.Object
    {
        public TelegramResultModel() 
        { 
        }
        public bool Ok { get; set; } = false;
        public string? Description { get; set; } = "The Telegram server did not send a result.";
        public string? Result { get; set; }
        public int? Error_code { get; set; }
    }
}
