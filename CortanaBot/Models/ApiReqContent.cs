// ReSharper disable All
// The json definition
namespace CortanaBot.Models
{
    /// <summary>
    /// Internal JSON class.
    /// See https://core.telegram.org/bots/api for more details.
    /// </summary>
    public class ApiReqContent
    {
        public Message message { get; set; }
    }

    /// <summary>
    /// Internal JSON class.
    /// See https://core.telegram.org/bots/api for more details.
    /// </summary>
    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    /// <summary>
    /// Internal JSON class.
    /// See https://core.telegram.org/bots/api for more details.
    /// </summary>
    public class Chat
    {
        public int id { get; set; }
        public string title { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    /// <summary>
    /// Internal JSON class.
    /// See https://core.telegram.org/bots/api for more details.
    /// </summary>
    public class Message
    {
        public User from { get; set; }
        public Chat chat { get; set; }
        public int message_id { get; set; }
        public string text { get; set; }
    }
}