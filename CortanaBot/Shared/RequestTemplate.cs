using System.Web.Configuration;

namespace CortanaBot.Shared
{
    public static class RequestTemplate
    {
        public const string RequestUrl = "https://www.bing.com/search?q={0}&speech=1&input=2&usersaid={0}&form=SBCLIK2&mkt={1}&setlang={1}";
        public const string RpcOpUrl = "https://api.telegram.org/{0}/{1}";

        public static string WebHookUrl;
        public static string AppId;
        public static string Lang;

        static RequestTemplate()
        {
            WebHookUrl = WebConfigurationManager.AppSettings["Telegram:WebHookUrl"];
            AppId = WebConfigurationManager.AppSettings["Telegram:AppId"];
            Lang = WebConfigurationManager.AppSettings["Bot:TargetLanguage"];
        }
    }
}