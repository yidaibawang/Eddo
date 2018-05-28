using Eddo.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Runtime.Validation
{  
    [Serializable]
    public class EddoValidationException:EddoException
    {
        /// <summary>
        /// 验证错误集合
        /// </summary>
        public IList<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        /// Exception severity.
        /// Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EddoValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }

#if !NET46
        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public EddoValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }
#endif

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public EddoValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="validationErrors">Validation errors</param>
        public EddoValidationException(string message, IList<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public EddoValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }
    }
}
