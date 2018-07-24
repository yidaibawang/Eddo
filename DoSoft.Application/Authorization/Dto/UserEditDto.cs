using DoSoft.Core.UserManagerment;
using Eddo.AutoMapper;
using Eddo.Permissions.Authorization.Users;
using System.ComponentModel.DataAnnotations;
namespace DoSoft.Application.Authorization.Dto
{
    [AutoMapTo(typeof(User))]
    public class UserEditDto
    {
        /// <summary>
        /// 如果为空创建新的用户
        /// </summary>
        public long? Id { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }


        [Required]
        [StringLength(EddoUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(EddoUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        // Not used "Required" attribute since empty value is used to 'not change password'
        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool ShouldChangePasswordOnNextLogin { get; set; }

        public virtual bool IsTwoFactorEnabled { get; set; }

        public virtual bool IsLockoutEnabled { get; set; }

    }
}
