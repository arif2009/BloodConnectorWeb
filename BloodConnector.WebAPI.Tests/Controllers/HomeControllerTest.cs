using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BloodConnector.WebAPI.Controllers;

namespace BloodConnector.WebAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly ApplicationUserManager _userManager;
        public HomeControllerTest(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_userManager);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
