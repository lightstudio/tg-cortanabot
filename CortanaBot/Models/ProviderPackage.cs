using System.IO;

namespace CortanaBot.Models
{
    /// <summary>
    /// Provider's data package.
    /// </summary>
    public sealed class ProviderPackage
    {
        /// <summary>
        /// Data package's type.
        /// </summary>
        public ContentType Type { get; private set; }
        /// <summary>
        /// Data package's text (if available).
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Data package's image content (if available).
        /// </summary>
        public Stream ImageStream { get; set; }
        /// <summary>
        /// Data package's original caller.
        /// </summary>
        public EventCaller Caller { get; set; }
        /// <summary>
        /// Data package's original priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Create a new ProviderPackage class with text.
        /// </summary>
        /// <param name="text">Text content.</param>
        /// <param name="caller">The original caller.</param>
        /// <param name="priority">The content's priority.</param>
        public ProviderPackage(string text, EventCaller caller, int priority)
        {
            Text = text;
            Caller = caller;
            Type = ContentType.TextOnly;
            Priority = priority;
        }

        /// <summary>
        /// Create a new ProviderPackage class with text and image, but have not been tested yet.
        /// </summary>
        /// <param name="text">Text content.</param>
        /// <param name="imageStream">Image content.</param>
        /// <param name="caller">The original caller.</param>
        /// <param name="priority">The content's priority.</param>
        public ProviderPackage(string text, Stream imageStream, EventCaller caller, int priority)
        {
            Caller = caller;
            Text = text;
            ImageStream = imageStream;
            Type = ContentType.TextWithImage;
            Priority = priority;
        }

        /// <summary>
        /// Create a new ProviderPackage class image, but have not been tested yet.
        /// </summary>
        /// <param name="imageStream">Image content.</param>
        /// <param name="caller">The original caller.</param>
        /// <param name="priority">The content's priority.</param>
        public ProviderPackage(Stream imageStream, EventCaller caller, int priority)
        {
            Caller = caller;
            ImageStream = imageStream;
            Type = ContentType.ImageOnly;
            Priority = priority;
        }

        /// <summary>
        /// Create a new package with no content.
        /// </summary>
        private ProviderPackage()
        {
            Type = ContentType.NotAvailable;
            Priority = 65535;
        }

        /// <summary>
        /// Create a null content package.
        /// </summary>
        /// <returns>A null content package.</returns>
        public static ProviderPackage ReturnNotAvailablePackage()
        {
            return new ProviderPackage();
        }
    }

    /// <summary>
    /// Data package's type
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Only text.
        /// </summary>
        TextOnly = 0,
        /// <summary>
        /// Text and image.
        /// </summary>
        TextWithImage = 1,
        /// <summary>
        /// Only image.
        /// </summary>
        ImageOnly = 2,
        /// <summary>
        /// Error package. Reserved for further use.
        /// </summary>
        Error = 3,
        /// <summary>
        /// Internal package. Reserved for further use.
        /// </summary>
        Internal = 4,
        /// <summary>
        /// Not available.
        /// </summary>
        NotAvailable = 5
    }
}