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
                    case GenderType.Female: return "Female";
                    case GenderType.Male: return "Male";
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
                    case Utilities.Religion.Islam : return "Islam";
                    case Utilities.Religion.Christianity: return "Christianity";
                    case Utilities.Religion.Hinduism: return "Hinduism";
                    case Utilities.Religion.Buddhism: return "Buddhism";
                    case Utilities.Religion.Other: return "Other";
                    default: return "N/A";
                }

            }
        }
    }
}