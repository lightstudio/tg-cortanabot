using System;
using System.Threading.Tasks;
using CortanaBot.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace CortanaBot.Provider
{
    public class CalculcatorProvider : IContentProvider
    {
        public Task<ProviderPackage> GetAsync(EventCaller caller, string inputContent)
        {
            return Task.Run(() =>
            {
                var htmlTree = new HtmlDocument();
                htmlTree.LoadHtml(inputContent);
                var node = htmlTree.DocumentNode.QuerySelector(".b_anno");
                var calcNode = htmlTree.DocumentNode.QuerySelector(".b_emphText");
                if (node != null && calcNode != null)
                    return new ProviderPackage(string.Format("{0} : {1}", node.InnerText, calcNode.InnerText), caller, Priority);
                return ProviderPackage.ReturnNotAvailablePackage();
            });
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public Version Version { get; set; }

        public string Bcp47LangTag { get; set; }

        public int Priority { get; set; }

        public CalculcatorProvider()
        {
            Name = "Cortana calculcator content provider";
            Author = "Ben.imbushuo Wang";
            Bcp47LangTag = "zh-CN";
            Version = new Version(1, 0, 0, 0);
            Priority = 1;
        }
    }
}