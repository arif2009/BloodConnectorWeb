using System.Web.Http;
using BloodConnector.WebAPI.Services;

namespace BloodConnector.WebAPI.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/users")]
    //[EnableCors("http://localhost:14717,http://localhost:2102", "*", "*")]
    public class UsersController : ApiController
    {
        private readonly UserServices _userServices;

        public UsersController(UserServices userManager)
        {
            _userServices = userManager;
        }

        // GET api/orders
        public IHttpActionResult Get()
        {
            return Ok(_userServices.GetUsers());
        }
   }
}