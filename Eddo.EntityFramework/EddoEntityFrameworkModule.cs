using Castle.Core.Internal;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Eddo.Dependency;
using Eddo.EntityFramework.dependency;
using Eddo.EntityFramework.Repositories;
using Eddo.EntityFramework.Uw;
using Eddo.Modules;
using Eddo.Reflection;
using System.Reflection;

namespace Eddo.EntityFramework
{     
    [DependsOn(typeof(EddoKernelModule))]
    public  class EddoEntityFrameworkModule: EddoModule
    {
        public ILogger Logger { get; set; }

        private readonly ITypeFinder _typeFinder;

        public EddoEntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new EntityFrameworkConventionalRegisterer());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component.For(typeof(IDbContextProvider<>))
                    .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                    .LifestyleTransient()
                );

            RegisterGenericRepositories();
        }

        private void RegisterGenericRepositories()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(EddoDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("没有发现类EddoDbContext");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
     
                EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
            }
            using (var dbContextMatcher = IocManager.ResolveAsDisposable<IDbContextTypeMatcher>())
            {
                dbContextMatcher.Object.Populate(dbContextTypes);
            }
      
        }

    }
}
