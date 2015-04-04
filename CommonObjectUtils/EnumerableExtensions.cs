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
        /// IEnumerable&lt;int> myItems = ...
        /// 
        /// myItems.ForEach((x) => Console.WriteLine(x));
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) throw new NullReferenceException("Object reference not set to an instance of an object.");
            if (action == null) return;

            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
