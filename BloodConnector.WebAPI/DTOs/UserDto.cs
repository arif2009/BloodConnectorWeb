using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration;
using BloodConnector.WebAPI.Interface;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NikeName { get; set; }

        public string Name
        {
            get
            {
                var fullName = $"{FirstName} {LastName}".Trim();

                return string.IsNullOrEmpty(NikeName) ? (string.IsNullOrEmpty(fullName) ? Email : fullName) : NikeName;
            }
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BirthDate => DateOfBirth?.ToShortDateString();
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
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
        public IList<Attachment> Attachments { get; set; }
    }
}