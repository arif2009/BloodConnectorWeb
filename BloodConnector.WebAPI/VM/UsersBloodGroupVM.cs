using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodConnector.WebAPI.VM
{
    public class UsersBloodGroupVM
    {
        public long TotalNumberOfUser { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }

    public class Group
    {
        public string GroupSymbole { get; set; }
        public long NumberOfGroupUser { get; set; }
    }
}