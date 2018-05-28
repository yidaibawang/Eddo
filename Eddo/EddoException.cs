using System;
using System.Runtime.Serialization;

namespace Eddo
{
    public class EddoException:Exception
    {
        public EddoException()
        {
        }
#if !NET46
        /// <summary>
        /// Creates a new <see cref="EddoException"/> object.
        /// </summary>
        public EddoException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
#endif

        /// <summary>
        /// Creates a new <see cref="EddoException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public EddoException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="EddoException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public EddoException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
