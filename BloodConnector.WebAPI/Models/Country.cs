using System.Collections.Generic;

namespace BloodConnector.WebAPI.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TowLetterCode { get; set; }
        public string PhonePrefix { get; set; }
        public IList<User> Users { get; set; }
    }
}