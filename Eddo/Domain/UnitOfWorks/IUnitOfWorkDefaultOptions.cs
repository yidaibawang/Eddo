using System;
using System.Transactions;

namespace Eddo.Domain.UnitOfWorks
{
    public  interface IUnitOfWorkDefaultOptions
    {
        TransactionScopeOption? Scope { get; set; }
        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsTransactional { get; set; }

        TimeSpan? Timeout { get; set; }

        IsolationLevel? IsolationLevel { get; set; }
    }
}
