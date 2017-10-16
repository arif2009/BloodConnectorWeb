using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Helper;
using BloodConnector.WebAPI.Interface;
using BloodConnector.WebAPI.Models;

namespace BloodConnector.WebAPI.Services
{
    public class UserServices
    {
        private readonly ApplicationUserManager _userManager;
        public ApplicationDbContext Db { get; private set; }
        public UserServices(ApplicationUserManager userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            Db = db;
        }

        public IList<UserDto> GetUsers()
        {
            var users = _userManager.Users.Include(x => x.BloodGroup).OrderByDescending(y=>y.UserId);
            var userList = Mapper.Map<IEnumerable<UserDto>>(users).ToList();
            return userList;
        }

        public UserDto GetUserById(string userId)
        {
            var id = Convert.ToInt64(userId.Decrypt());
            var user = Db.Users.Include(x=>x.BloodGroup).FirstOrDefault(x=>x.UserId == id);
            return Mapper.Map<UserDto>(user);
        }
    }
}