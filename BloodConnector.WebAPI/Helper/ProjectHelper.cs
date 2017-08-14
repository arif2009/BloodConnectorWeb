using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodConnector.WebAPI.Helper
{
    public class ProjectHelper
    {
        public static string GetUserName(string fName, string lName, string nName, string email)
        {
            var fullName = $"{fName} {lName}".Trim();

            return string.IsNullOrEmpty(nName) ? (string.IsNullOrEmpty(fullName) ? email : fullName) : nName;
        }
    }
}