using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.Helper
{
    public class We
    {
        /// <summary>
        /// var role = We.Roles(User.Identity as ClaimsIdentity);
        /// </summary>
        /// <returns>List of roles</returns>
        public static List<string> Roles(ClaimsIdentity claimsIdentity)
        {
            var claims = claimsIdentity.Claims;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            return roles;
        }
        /// <summary>
        /// bool isSuperAdmin = We.IsSuperAdmin(User.Identity as ClaimsIdentity);
        /// </summary>
        /// <returns>true/false</returns>
        public static bool IsSuperAdmin(ClaimsIdentity claimsIdentity)
        {
            var roles = Roles(claimsIdentity);
            return roles.Contains(Enums.Role.First(r => r.Value == "1").Key);
        }
    }
}