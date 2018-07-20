using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Events
{
    public class EddoHandledExceptionData:ExceptionData
    {
        public EddoHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}
