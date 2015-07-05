using System.Linq;
using System.Threading.Tasks;
using CortanaBot.Models;
using CortanaBot.Provider;
using CortanaBot.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CortanaTest
{
    [TestClass]
    public class Bot
    {
        [TestMethod, TestCategory("Utils")]
        public void TestQueryPlugin()
        {
            var result = CortanaBot.Utils.InterfaceHelper.TypesImplementingInterface<IContentProvider>();
            Assert.AreEqual(2, result.Count(), "Find impl failed");
        }

        [TestMethod, TestCategory("Networking")]
        public async Task TestBingReq()
        {
            var caller = new EventCaller()
            {
                Id = 0,
                CallerId = 0,
                CallerFirstName = "Ben"
            };
            var textContent = "你好";
            textContent = textContent.Substring(textContent.IndexOf(' ') + 1);
            var bingContent = await CortanaBot.Networking.BingContent.GetAsync(caller, textContent, RequestTemplate.Lang);
            Assert.AreNotEqual("", bingContent,"Request failed");
        }
    }
}
