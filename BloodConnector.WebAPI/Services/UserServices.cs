using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var users = _userManager.Users.Include(x => x.BloodGroup);
            var userList = Mapper.Map<IEnumerable<UserDto>>(users).ToList();
            return userList;
        }
    }
}