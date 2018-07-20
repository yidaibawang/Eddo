using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Events
{
    public class ExceptionData: EventData
    {    
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">Exception object</param>
        public ExceptionData(Exception exception)
        {
            Exception = exception;
        }
    }
}
