﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using BloodConnector.WebAPI.VM;
using BloodConnector.WebAPI.Filters;
using BloodConnector.WebAPI.Helper;
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
            var role = We.Roles(User.Identity as ClaimsIdentity).First();
            return Ok(_userServices.GetUsers(role));
        }

        public IHttpActionResult Get(string id)
        {
            return Ok(_userServices.GetUserById(id));
        }

        public async Task<IHttpActionResult> Post()
        {
            if (HttpContext.Current.Request.Files.Count == 0) { return Ok();}

            var avater = HttpContext.Current.Request.Files[0];
            var id = HttpContext.Current.Request.Form["id"];

            try
            {
                return Ok(await _userServices.ManageAvater(id, avater));
            }
            catch
            {
                return BadRequest(ModelState);
            }
            
        }

        [ValidateModelState]
        public async Task<IHttpActionResult> Put([Bind(Exclude = "CreatedDate,UpdatedDate")]UserVM userData)
        {
            try
            {
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

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Authorize(Roles = "SuperAdmin")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var userId = User.Identity.GetUserId();
            if (userId == id)
            {
                return BadRequest("You cant kill your self");
            }
            var result = await _userServices.DeleteUserById(id);
            return Ok(result);
        }
    }
}