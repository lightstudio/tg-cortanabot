using System.IO;

namespace CortanaBot.Models
{
    public sealed class ProviderPackage
    {
        public ContentType Type { get; private set; }
        public string Text { get; set; }
        public Stream ImageStream { get; set; }
        public EventCaller Caller { get; set; }
        public int Priority { get; set; }

        public ProviderPackage(string text, EventCaller caller, int priority)
        {
            Text = text;
            Caller = caller;
            Type = ContentType.TextOnly;
            Priority = priority;
        }

        public ProviderPackage(string text, Stream imageStream, EventCaller caller, int priority)
        {
            Caller = caller;
            Text = text;
            ImageStream = imageStream;
            Type = ContentType.TextWithImage;
            Priority = priority;
        }

        public ProviderPackage(Stream imageStream, EventCaller caller, int priority)
        {
            Caller = caller;
            ImageStream = imageStream;
            Type = ContentType.ImageOnly;
            Priority = priority;
        }

        private ProviderPackage()
        {
            Type = ContentType.NotAvailable;
            Priority = 65535;
        }

        public static ProviderPackage ReturnNotAvailablePackage()
        {
            return new ProviderPackage();
        }
    }

    public enum ContentType
    {
        TextOnly = 0,
        TextWithImage = 1,
        ImageOnly = 2,
        Error = 3,
        Internal = 4,
        NotAvailable = 5
    }
}