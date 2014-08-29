using System;
using System.Runtime.Serialization;

namespace Capgemini.CommonObjectUtils.Testing
{
    /// <summary>
    /// An exception that is thrown when an error test fails.
    /// </summary>
    [Serializable]
    public class ErrorTestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorTestException"/> class.
        /// </summary>
        public ErrorTestException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorTestException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ErrorTestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorTestException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="cause">The underlying cause.</param>
        public ErrorTestException(string message, Exception cause)
            : base(message, cause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorTestException"/> class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected ErrorTestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
