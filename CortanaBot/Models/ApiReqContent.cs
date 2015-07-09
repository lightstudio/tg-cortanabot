// ReSharper disable All
// The json definition
namespace CortanaBot.Models
{
    public class ApiReqContent
    {
        public Message message { get; set; }

        public int update_id { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    public class Chat
    {
        public int id { get; set; }
        public string title { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    public class Message
    {
        public User from { get; set; }
        public Chat chat { get; set; }
        public int message_id { get; set; }
        public string text { get; set; }
    }
}