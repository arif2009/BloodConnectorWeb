using System.Web.Mvc;

namespace BloodConnector.WebAPI.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager; //every thing that needs the old UserManager property references this now
        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string email)
        {
            //Leder till en  vy som är en egen mvvm-sida
            return code == null ? View("Error") : View();
        }
    }
}