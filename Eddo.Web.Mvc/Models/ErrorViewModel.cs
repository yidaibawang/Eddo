using Eddo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Mvc.Models
{
    public class ErrorViewModel
    {
        public ErrorInfo ErrorInfo { get; set; }

        public Exception Exception { get; set; }

        public ErrorViewModel()
        {

        }

        public ErrorViewModel(Exception exception)
        {
            Exception = exception;
            ErrorInfo = ErrorInfoBuilder.Instance.BuildForException(exception);
        }
    }
}
