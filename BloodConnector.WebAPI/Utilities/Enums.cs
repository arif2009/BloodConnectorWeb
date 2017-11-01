using System.Collections.Generic;

namespace BloodConnector.WebAPI.Utilities
{
    public static class Enums
    {
        public enum GenderType : int
        {
            Female = 0,
            Male = 1
        }

        public enum Religion : int
        {
            Islam = 1,
            Christianity = 2,
            Hinduism = 3,
            Buddhism = 4,
            Other = 5
        }

        public enum FileType
        {
            Avatar = 1,
            Resume = 2,
            Document = 3
        }

        public static IDictionary<string, string> Role = new Dictionary<string, string>
        {
            {"SuperAdmin", "1"},
            {"Admin", "2"},
            {"User", "3"}
        };
    }
}