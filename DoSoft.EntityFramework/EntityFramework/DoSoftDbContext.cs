using DoSoft.Core.Authorization.Roles;
using DoSoft.Core.Security.Entities;
using DoSoft.Core.UserManagerment;
using Eddo.Permissions.EntityFramework;
using System.Data.Common;
using System.Data.Entity;

namespace DoSoft.EntityFramework.EntityFramework
{
    public class DoSoftDbContext : EddoPermissionsDbContext<Role, User>
    {
        public virtual IDbSet<Module> Modules{ get;set;}
        public DoSoftDbContext()
            : base("Default")
        {

        }
        public DoSoftDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        /* 
         */
        public DoSoftDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.ChangeEddoTablePrefix<Role, User>("Eddo");
            base.OnModelCreating(modelBuilder);
        }

    }
}
