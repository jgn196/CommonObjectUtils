using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using CuttingEdge.Conditions;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// Holds extensions to the Exception class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Checks if the exception is an instance of, sub-class of or implements one of a list of types.
        /// </summary>
        /// <param name="exception">The exception being checked.</param>
        /// <param name="argsRest">
        /// An array of types that the exception will be compared to.
        /// </param>
        /// <returns>True if the exception's type matched any of the types.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062", MessageId = "0", Justification = "Argument validated")]
        public static bool IsOneOf(this Exception exception, params Type[] argsRest)
        {
            Condition.Requires(exception).IsNotNull();
            Condition.Requires(argsRest).IsNotNull();

            Type exceptionType = exception.GetType();

            return argsRest.Any(arg => arg.IsAssignableFrom(exceptionType));
        }
    }
}
