// ReSharper disable All
// The json definition
namespace CortanaBot.Models
{
    public class ApiReply
    {
        public int chat_id { get; set; }
        public string text { get; set; }
        public bool disable_web_page_preview { get; set; }
        public int reply_to_message_id { get; set; }
    }
}