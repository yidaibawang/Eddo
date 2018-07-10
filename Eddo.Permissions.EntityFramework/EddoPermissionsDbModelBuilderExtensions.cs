using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;
using Eddo.Permissions.Authorization.Users.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.EntityFramework
{
    public static class EddoPermissionsDbModelBuilderExtensions
    {
        public static void ChangeEddoTablePrefix< TRole, TUser>(this DbModelBuilder modelBuilder, string prefix, string schemaName = null)
            where TRole : EddoRole<TUser>
            where TUser : EddoUser<TUser>
        {
            prefix = prefix ?? "";

       
            SetTableName<PermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<RolePermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<UserPermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<TRole>(modelBuilder, prefix + "Roles", schemaName);
            SetTableName<UserLogin>(modelBuilder, prefix + "UserLogins", schemaName);
            SetTableName<UserLoginAttempt>(modelBuilder, prefix + "UserLoginAttempts", schemaName);
            SetTableName<UserRole>(modelBuilder, prefix + "UserRoles", schemaName);
            SetTableName<TUser>(modelBuilder, prefix + "Users", schemaName);

            SetTableName<UserClaim>(modelBuilder, prefix + "UserClaims", schemaName);
        }

        private static void SetTableName<TEntity>(DbModelBuilder modelBuilder, string tableName, string schemaName)
            where TEntity : class
        {
            if (schemaName == null)
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName);
            }
            else
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName, schemaName);
            }
        }
    }
}
