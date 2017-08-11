using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.ViewModel;
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

        public IList<UserViewModel> GetUsers()
        {
            var users = _userManager.Users.Include(x => x.BloodGroup).Select(y => new UserViewModel
            {
                Email = y.Email,
                PhoneNumber = y.PhoneNumber,
                BloodGroupSymbole = y.BloodGroup.Symbole
            });

            return users.ToList();
        }
    }
}