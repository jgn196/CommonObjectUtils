using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.CommonObjectUtils.Testing
{
    /// <summary>
    /// An exception that is thrown when the EqualsTester finds breaches of the <c>Equals</c> or <c>GetHashCode</c> method contracts.
    /// </summary>
    [Serializable]
    public class EqualsTestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualsTestException"/> class.
        /// </summary>
        public EqualsTestException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualsTestException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public EqualsTestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualsTestException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EqualsTestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualsTestException"/> class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected EqualsTestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
