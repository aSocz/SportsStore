using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests.HTMLHelpers
{
    [TestClass]
    public class HtmlHelpersTests
    {
        const string BaseUrl = "app/test";

        [TestMethod]
        public void PageLinks_GeneratesValidPageLinks()
        {
            HtmlHelper helper = null;
            var pagingInfo = new PagingInfo
            {
                TotalItems = 10,
                CurrentPage = 3,
                ItemsPerPage = 3
            };

            string UrlProvider(int i) => BaseUrl + "/" + i;

            var tag = helper.PageLinks(pagingInfo, UrlProvider);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""app/test/1"">1</a>" +
                @"<a class=""btn btn-default"" href=""app/test/2"">2</a>" +
                @"<a class=""btn btn-default btn-primary selected"" href=""app/test/3"">3</a>" +
                @"<a class=""btn btn-default"" href=""app/test/4"">4</a>", tag.ToString());
        }
    }
}
