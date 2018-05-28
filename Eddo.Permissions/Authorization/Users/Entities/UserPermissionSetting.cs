using Eddo.Permissions.Authorization.Roles.Entities;

namespace Eddo.Permissions.Authorization.Users.Entities
{
    /// <summary>
    /// 用户资源设置
    /// </summary>
    public class UserPermissionSetting: PermissionSetting
    {
        public virtual long UserId { get; set; }
    }
}
