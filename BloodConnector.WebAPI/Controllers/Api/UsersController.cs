using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Filters;
using BloodConnector.WebAPI.Services;
using Microsoft.AspNet.Identity;

namespace BloodConnector.WebAPI.Controllers.Api
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/users")]
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

        [ValidateModelState]
        public async Task<IHttpActionResult> Put([Bind(Exclude = "CreatedDate,UpdatedDate")]UserDto userData)
        {
            try
            {
                /*if (await _userServices.EmailAlreadyExist(userData.UserId, userData.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return BadRequest(ModelState);
                }*/

                return Ok(await _userServices.UpdateUser(userData));
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                //ModelState.AddModelError("Exception", ex.Message);
                ModelState.AddModelError("Network", "Network problem !");
                return BadRequest(ModelState);
            }
        }
    }
}