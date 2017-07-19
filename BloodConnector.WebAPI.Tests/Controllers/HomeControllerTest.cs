using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BloodConnector.WebAPI;
using BloodConnector.WebAPI.Controllers;

namespace BloodConnector.WebAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
