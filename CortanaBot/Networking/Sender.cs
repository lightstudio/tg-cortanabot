using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CortanaBot.Models;
using CortanaBot.Shared;

namespace CortanaBot.Networking
{
    public sealed class Sender
    {
        private async static Task<bool> SendMessageTextOnlyAsync(string message, string chatId, string messageId)
        {
            var tghttpClient = new HttpClient();
            // Send Message
            var endpointUrl =
                        new Uri(string.Format(RequestTemplate.RpcOpUrl, RequestTemplate.AppId, "sendMessage"));
            var result = await
                tghttpClient.PostAsync(endpointUrl, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("text", message),
                            new KeyValuePair<string, string>("chat_id", chatId),
                            new KeyValuePair<string, string>("disable_web_page_preview","true"),
                            new KeyValuePair<string, string>("reply_to_message_id", messageId)
                        }));
            tghttpClient.Dispose();
            var content = await result.Content.ReadAsStringAsync();
            return result.IsSuccessStatusCode;
        }

        private async static Task<bool> SendMessageWithImageAsync(string message, string chatId, string messageId,
            Stream imageStream)
        {
            var tghttpClient = new HttpClient();
            imageStream.Seek(0, SeekOrigin.Begin);
            var buf = ReadFully(imageStream);

            var imageContent = new ByteArrayContent(buf);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");

            // Send Message, as well as image
            var endpointUrl =
                        new Uri(string.Format(RequestTemplate.RpcOpUrl, RequestTemplate.AppId, "sendPhoto"));
            var content = new MultipartFormDataContent();
            /*content.Add(new FormUrlEncodedContent(new List<KeyValuePair<string,string>>
            {
                new KeyValuePair<string, string>("chat_id", chatId),
                new KeyValuePair<string, string>("reply_to_message_id", messageId),
                new KeyValuePair<string, string>("caption", message)
            }));*/
            content.Add(imageContent, "photo", string.Format("{0}.png", Guid.NewGuid()));
            content.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(chatId)),"chat_id");
            content.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(messageId)), "reply_to_message_id");
            content.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(message)), "aptiond");
            var result = await
                tghttpClient.PostAsync(endpointUrl, content);
            var content2 = await result.Content.ReadAsStringAsync();
            return result.IsSuccessStatusCode;
        }

        public static Task<bool> SendMessagePackageAsync(ProviderPackage package)
        {
            switch (package.Type)
            {
                case ContentType.TextOnly:
                    return SendMessageTextOnlyAsync(package.Text, package.Caller.CallerId.ToString(), package.Caller.Id.ToString());
                case ContentType.TextWithImage:
                    return SendMessageWithImageAsync(package.Text, package.Caller.CallerId.ToString(), package.Caller.Id.ToString(), package.ImageStream);
                case ContentType.ImageOnly:
                    return SendMessageWithImageAsync("", package.Caller.CallerId.ToString(), package.Caller.Id.ToString(), package.ImageStream);
            }
            throw new NotImplementedException();
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}