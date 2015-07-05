namespace CortanaBot.Models
{
    /// <summary>
    /// Message's event caller.
    /// </summary>
    public sealed class EventCaller
    {
        /// <summary>
        /// Message Id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Message caller's Id. For group chat, the group's Id.
        /// </summary>
        public int CallerId { get; set; }
        /// <summary>
        /// Message caller's first name.
        /// </summary>
        public string CallerFirstName { get; set; }
    }
}