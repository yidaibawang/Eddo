using Eddo.Domain.Entities;
using Eddo.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
namespace Eddo.Permissions.Authorization.Users
{
    [Table("EddoUserRoles")]
    public  class UserRole:CreationAuditedEntity<long>, IMayHaveTenant
    {   
        /// <summary>
        /// 租户编号
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// 初始化话
        /// </summary>
        public UserRole()
        {

        }
        /// <summary>
        ///初始化
        /// </summary>
        public UserRole(int? tenantId, long userId, int roleId)
        {
            TenantId = tenantId;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
