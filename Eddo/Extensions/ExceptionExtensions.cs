using System;
using System.Runtime.ExceptionServices;

namespace Eddo.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}
