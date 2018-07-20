using DoSoft.Core;
using DoSoft.EntityFramework.EntityFramework;
using Eddo.Modules;
using Eddo.Permissions.EntityFramework;
using System.Data.Entity;
using System.Reflection;

namespace DoSoft.EntityFramework
{
    [DependsOn(typeof(EddoPermissionsEntityFrameworkModule), typeof(DoSoftCoreModule))]
    public class DoSoftEntityFrameworkModule:EddoModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DoSoftDbContext>());
            Configuration.DefaultNameOrConnectionString = "Default";
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
