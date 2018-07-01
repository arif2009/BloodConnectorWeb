using System.Collections.Generic;

namespace BloodConnector.WebAPI.Models
{
    public class BloodGroup : BaseEntity
    {
        public int ID { get; set; }
        public string Symbole { get; set; }
        public string GroupName { get; set; }
        public IList<User> Users { get; set; }
    }
}