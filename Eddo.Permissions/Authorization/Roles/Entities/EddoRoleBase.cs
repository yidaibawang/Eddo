using Eddo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization.Roles.Entities
{
    [Table("EddoRoles")]
    public abstract class EddoRoleBase : Entity<int>, IMayHaveTenant
    {
        /// <summary>
        /// 最大显示名称长度
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// 最大名称长度
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// 租户编号
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 是否静态
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        ///是否默认
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        ///权限列表
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermissionSetting> Permissions { get; set; }

        protected EddoRoleBase()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        protected EddoRoleBase(int? tenantId, string displayName)
            : this()
        {
            TenantId = tenantId;
            DisplayName = displayName;
        }

        protected EddoRoleBase(int? tenantId, string name, string displayName)
            : this(tenantId, displayName)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[Role {Id}, Name={Name}]";
        }
    }
}
