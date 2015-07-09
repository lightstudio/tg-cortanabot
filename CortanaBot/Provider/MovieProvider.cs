using System;
using System.Threading.Tasks;
using CortanaBot.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace CortanaBot.Provider
{
    public class MovieProvider : IContentProvider
    {
        public Task<ProviderPackage> GetAsync(EventCaller caller, string inputContent)
        {
            return Task.Run(() =>
            {
                var htmlTree = new HtmlDocument();
                htmlTree.LoadHtml(inputContent);
                var node = htmlTree.DocumentNode.QuerySelector(".MSNJVData .b_imagePair");
                if (node != null)
                    return new ProviderPackage(string.Format("{0}", node.InnerText), caller, Priority);
                return ProviderPackage.ReturnNotAvailablePackage();
            });
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public Version Version { get; set; }

        public string Bcp47LangTag { get; set; }

        public int Priority { get; set; }

        public MovieProvider()
        {
            Name = "Cortana movie content provider";
            Author = "Ben.imbushuo Wang";
            Bcp47LangTag = "zh-CN";
            Priority = 2;
        }
    }
}