using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.ViewModel
{
    public class UserViewModel:User
    {
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enums.GenderType.Female: return "Female";
                    case Enums.GenderType.Male: return "Male";
                    default: return "N/A";
                }

            }
        }

        public string ReligionName
        {
            get
            {
                switch (Religion)
                {
                    case Enums.Religion.Islam : return "Islam";
                    case Enums.Religion.Christianity: return "Christianity";
                    case Enums.Religion.Hinduism: return "Hinduism";
                    case Enums.Religion.Buddhism: return "Buddhism";
                    case Enums.Religion.Other: return "Other";
                    default: return "N/A";
                }

            }
        }
    }
}