using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BloodConnector.WebAPI.Services
{
    public class UserServices
    {
        private readonly ApplicationUserManager _userManager;
        public UserServices(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public IList<UserDto> GetUsers()
        {

            var users = _userManager.Users.Include(x => x.BloodGroup).Select(y => new UserDto
            {
                Email = y.Email,
                ContactNumber = y.PhoneNumber,
                BloodGroup = y.BloodGroup.Symbole
            });

            return users.ToList();
        }
    }
}