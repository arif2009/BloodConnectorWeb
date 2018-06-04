using System.ComponentModel.DataAnnotations;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [StringLength(20, ErrorMessage = "{0} is too long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "{0} is too long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "{0} is too long.")]
        [Display(Name = "Display Name")]
        public string NikeName { get; set; }

        [Range(0, 50, ErrorMessage = "{0} too long.")]
        [Display(Name = "Blood Given")]
        public int BloodGiven { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        [Range(1, 8, ErrorMessage = "The {0} field is required.")]
        public int BloodGroupId { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]{5,15})$", ErrorMessage = "Not a valid Contact Number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at most 12 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AppRegisterBindingModel
    {
        [Required]
        [StringLength(40, ErrorMessage = "{0} is too long.")]
        public string Name { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        public string NikeName { get; set; }

        public Enums.GenderType? Gender { get; set; }

        [Range(0, 50, ErrorMessage = "{0} too long.")]
        [Display(Name = "Blood Given")]
        public int BloodGiven { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        [Range(1, 8, ErrorMessage = "The {0} field is required.")]
        public int BloodGroupId { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]{5,15})$", ErrorMessage = "Not a valid Contact Number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at most 12 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at most 12 characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public string Code { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
