using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CortanaBot.Models;
using CortanaBot.Shared;

namespace CortanaBot.Networking
{
    public sealed class BingContent
    {
        public async static Task<string> GetAsync(EventCaller caller, string queryString, string lang)
        {
            // Send request to Bing
            var httpClient = new HttpClient();
            // Encode the content
            var reqContent = HttpUtility.UrlEncode(queryString);
            var requestUrl = string.Format(RequestTemplate.RequestUrl, reqContent, lang);
            // Remote header
            httpClient.DefaultRequestHeaders.Add("X-Search-MobileClientType", "Hose");
            httpClient.DefaultRequestHeaders.Add("X-Search-AppId", "D41D8CD98F00B204E9800998ECF8427E09AA4958");
            httpClient.DefaultRequestHeaders.Add("X-BM-Client", "BingWP/2.1/assistant");
            httpClient.DefaultRequestHeaders.Add("X-BM-Theme", "000000;1BA1E2");
            httpClient.DefaultRequestHeaders.Add("X-BM-DateFormat", "yyyy/M/d");
            httpClient.DefaultRequestHeaders.Add("X-BM-RegionalSettings", "zh-CN");
            httpClient.DefaultRequestHeaders.Add("X-BM-DeviceOrientation", "0");
            httpClient.DefaultRequestHeaders.Add("X-COMMON-PARTNERCODE", "NOKMSB");
            httpClient.DefaultRequestHeaders.Add("X-BM-MO", "000-HK");
            httpClient.DefaultRequestHeaders.Add("X-BM-CBT", "1435931517761");
            httpClient.DefaultRequestHeaders.Add("X-BM-Bandwidth", "High");
            httpClient.DefaultRequestHeaders.Add("X-BM-Theme", "000000;1BA1E2");
            httpClient.DefaultRequestHeaders.Add("X-Search-Location", "lat:30.281360;long:120.123233;ts:1435931517;re:124.000000");
            httpClient.DefaultRequestHeaders.Add("X-BM-DeviceDpi", "96");
            httpClient.DefaultRequestHeaders.Add("X-BM-DeviceScale", "1.000000");
            httpClient.DefaultRequestHeaders.Add("X-BM-DeviceDimensionsLogical", "480x800");
            httpClient.DefaultRequestHeaders.Add("X-DeviceId", "1CB009C462E10B208FAFE968E04058C07F2CD3DFC9E2DF7F0985563621B90E95");
            httpClient.DefaultRequestHeaders.Add("X-BM-NetworkType", "Wi-Fi");
            httpClient.DefaultRequestHeaders.Add("X-BM-BuildNumber", " Windows Phone 6.3.0.0.9651");
            httpClient.DefaultRequestHeaders.Add("X-BM-UserDisplayName", caller.CallerFirstName);
            httpClient.DefaultRequestHeaders.Add("AppContext", "{\"Stores\":\"Zest:NOKIA\",\"CatalogCountry\":\"CN\",\"OverrideCountry\":\"CN\",\"DisplayLanguageLocale\":\"zh-CN\",\"DeviceConfig\":\"268481538\",\"DeviceTypeStr\":\"winphone8.10\",\"DeviceModel\":\"RM-1019_1027\",\"SafeSearchRating\":\"99:1\"}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Mobile; Windows Phone 8.1; Android 4.0; ARM; Trident/7.0; Touch; rv:11.0; IEMobile/11.0; NOKIA; Lumia 530 Dual SIM) like iPhone OS 7_0_3 Mac OS X AppleWebKit/537 (KHTML, like Gecko) Mobile Safari/537");
            httpClient.DefaultRequestHeaders.Add("UA-CPU", "ARM");
            // Get
            var bingContent = await httpClient.GetAsync(new Uri(requestUrl));
            httpClient.Dispose();
            if (bingContent.IsSuccessStatusCode)
            {
                return await bingContent.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}