using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonObjectUtils
{
    /// <summary>
    /// A port of the Apache Commons EqualsBuilder utility class.
    /// </summary>
    public class EqualsBuilder
    {
        /// <summary>
        /// The state of the equals tests.
        /// <para>
        /// This starts off as true and is updated by each Append method.
        /// </para>
        /// </summary>
        private bool isEqual = true;

        /// <summary>
        /// Gets a value indicating whether the fields that have been checked were equal.
        /// </summary>
        public bool IsEquals
        {
            get
            {
                return isEqual;
            }
        }

        /// <summary>
        /// Compares two objects that implement <c>IEquatable</c> interface.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="left">The left hand object.</param>
        /// <param name="right">The right hand object.</param>
        /// <returns>This EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder Append<T>(T left, T right) where T : IEquatable<T>
        {
            isEqual = isEqual && left.Equals(right);

            return this;
        }

        /// <summary>
        /// Does a deep comparison of two enumerable objects containing the same type.
        /// </summary>
        /// <typeparam name="T">The type the enumerable objects contain.</typeparam>
        /// <param name="left">The left hand enumerable.</param>
        /// <param name="right">The right hand enumerable.</param>
        /// <returns>This EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder AppendMany<T>(IEnumerable<T> left, IEnumerable<T> right) where T : IEquatable<T>
        {
            bool result;

            if (left == right)
            {
                result = true;
            }
            else if (left == null || right == null)
            {
                result = false;
            }
            else if (left.Count() != right.Count())
            {
                result = false;
            }
            else
            {
                var deepEquals = left.Zip(right, (l, r) => l.Equals(r));

                result = deepEquals.All(r => r);
            }

            isEqual = isEqual && result;

            return this;
        }

        /// <summary>
        /// Adds the result of super.Equals().
        /// </summary>
        /// <param name="superEquals">The result of super.Equals().</param>
        /// <returns>This EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder AppendSuper(bool superEquals)
        {
            isEqual = isEqual && superEquals;

            return this;
        }

        /// <summary>
        /// Reset the EqualsBuilder so that it can be used again.
        /// </summary>
        public void Reset()
        {
            isEqual = true;
        }
    }
}
