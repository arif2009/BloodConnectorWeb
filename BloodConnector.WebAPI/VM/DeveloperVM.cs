using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodConnector.WebAPI.VM
{
    public class DeveloperVM
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
    }
}