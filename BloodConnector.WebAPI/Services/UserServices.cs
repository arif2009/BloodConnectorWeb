using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using BloodConnector.WebAPI.Helper;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Utilities;
using BloodConnector.WebAPI.VM;

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

        public IList<UserVM> GetUsers()
        {
            var users = _userManager.Users.Include(x => x.BloodGroup).OrderByDescending(y=>y.UserId);
            var userList = Mapper.Map<IEnumerable<UserVM>>(users).ToList();
            return userList;
        }

        public UserVM GetUserById(string userId)
        {
            var id = Convert.ToInt64(userId.Decrypt());
            var user = Db.Users.Include(x=>x.BloodGroup).Include(y=>y.Country).Include(z=>z.Attachments).FirstOrDefault(x=>x.UserId == id);
            return Mapper.Map<UserVM>(user);
        }

        public async Task<bool> EmailAlreadyExist(string userId, string email)
        {
            var id = Convert.ToInt64(userId.Decrypt());
            return await Db.Users.AnyAsync(x => x.Email == email && x.UserId != id);
        }

        public async Task<UserVM> UpdateUser(UserVM data)
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

        public async Task<int> ManageAvater(string userId, HttpPostedFile avater)
        {
            var uploadConfig = FileHelper.Upload(avater, Enums.FileType.Avatar);

            var userAvater = Db.Attachment.FirstOrDefault(x => x.Type == (int) Enums.FileType.Avatar && x.UserId==userId) ?? new Attachment();
            var oldPath = userAvater.FileName;
            userAvater.UserId = userId;
            userAvater.FileName = uploadConfig.FileNameWithPath;
            userAvater.Type = (int) Enums.FileType.Avatar;
            userAvater.FileguId = uploadConfig.FileguId;
            userAvater.ContentType = avater.ContentType;
            userAvater.Size = avater.ContentLength/1024; //Make byte to KB
            userAvater.LastUpdatedOn = DateTime.UtcNow;

            if (userAvater.ID == default(int))
            {
                Db.Entry(userAvater).State = EntityState.Added;
                Db.Attachment.Add(userAvater);
            }
            else
            {
                FileHelper.Delete(oldPath);
                Db.Entry(userAvater).State = EntityState.Modified;
            }

            return await Db.SaveChangesAsync();
        }
    }
}