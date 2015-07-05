using System;
using System.Threading.Tasks;
using CortanaBot.Models;

namespace CortanaBot.Provider
{
    /* HOW TO IMPLEMENT A CONTENT PROVIDER *
     * Create a new class which inherits from the interface below.
     * 
     * Define the supported language (only single language yet), plugin name, plugin author, plugin priority, as well as version.
     * 
     * The plugin's priority controls how the bot returns data. The higher the priority is, the more likely the content provider returns data.
     * Implements GetAsync method. Your content provider will get request via that method. Argument `caller` provides you the details of the message,
     * argument `inputContent` provides you the Bing's HTML result in string.
     * For example, I have two plugins, Calculcator and Chitchat. Calculcator has higher priority than Chitchat, so when calculcation content is available, 
     * Calculcator will return data instead of Chitchat. You can see the default Calculcator.cs and Chitchat.cs for more details.
     * 
     * When no data is available, simply return a "not available" data package. You can get one simply by calling static method ProviderPackage.ReturnNotAvailablePackage().
     * 
     * Note that all plugins run in asynchronous method.
     */

    /// <summary>
    /// Content Provider's interface
    /// </summary>
    /// <seealso cref="EventCaller"/>
    interface IContentProvider
    {
        /// <summary>
        /// Content provider's request entry.
        /// </summary>
        /// <param name="caller">Message caller details</param>
        /// <param name="inputContent">Bing's HTML result</param>
        /// <returns></returns>
        Task<ProviderPackage> GetAsync(EventCaller caller, string inputContent);
        /// <summary>
        /// Provider's name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Provider's author.
        /// </summary>
        string Author { get; set; }
        /// <summary>
        /// Provider's version, reserved for further use.
        /// </summary>
        Version Version { get; set; }
        /// <summary>
        /// Provider's support language, e.g. en-US zh-CN
        /// Currently only single language is supported.
        /// </summary>
        string Bcp47LangTag { get; set; }
        /// <summary>
        /// Provider's priority, in desecending sort.
        /// </summary>
        int Priority { get; set; }
    }
}
