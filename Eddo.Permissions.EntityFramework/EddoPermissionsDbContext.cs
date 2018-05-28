using Eddo.EntityFramework;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;
using System.Data.Entity;
using Eddo.Permissions.Authorization.Users.Entities;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Eddo.Permissions.EntityFramework
{
    public abstract class EddoPermissionsDbContext<TRole,TUser>:EddoDbContext
        where TUser:EddoUser<TUser>
        where TRole:EddoRole<TUser>   
    {
        public virtual IDbSet<TUser> Users { get; set; }
        public virtual IDbSet<TRole> Roles { get; set; }
        public virtual IDbSet<UserClaim> UserClaim { get; set; }
        public virtual IDbSet<UserLogin> UserLogin { get; set; }
        public virtual IDbSet<UserLoginAttempt> UserLoginAttempt { get; set; }

        public virtual IDbSet<UserRole> UserRole { get; set; }
        public virtual IDbSet<PermissionSetting> Permissions { get; set; }
        public virtual IDbSet<RolePermissionSetting> RolePermissions { get; set; }
        public virtual IDbSet<UserPermissionSetting> UserPermissions { get; set; }
  

        protected EddoPermissionsDbContext()
        {

        }
        protected EddoPermissionsDbContext(string nameOrConnectionString) :base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected EddoPermissionsDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected EddoPermissionsDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {

        }

        protected EddoPermissionsDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected EddoPermissionsDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {

        }
    }
}
