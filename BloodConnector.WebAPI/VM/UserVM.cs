using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BloodConnector.WebAPI.Helper;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.VM
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NikeName { get; set; }
        public string Name => ProjectHelper.GetUserName(FirstName, LastName, NikeName);
        public string FullName => ProjectHelper.GetFullName(FirstName, LastName, NikeName);
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Blood Group")]
        [Range(1, 8, ErrorMessage = "The {0} field is required.")]
        public int BloodGroupId { get; set; }
        public string BloodGroup { get; set; }
        public int SimilarBlood { get; set; }
        public int BloodGiven { get; set; }
        public string ContactNumber => PhoneNumber;
        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]{5,15})$", ErrorMessage = "Not a valid Contact Number.")]
        public string PhoneNumber { get; set; }
        public string AlternativeContactNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BirthDate => DateOfBirth?.ToShortDateString();
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        [Required]
        public Enums.GenderType? Gender { get; set; }
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
        public Enums.Religion? Religion { get; set; }
        public string ReligionName
        {
            get
            {
                switch (Religion)
                {
                    case Enums.Religion.Islam: return "Islam";
                    case Enums.Religion.Christianity: return "Christianity";
                    case Enums.Religion.Hinduism: return "Hinduism";
                    case Enums.Religion.Buddhism: return "Buddhism";
                    case Enums.Religion.Other: return "Other";
                    default: return "N/A";
                }

            }
        }
        public string PersonalIdentityNum { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastUpdatedDate
        {
            get
            {
                var utcDate = DateTime.SpecifyKind(UpdatedDate, DateTimeKind.Utc);
                return utcDate.ToLocalTime();
            }

        }

        public string Avater
        {
            get
            {
                var userAvater = this.Attachments.FirstOrDefault(x => x.Type == (int) Enums.FileType.Avatar)??new AttachmentVM();
                return userAvater.FileName;
            }
        }

        public IList<AttachmentVM> Attachments { get; set; }

        public string LatLong { get; set; }
    }

    public class UserData
    {
        public string Role { get; set; } = Enums.Role.First(r=>r.Value == "3").Key;
        public List<UserVM> Users { get; set; }
    }
}