using Castle.Core;
using Castle.Core.Logging;
using Eddo.Dependency;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

namespace Eddo.Domain.UnitOfWorks
{
    public class CurrentUnitOfWorkProvider : IUnitOfWorkProvider, ISingletonDependency
    {
        [DoNotWire]
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(Logger); }
            set { SetCurrentUow(value, Logger); }
        }
        public ILogger Logger { get; set; }
        private readonly IocManager _iocManager;
        private const string ContextKey = "Eddo.UnitOfWork.Current";

        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();
        public CurrentUnitOfWorkProvider(IocManager iocManager)
        {
   
            Logger = NullLogger.Instance;
            _iocManager = iocManager;
        }
        /// <inheritdoc />
        private IUnitOfWork GetCurrentUow(ILogger logger)
        {
            IUnitOfWork unitOfWork;
            
            if (!UnitOfWorkDictionary.TryGetValue(ContextKey, out unitOfWork))
            {
                //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on GetCurrentUow)! UnitOfWork key: " + unitOfWorkKey);
                unitOfWork = _iocManager.Resolve<IUnitOfWork>();
                UnitOfWorkDictionary.TryAdd(ContextKey, unitOfWork);
                return unitOfWork;
            }

            if (unitOfWork.IsDisposed)
            {
                logger.Warn("There is a unitOfWorkKey in CallContext but the UOW was disposed!");
                UnitOfWorkDictionary.TryRemove(ContextKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            return unitOfWork;
        }
        private static void SetCurrentUow(IUnitOfWork value, ILogger logger)
        {   

            if (value == null)
            {
                ExitFromCurrentUowScope(logger);
                return;
            }

            IUnitOfWork outer;
            if (UnitOfWorkDictionary.TryGetValue(ContextKey, out outer))
            {
                if (outer == value)
                {
                    logger.Warn("Setting the same UOW to the CallContext, no need to set again!");
                    return;
                }

                value.Outer = outer;
            }
            else
            {
                if (!UnitOfWorkDictionary.TryAdd(ContextKey, value))
                {
                    throw new EddoException("Can not set unit of work! UnitOfWorkDictionary.TryAdd returns false!");
                }
            }
            CallContext.LogicalSetData(ContextKey, ContextKey);
        }
        private static void ExitFromCurrentUowScope(ILogger logger)
        {

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(ContextKey, out unitOfWork))
            {

                return;
            }

            UnitOfWorkDictionary.TryRemove(ContextKey, out unitOfWork);
            if (unitOfWork.Outer == null)
            {
                return;
            }

            //Restore outer UOW


            if (!UnitOfWorkDictionary.TryGetValue(ContextKey, out unitOfWork))
            {

                return;
            }


        }

    }
}
