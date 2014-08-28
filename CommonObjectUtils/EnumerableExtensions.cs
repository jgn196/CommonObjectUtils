using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using CuttingEdge.Conditions;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// Holds extensions to objects that implement the IEnumerable interface.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements held in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to act on.</param>
        /// <param name="action">The action to take on each element.</param>
        /// <example>
        /// <code>
        /// List&lt;int> myList = new List&lt;int>() { 1, 2, 3 };
        /// 
        /// myList.ForEach((x) => Console.WriteLine(x));
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1062", MessageId = "1", Justification = "Argument validated")]
        [SuppressMessage("Microsoft.Design", "CA1062", MessageId = "0", Justification = "Argument validated")]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Condition.Requires(enumerable).IsNotNull();
            Condition.Requires(action).IsNotNull();

            foreach (T t in enumerable)
            {
                action(t);
            }
        }
    }
}
