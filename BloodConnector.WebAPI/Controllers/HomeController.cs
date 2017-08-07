using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace BloodConnector.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        public HomeController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> UserAlreadyExistsAsync(string Email)
        {
            var result = await _userManager.FindByEmailAsync(Email);
            return Json(result == null, JsonRequestBehavior.AllowGet);
        }
    }
}
