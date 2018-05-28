using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization.Users
{
    public  interface IUserPermissionStore<in TUser> where TUser:EddoUser<TUser>
    {
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        Task AddPermissionAsync(TUser user, PermissionGrantInfo permissionGrant);

        /// <summary>
        ///移除资源
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        Task RemovePermissionAsync(TUser user, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// 获取用户资源
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of permission setting informations</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(long userId);

        /// <summary>
        /// 是否存在资源
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        /// <returns></returns>
        Task<bool> HasPermissionAsync(long userId, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// 删除所有资源设置
        /// </summary>
        /// <param name="user">User</param>
        Task RemoveAllPermissionSettingsAsync(TUser user);
    }
}
