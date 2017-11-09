using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.VM;

namespace BloodConnector.WebAPI.Helper
{
    public static class Extentions
    {
        public static IHtmlString WeComment(this HtmlHelper html, string comment)
        {
            #if DEBUG
            // Fix for jQuery / JSON problem:
            //comment = comment.Replace("[", "¤-- ").Replace("]", " --¤");
            return html.Raw("<!-- " + comment + " -->");
            #else
            return html.Raw(string.Empty);
            #endif
        }

        public static byte[] ToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteToString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        public static string Encrypt(this string text)
        {
            return Convert.ToBase64String(text.ToByteArray());
        }

        public static string Decrypt(this string text)
        {
            return Convert.FromBase64String(text).ByteToString();
        }

        public static bool Equal(this UserVM vm, User user)
        {
            return string.Equals(vm.FirstName, user.FirstName) &&
                   string.Equals(vm.LastName, user.LastName) &&
                   string.Equals(vm.NikeName, user.NikeName) &&
                   string.Equals(vm.Email, user.Email) &&
                   vm.BloodGiven == user.BloodGiven &&
                   string.Equals(vm.PhoneNumber, user.PhoneNumber) &&
                   vm.BloodGroupId == user.BloodGroupId &&
                   vm.DateOfBirth.Equals(user.DateOfBirth) &&
                   string.Equals(vm.Address, user.Address) &&
                   string.Equals(vm.PostCode, user.PostCode) &&
                   string.Equals(vm.City, user.City) &&
                   vm.Gender == user.Gender &&
                   vm.CountryId == user.CountryId &&
                   string.Equals(vm.PersonalIdentityNum, user.PersonalIdentityNum);
        } 
    }
}