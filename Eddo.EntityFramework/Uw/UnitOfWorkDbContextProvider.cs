using Eddo.Dependency;
using Eddo.Domain.UnitOfWorks;
using System.Data.Entity;

namespace Eddo.EntityFramework.Uw
{
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
       where TDbContext : DbContext
    {
        /// <summary>
        /// 获取数据库处理上下文
        /// </summary>
        public TDbContext DbContext { get {
                //判断工作单位时候为空
                if (_currentUnitOfWorkProvider.Current == null)
                {
                    _currentUnitOfWorkProvider.Current = _IocManager.Resolve<IUnitOfWork>();
                }
                return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>();
            } }

        private readonly IUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly IocManager _IocManager;
        /// <summary>
        /// 创建数据工作单元代理类
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public UnitOfWorkDbContextProvider(IUnitOfWorkProvider currentUnitOfWorkProvider, IocManager iocManager)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _IocManager = iocManager;
        }
    }
}
