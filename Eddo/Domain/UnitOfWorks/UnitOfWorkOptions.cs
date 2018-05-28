using System;
using System.Collections.Generic;

using System.Transactions;

namespace Eddo.Domain.UnitOfWorks
{
    public  class UnitOfWorkOptions
    {
        public TransactionScopeOption? Scope { get; set; }
        /// <summary>
        /// 不对称事务是否开启
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        ///超时时间
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 事务的级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 是否开启同步
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }
        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }
            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }
            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }

    }
}
