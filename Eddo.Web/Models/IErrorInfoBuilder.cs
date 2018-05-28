using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Models
{
    public interface IErrorInfoBuilder
    {
        ErrorInfo BuildForException(Exception exception);

        /// <summary>
        /// Adds an <see cref="IExceptionToErrorInfoConverter"/> object.
        /// </summary>
        /// <param name="converter">Converter</param>
        void AddExceptionConverter(IExceptionToErrorInfoConverter converter);
    }
}
