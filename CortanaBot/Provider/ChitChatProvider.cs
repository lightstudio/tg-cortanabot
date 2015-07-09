﻿using System;
using System.Threading.Tasks;
using CortanaBot.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace CortanaBot.Provider
{
    public class ChitChatProvider : IContentProvider
    {
        public Task<ProviderPackage> GetAsync(EventCaller caller, string inputContent)
        {
            return Task.Run(() =>
            {
                var htmlTree = new HtmlDocument();
                htmlTree.LoadHtml(inputContent);
                var node = htmlTree.DocumentNode.QuerySelector(".b_anno");
                return node != null
                    ? new ProviderPackage(node.InnerText, caller, Priority)
                    : ProviderPackage.ReturnNotAvailablePackage();
            });
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public Version Version { get; set; }

        public string Bcp47LangTag { get; set; }

        public int Priority { get; set; }

        public ChitChatProvider()
        {
            Name = "Cortana chit-chat content provider";
            Author = "Ben.imbushuo Wang";
            Bcp47LangTag = "zh-CN";
            Priority = 0;
        }
    }
}