using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Eddo.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Eddo.Permissions.Authorization.Users.Entities;

namespace Eddo.Permissions.Authorization.Users
{
    [Table("Users")]
    public abstract class EddoUserBase : Entity<long>
    {
        /// <summary>
        /// 最大用户名称长度
        /// </summary>
        public const int MaxUserNameLength = 32;

        /// <summary>
        /// 最大邮箱地址长度
        /// </summary>
        public const int MaxEmailAddressLength = 256;

        /// <summary>
        /// 最大名称长度
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// 最大真实名称长度
        /// </summary>
        public const int MaxSurnameLength = 32;

        /// <summary>
        ///最大验证资源长度
        /// </summary>
        public const int MaxAuthenticationSourceLength = 64;

        /// <summary>
        /// 默认管理员名称
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// 最大密码长度
        /// </summary>
        public const int MaxPasswordLength = 128;

        /// <summary>
        /// 最大验证密码长度
        /// </summary>
        public const int MaxPlainPasswordLength = 32;

        /// <summary>
        /// 最大邮箱长度
        /// </summary>
        public const int MaxEmailConfirmationCodeLength = 328;

        /// <summary>
        /// 确认密码长度
        /// </summary>
        public const int MaxPasswordResetCodeLength = 328;

        /// <summary>
        /// 权限资源
        /// </summary>
        [MaxLength(MaxAuthenticationSourceLength)]
        public virtual string AuthenticationSource { get; set; }

        /// <summary>
        /// 最大用户长度
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        /// <summary>
        ///租户编号
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Required]
        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        ///确认邮箱
        /// </summary>
        [StringLength(MaxEmailConfirmationCodeLength)]
        public virtual string EmailConfirmationCode { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [StringLength(MaxPasswordResetCodeLength)]
        public virtual string PasswordResetCode { get; set; }

        /// <summary>
        /// 锁定日期
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///登录次数
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLockoutEnabled { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 是否电话验证
        /// </summary>
        public virtual bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public virtual string SecurityStamp { get; set; }


        /// <summary>
        /// Roles of this user.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserRole> Roles { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserLogin> Logins { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserClaim> Claims { get; set; }

        /// <summary>
        /// Permission definitions for this user.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserPermissionSetting> Permissions { get; set; }

        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        public virtual bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 是否激活
        /// If as user is not active, he/she can not use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }


        public override string ToString()
        {
            return $"[User {Id}] {UserName}";
        }
        protected EddoUserBase()
        {
            IsActive = true;
            IsLockoutEnabled = true;
        }
    }
}
