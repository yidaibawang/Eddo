using Eddo.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.UnitOfWorks
{
    public interface IUnitOfWork:IDisposable
    {
        string Id { get; }
        IUnitOfWork Outer { get; set; }
        void SaveChanges();
        Task SaveChangesAsync();
        bool IsDisposed { get; }
        void Begin(UnitOfWorkOptions options);
        void Commit();
        Task CommitAsync();
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// This event is raised when this UOW is disposed.
        /// </summary>
        event EventHandler Disposed;
        int? GetTenantId();


    }
}
