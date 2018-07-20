using Eddo.Extensions;
using Eddo.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.UnitOfWorks
{
    public abstract class UnitOfWorkBase : IUnitOfWork,IDisposable
    {
        public string Id { get; set; }
        public IEddoSession EddoSession { protected get; set; }
       private int? _tenantId;
        public  IUnitOfWork Outer
        {
            get; set;
        }
        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        /// <summary>
        /// 开始事务
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// 提交事务
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// 异步提交
        /// </summary>
        protected abstract Task CompleteUowAsync();


        public event EventHandler Completed;

        /// <inheritdoc/>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <inheritdoc/>
        public event EventHandler Disposed;

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();
        protected IConnectionStringResolver ConnectionStringResolver { get; }
        public UnitOfWorkOptions Options { get; private set; }

        private Exception _exception;
        protected UnitOfWorkBase(IConnectionStringResolver connectionStringResolver)
        {
            Id = Guid.NewGuid().ToString("N");
            ConnectionStringResolver = connectionStringResolver;
            EddoSession = NullEddoSession.Instance;
            Options = new UnitOfWorkOptions();

        }
        public void Begin(UnitOfWorkOptions options)
        {
            Options = options;
            BeginUow();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                CompleteUow();
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }

        }

        public async Task CommitAsync()
        {
            try
            {
                await CompleteUowAsync();
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// </summary>
        /// <param name="exception">Exception that cause failure</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }
        public bool IsDisposed { get; private set; }
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            OnFailed(_exception);
            DisposeUow();
        }

        public int? GetTenantId()
        {
            return _tenantId;
        }


    }
}
