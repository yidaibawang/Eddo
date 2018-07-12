using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Permissions.EntityFramework;
using DoSoft.Core.Authorization.Roles;
using DoSoft.Core.UserManagerment;
using System.Data.Common;
using System.Data.Entity;

namespace DoSoft.EntityFramework.EntityFramework
{
    public class DoSoftDbContext:EddoPermissionsDbContext<Role,User>
    {
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
