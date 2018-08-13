using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI;
using SportsStore.WebUI.Areas.Admin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.UnitTests.Routing
{
    [TestClass]
    public class RoutingTests
    {
        [TestMethod]
        public void TestRoutes()
        {
            TestRouteMatch(
                "~/",
                new Dictionary<string, string>
                {
                    ["controller"] = "Products",
                    ["action"] = "List",
                    ["categoryId"] = null,
                    ["page"] = 1.ToString()
                });

            TestRouteMatch(
                "~/Page5",
                new Dictionary<string, string>
                {
                    ["controller"] = "Products",
                    ["action"] = "List",
                    ["categoryId"] = null,
                    ["page"] = 5.ToString()
                });

            TestRouteMatch(
                "~/5",
                new Dictionary<string, string>
                {
                    ["controller"] = "Products",
                    ["action"] = "List",
                    ["categoryId"] = 5.ToString(),
                    ["page"] = 1.ToString()
                });

            TestRouteMatch(
                "~/5/Page5",
                new Dictionary<string, string>
                {
                    ["controller"] = "Products",
                    ["action"] = "List",
                    ["categoryId"] = 5.ToString(),
                    ["page"] = 5.ToString()
                });

            TestRouteMatch(
                "~/Cart/Index",
                new Dictionary<string, string>
                {
                    ["controller"] = "Cart",
                    ["action"] = "Index",
                });

            TestRouteMatch(
                "~/Cart/Index",
                new Dictionary<string, string>
                {
                    ["controller"] = "Cart",
                    ["action"] = "Index",
                });

            TestRouteMatch(
                "~/Products/GetImage",
                new Dictionary<string, string>
                {
                    ["controller"] = "Products",
                    ["action"] = "GetImage",
                });

            TestRouteMatch(
                "~/Admin/Products/Create",
                new Dictionary<string, string>
                {
                    ["controller"] = "Products",
                    ["action"] = "Create",
                });

            TestRouteMatch(
                "~/Admin",
                new Dictionary<string, string>
                {
                    ["controller"] = "Home",
                    ["action"] = "Index",
                });
        }

        private void TestRouteMatch(string url, Dictionary<string, string> routeValues, string httpMethod = "GET")
        {
            var routes = new RouteCollection();
            var adminAreaRegistration = new AdminAreaRegistration();
            var registrationContext = new AreaRegistrationContext("Admin", routes);
            adminAreaRegistration.RegisterArea(registrationContext);
            RouteConfig.RegisterRoutes(routes);
            var httpContext = GetHttpContext(url, httpMethod);


            var result = routes.GetRouteData(httpContext);

            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, routeValues));
        }

        private void TestRouteFail(string url)
        {
            var routes = new RouteCollection();
            var adminAreaRegistration = new AdminAreaRegistration();
            var registrationContext = new AreaRegistrationContext("Admin", routes);
            adminAreaRegistration.RegisterArea(registrationContext);
            RouteConfig.RegisterRoutes(routes);
            var httpContext = GetHttpContext(url);

            var result = routes.GetRouteData(httpContext);

            Assert.IsNull(result?.Route);
        }

        private static bool TestIncomingRouteResult(RouteData routeResult, Dictionary<string, string> routeValues)
        {
            bool Comparer(object v1, object v2) => string.Equals(
                v1?.ToString(),
                v2?.ToString(),
                StringComparison.InvariantCultureIgnoreCase);

            var result = true;
            foreach (var routeValue in routeValues)
            {
                var route = routeResult.Values[routeValue.Key];
                var localResult = Comparer(routeValue.Value, route);
                if (!localResult)
                {
                    result = false;
                }
            }

            return result;
        }

        private static HttpContextBase GetHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(r => r.HttpMethod).Returns(httpMethod);

            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request).Returns(mockRequest.Object);
            mockContext.Setup(c => c.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }
    }
}