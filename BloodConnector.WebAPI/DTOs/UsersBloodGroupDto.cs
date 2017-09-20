using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodConnector.WebAPI.DTOs
{
    public class UsersBloodGroupDto
    {
        public long TotalNumberOfUser { get; set; }
        public IEnumerable<Group> Group { get; set; }
    }

    public class Group
    {
        public string GroupSymbole { get; set; }
        public long NumberOfGroupUser { get; set; }
    }
}