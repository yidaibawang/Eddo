using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.UnitOfWorks
{
    public interface IUnitOfWorkManage
    {
        /// <summary>
        /// 获取工作单元提供者
        /// </summary>
        IUnitOfWork Current { get; }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>工作单元</returns>
        IUnitOfWork Begin();

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>工作单元</returns>
        IUnitOfWork Begin(UnitOfWorkOptions options);
    }
}
