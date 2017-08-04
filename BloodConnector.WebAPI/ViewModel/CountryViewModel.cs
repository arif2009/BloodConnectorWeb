using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.ViewModel
{
    public class CountryViewModel:User
    {
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enums.GenderType.Female: return "[[[Female]]]";
                    case Enums.GenderType.Male: return "[[[Male]]]";
                    default: return "[[[N/A]]]";
                }

            }
        }
    }
}