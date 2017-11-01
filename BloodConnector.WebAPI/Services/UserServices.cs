using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Helper;
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
            var user = Db.Users.Include(x=>x.BloodGroup).Include(y=>y.Country).Include(z=>z.Attachments).FirstOrDefault(x=>x.UserId == id);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<bool> EmailAlreadyExist(string userId, string email)
        {
            var id = Convert.ToInt64(userId.Decrypt());
            return await Db.Users.AnyAsync(x => x.Email == email && x.UserId != id);
        }

        public async Task<UserDto> UpdateUser(UserDto data)
        {
            //Step-1 : Retrieve
            var id = Convert.ToInt64(data.UserId.Decrypt());
            var user = Db.Users.First(x => x.UserId == id);

            //Step-2 : Update
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.NikeName = data.NikeName;
            user.BloodGiven = data.BloodGiven;
            user.PhoneNumber = data.PhoneNumber;
            //user.Email = data.Email;
            //user.UserName = data.Email;
            user.BloodGroupId = data.BloodGroupId;
            user.DateOfBirth = data.DateOfBirth;
            user.Address = data.Address;
            user.PostCode = data.PostCode;
            user.City = data.City;
            user.Gender = data.Gender;
            user.CountryId = data.CountryId;
            user.PersonalIdentityNum = data.PersonalIdentityNum;
            user.UpdatedDate = DateTime.UtcNow;
            data.UpdatedDate = DateTime.UtcNow;

            //Step-3 : Save
            //Mark entity as modified
            Db.Entry(user).State = EntityState.Modified;
            await Db.SaveChangesAsync();
            return data;
        }
    }
}