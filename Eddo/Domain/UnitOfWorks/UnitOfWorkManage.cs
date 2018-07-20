using Eddo.Dependency;

namespace Eddo.Domain.UnitOfWorks
{
    public class UnitOfWorkManage : IUnitOfWorkManage,ITransientDependency
    {    

        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IIocResolve _iocResolver;
        private readonly IUnitOfWorkDefaultOptions _defaultOptions;
        public UnitOfWorkManage(IIocResolve iocResolver, IUnitOfWorkProvider unitOfWorkProvider, IUnitOfWorkDefaultOptions defaultOptions)
        {
            _iocResolver = iocResolver;
            _unitOfWorkProvider = unitOfWorkProvider;
            _defaultOptions = defaultOptions;
        }
        public IUnitOfWork Current
        {
            get
            {
               return _unitOfWorkProvider.Current;
            }
        }

        public IUnitOfWork Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        public IUnitOfWork Begin(UnitOfWorkOptions options)
        {
            options.FillDefaultsForNonProvidedOptions(_defaultOptions);
            var uow = _iocResolver.Resolve<IUnitOfWork>();
            uow.Begin(options);
 
            _unitOfWorkProvider.Current = uow;
            return uow;
        }
    }
}
