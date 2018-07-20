namespace Eddo.Dependency
{
    internal class DisposableDependencyObjectWrapper : DisposableDependencyObjectWrapper<object>, IDisposableDependencyObjectWrapper
    {
        public DisposableDependencyObjectWrapper(IIocResolve iocResolver, object obj)
            : base(iocResolver, obj)
        {

        }
    }

    internal class DisposableDependencyObjectWrapper<T> : IDisposableDependencyObjectWrapper<T>
    {
        private readonly IIocResolve _iocResolver;

        public T Object { get; private set; }

        public DisposableDependencyObjectWrapper(IIocResolve iocResolver, T obj)
        {
            _iocResolver = iocResolver;
            Object = obj;
        }

        public void Dispose()
        {
            _iocResolver.Release(Object);
        }
    }
}
