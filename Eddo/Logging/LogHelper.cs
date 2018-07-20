using Castle.Core.Logging;
using Eddo.Collections.Extensions;
using Eddo.Dependency;
using Eddo.Runtime.Validation;
using System;
using System.Linq;

namespace Eddo.Logging
{
    public static class LogHelper
    {
        /// <summary>
        /// 获取日志记录
        /// </summary>
        public static ILogger Logger { get; private set; }

        static LogHelper()
        {
            Logger = IocManager.Instance.IsRegistered(typeof(ILoggerFactory))
                ? IocManager.Instance.Resolve<ILoggerFactory>().Create(typeof(LogHelper))
                : NullLogger.Instance;
        }

        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        public static void LogException(ILogger logger, Exception ex)
        {
            var severity = (ex as IHasLogSeverity)?.Severity ?? LogSeverity.Error;

            logger.Log(severity, ex.Message, ex);

            LogValidationErrors(logger, ex);
        }

        private static void LogValidationErrors(ILogger logger, Exception exception)
        {
            //Try to find inner validation exception
            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is EddoValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (!(exception is EddoValidationException))
            {
                return;
            }

            var validationException = exception as EddoValidationException;
            if (validationException.ValidationErrors.IsNullOrEmpty())
            {
                return;
            }

            logger.Log(validationException.Severity, "There are " + validationException.ValidationErrors.Count + " validation errors:");
            foreach (var validationResult in validationException.ValidationErrors)
            {
                var memberNames = "";
                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    memberNames = " (" + string.Join(", ", validationResult.MemberNames) + ")";
                }

                logger.Log(validationException.Severity, validationResult.ErrorMessage + memberNames);
            }
        }
    }
}
