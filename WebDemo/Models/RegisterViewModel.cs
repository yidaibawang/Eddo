using Eddo.Extensions;
using Eddo.Permissions.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebDemo.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(EddoUserBase.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(EddoUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(EddoUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(EddoUserBase.MaxPlainPasswordLength)]
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        
        public bool IsExternalLogin { get; set; }

        public string ExternalLoginAuthSchema { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserName.IsNullOrEmpty())
            {
                var emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                if (!UserName.Equals(EmailAddress) && emailRegex.IsMatch(UserName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }
    }
}