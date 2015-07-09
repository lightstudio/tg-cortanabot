using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using CortanaBot.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using NGraphics;

namespace CortanaBot.Provider
{
    public class StockProvider : IContentProvider
    {
        public Task<ProviderPackage> GetAsync(EventCaller caller, string inputContent)
        {
            return Task.Run(() =>
            {
                var htmlTree = new HtmlDocument();
                htmlTree.LoadHtml(inputContent);
                var node = htmlTree.DocumentNode.QuerySelector(".b_anno");
                var stockNode = htmlTree.DocumentNode.QuerySelector(".b_hPanel");
                var trendMap = htmlTree.DocumentNode.QuerySelector("svg");
                
                if (node != null && stockNode != null)
                {
                    if (trendMap != null)
                    {
                        var w = trendMap.Attributes["width"];
                        var h = trendMap.Attributes["height"];
                        var trendMapContent = string.Format("<svg width=\"{0}\" height=\"{1}\">{2}</svg>", w.Value, h.Value, trendMap.InnerHtml);
                        
                        var trendMapXml = new XmlDocument();
                        trendMapXml.LoadXml(trendMapContent);

                        var reader = new SvgReader(new StringReader(trendMapContent));
                        var graphic = reader.Graphic;
                        var c =
                            Platforms.Current.CreateImageCanvas(
                                new NGraphics.Size(double.Parse(w.Value), double.Parse(h.Value)), scale: 1);
                        graphic.Draw(c);
                        var trendMapImageStream = new MemoryStream();
                        c.GetImage().SaveAsPng(trendMapImageStream);

                        // Send Package
                        return new ProviderPackage(string.Format("{0} : {1}", node.InnerText, stockNode.InnerText)
                            , trendMapImageStream, caller, Priority);
                    }
                    return new ProviderPackage(string.Format("{0} : {1}", node.InnerText, stockNode.InnerText),
                            caller, Priority);
                }
                    
                return ProviderPackage.ReturnNotAvailablePackage();
            });
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public Version Version { get; set; }

        public string Bcp47LangTag { get; set; }

        public int Priority { get; set; }

        public StockProvider()
        {
            Name = "Cortana stock content provider";
            Author = "Ben.imbushuo Wang";
            Bcp47LangTag = "zh-CN";
            Priority = 3;
        }
    }
}