using System.Web.Http;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Models;
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

        public IHttpActionResult Get(string id)
        {
            return Ok(_userServices.GetUserById(id));
        }

        public IHttpActionResult Put(User userData)
        {
            return null;
        }
   }
}