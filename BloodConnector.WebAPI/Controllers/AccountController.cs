using System.Web.Mvc;

namespace BloodConnector.WebAPI.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string email)
        {
            //Leder till en  vy som är en egen mvvm-sida
            return code == null ? View("Error") : View();
        }
    }
}