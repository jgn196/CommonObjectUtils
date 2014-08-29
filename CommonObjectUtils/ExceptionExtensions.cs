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
        /// <remarks>
        /// You can use this extension method to combine identical error handling code blocks by catching
        /// the common exception base class, testing it to see if it is one of the errors you want to handle
        /// and re-throwing it if it isn't.
        /// </remarks>
        /// <example>
        /// Repeating error handling code like this can be combined.
        /// <code>
        /// try
        /// {
        ///     ...
        /// }
        /// catch(FileNotFoundException exception)
        /// {
        ///     // Handle error
        /// }
        /// catch(PathTooLongException exception)
        /// {
        ///     // Handle error
        /// }
        /// </code>
        /// Instead we catch the common base exception (IOException), test it and handle or re-throw as 
        /// appropriate.
        /// <code>
        /// try 
        /// {
        ///     ...
        /// }
        /// catch(IOException exception)
        /// {
        ///     if(exception.IsOneOf(typeof(FileNotFoundException), typeof(PathTooLongException)))
        ///     {
        ///         // Handle error
        ///     }
        ///     else
        ///     {
        ///         throw;
        ///     }
        /// }
        /// </code>
        /// </example>
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
