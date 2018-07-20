using Web.Core;
using System.Data.Common;
using Eddo.Permissions.EntityFramework;
using Web.Core.Authorization;
using Web.Core.Order;
using System.Data.Entity;

namespace web.ef
{
    public class WebDBContext: EddoPermissionsDbContext<Role,User>
    {
        public virtual IDbSet<OrderInfo> OrderInfo { get; set; }
        public virtual IDbSet<OrderDetail> OrderDetail { get; set; }
        public WebDBContext()
            : base("Default")
        {

        }
        public WebDBContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        /* 
         */
        public WebDBContext(DbConnection dbConnection)
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
