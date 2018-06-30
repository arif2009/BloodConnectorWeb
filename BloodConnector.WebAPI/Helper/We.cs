using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BloodConnector.Shared.Log;
using BloodConnector.WebAPI.Services;
using BloodConnector.WebAPI.Utilities;
using BloodConnector.WebAPI.VM;

namespace BloodConnector.WebAPI.Helper
{
    public class We
    {
        private static readonly IpInfoClient _ipInfoClient;
        static We()
        {
            _ipInfoClient = new IpInfoClient();
        }

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

        public static async Task<IpInfoViewModel> GetUserLocation()
        {
            IpInfoViewModel data = null;

            var clientIp = HttpContext.Current.Request.UserHostAddress;

            #if DEBUG
            //If localhost or local IP, use Bangladeshi public IP 
            if (clientIp == null || clientIp == "127.0.0.1" || clientIp == "::1" || clientIp.StartsWith("10."))
            {
                clientIp = "103.4.146.91";
            }
            #endif

            // handle multiple ips
            if (clientIp.Contains(","))
                clientIp = clientIp.Split(',').First();

            var response = await _ipInfoClient.GetAsync(clientIp);

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<IpInfoViewModel>();
            }
            else
            {
                Logger.Log($"Exception occured when getting ip. StatusCode={response.StatusCode}");
            }

            return data;
        }
    }
}