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
                    case GenderType.Female: return "[[[Female]]]";
                    case GenderType.Male: return "[[[Male]]]";
                    default: return "[[[N/A]]]";
                }

            }
        }
    }
}