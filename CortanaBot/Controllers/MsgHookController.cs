using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CortanaBot.Models;
using CortanaBot.Provider;
using CortanaBot.Shared;

namespace CortanaBot.Controllers
{
    public class MsgHookController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            // Setup up web hook
            var tghttpClient = new HttpClient();
            var endpointUrl =
                new Uri(string.Format(RequestTemplate.RpcOpUrl, RequestTemplate.AppId, "setWebhook"));
            var result = await
                tghttpClient.PostAsync(endpointUrl,
                    new FormUrlEncodedContent(new List<KeyValuePair<string,string>>
                    {
                        new KeyValuePair<string, string>("url",RequestTemplate.WebHookUrl)
                    }));
            // Complete request
            tghttpClient.Dispose();
            // Check result
            if (result.IsSuccessStatusCode)
                return new OkResult(new HttpRequestMessage(HttpMethod.Get, RequestTemplate.WebHookUrl));
            return new InternalServerErrorResult(new HttpRequestMessage(HttpMethod.Get, RequestTemplate.WebHookUrl));
        }

        public async Task<bool> InternalSendMessage(string message, string chatId, string messageId)
        {
            var tghttpClient = new HttpClient();
            var endpointUrl =
                        new Uri(string.Format(RequestTemplate.RpcOpUrl, RequestTemplate.AppId, "sendMessage"));
            var result = await
                tghttpClient.PostAsync(endpointUrl, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("chat_id", chatId),
                            new KeyValuePair<string, string>("text", message),
                            new KeyValuePair<string, string>("disable_web_page_preview","true"),
                            new KeyValuePair<string, string>("reply_to_message_id", messageId)
                        }));
            tghttpClient.Dispose();
            return result.IsSuccessStatusCode;
        }

        public async Task<IHttpActionResult> Post([FromBody] ApiReqContent jsonRpcValue)
        {
            // Prep sender
            var caller = new EventCaller
            {
                CallerName = jsonRpcValue.message.from.first_name,
                CallerId = jsonRpcValue.message.chat.id,
                Id = jsonRpcValue.message.message_id
            };

            // Scenario fix
            // 1. Add bot
            if (jsonRpcValue.message.text == null)
            {
                var r = await Networking.Sender.SendMessagePackageAsync(new ProviderPackage("Hey!",caller, 65535));
                if (r)
                    return new OkResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
                return new InternalServerErrorResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
            }
            // 2. Initial
            if (jsonRpcValue.message.text.Contains("/start"))
            {
                var r =
                    await Networking.Sender.SendMessagePackageAsync(new ProviderPackage("Hi, I am Cortana, your truly digital personal assistant.", caller, 65535));
                if (r)
                    return new OkResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
                return new InternalServerErrorResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
            }

            
            // Ask Bing
            // Encode the content
            var textContent = jsonRpcValue.message.text;
            if (textContent.StartsWith("/cortana")) textContent = textContent.Substring(textContent.IndexOf(' ') + 1);
            if (textContent.EndsWith("?") || textContent.EndsWith("？")) textContent = textContent.Remove(textContent.Length - 1);
            if (textContent.EndsWith("!") || textContent.EndsWith("！")) textContent = textContent.Remove(textContent.Length - 1);
            if (textContent.EndsWith(".") || textContent.EndsWith("。")) textContent = textContent.Remove(textContent.Length - 1);
            if (textContent.EndsWith("~") || textContent.EndsWith("/")) textContent = textContent.Remove(textContent.Length - 1);
            var bingContent = await Networking.BingContent.GetAsync(caller, textContent, RequestTemplate.Lang);

            // Normal mode

            var availPlugins = Utils.InterfaceHelper.TypesImplementingInterface<IContentProvider>();
            var results = new List<ProviderPackage>();
            foreach (var contentProvider in availPlugins.Select(pluginType => (IContentProvider) Activator.CreateInstance(pluginType)).Where(contentProvider => contentProvider != null && contentProvider.Bcp47LangTag == RequestTemplate.Lang))
            {
                var resultPkg = await contentProvider.GetAsync(caller, bingContent);
                results.Add(resultPkg);
            }

            var query = from c in results
                where c.Type != ContentType.Error && c.Type != ContentType.NotAvailable
                orderby c.Priority descending
                select c;

            // Determind return type.
            if (query.Any())
            {
                // Send the first one.
                var succResult = await Networking.Sender.SendMessagePackageAsync(query.FirstOrDefault());
                if (succResult)
                    return new OkResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
                return new InternalServerErrorResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
            }

            // Nothing, return search result
            var nResult = await Networking.Sender.SendMessagePackageAsync(
                new ProviderPackage(string.Format(RequestTemplate.RequestUrl, WebUtility.UrlEncode(textContent), RequestTemplate.Lang),
                    caller, 65535));
            if (nResult)
                return new OkResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
            return new InternalServerErrorResult(new HttpRequestMessage(HttpMethod.Post, RequestTemplate.WebHookUrl));
        }
    }
}
