using Eddo.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eddo.Permissions.Authorization.Users
{
    //实体类用于保存通过外部授权服务授权的用户的登入信息
    [Table("UserLogin")]
    public class UserLogin:Entity<long>
    {
        /// <summary>
        ///LoginProvider最大长度
        /// </summary>
        public const int MaxLoginProviderLength = 128;

        /// <summary>
        /// LoginProvider编码最大长度
        /// </summary>
        public const int MaxProviderKeyLength = 256;

        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 登录代理
        /// </summary>
        [Required]
        [MaxLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// LoginProvider编码
        /// </summary>
        [Required]
        [MaxLength(MaxProviderKeyLength)]
        public virtual string ProviderKey { get; set; }

        public UserLogin()
        {

        }

        public UserLogin(int? tenantId, long userId, string loginProvider, string providerKey)
        {
            TenantId = tenantId;
            UserId = userId;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}
