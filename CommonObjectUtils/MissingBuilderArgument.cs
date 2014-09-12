using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// An exception that is thrown when mandatory arguments aren't supplied to sub-classes of
    /// the <see cref="Builder"/> class before calling the Build method.
    /// </summary>
    [Serializable]
    public class MissingBuilderArgumentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBuilderArgumentException"/> class.
        /// </summary>
        public MissingBuilderArgumentException() : base("Missing argument(s).") 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBuilderArgumentException"/> class.
        /// </summary>
        /// <param name="missingArgumentNames">The names of the missing arguments.</param>
        public MissingBuilderArgumentException(params string[] missingArgumentNames)
            : this(BuildErrorMessage(missingArgumentNames))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBuilderArgumentException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public MissingBuilderArgumentException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBuilderArgumentException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="cause">The inner cause of the exception.</param>
        public MissingBuilderArgumentException(string errorMessage, Exception cause)
            : base(errorMessage, cause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBuilderArgumentException"/> class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected MissingBuilderArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { 
        }

        /// <summary>
        /// Builds an error message from an array of missing argument names.
        /// </summary>
        /// <param name="missingArgumentNames">The missing argument names.</param>
        /// <returns>The error message.</returns>
        private static string BuildErrorMessage(string[] missingArgumentNames)
        {
            StringBuilder errorMessage = new StringBuilder("Missing the following argument(s): ");

            missingArgumentNames.ForEach(a => errorMessage.Append(a));

            return errorMessage.ToString();
        }
    }
}
