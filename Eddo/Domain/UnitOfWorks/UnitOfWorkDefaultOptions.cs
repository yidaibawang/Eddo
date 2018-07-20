using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.UnitOfWorks
{
    internal class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsTransactional { get; set; }

        public TimeSpan? Timeout { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public TransactionScopeOption? Scope
        {
            get;set;
            
        }

        public UnitOfWorkDefaultOptions()
        {
            IsTransactional = true;
        }
    }
}
