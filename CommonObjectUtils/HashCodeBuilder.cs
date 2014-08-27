using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// A port of the Apache Commons HashCodeBuilder utility class.
    /// </summary>
    public class HashCodeBuilder
    {
        /// <summary>
        /// The value to multiply the hash code by while adding each field value.
        /// </summary>
        private readonly int multiplierOddNumber;

        /// <summary>
        /// The calculated hash code that is updated during Append method calls.
        /// </summary>
        private int hashCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCodeBuilder"/> class.
        /// </summary>
        public HashCodeBuilder()
            : this(17, 37)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCodeBuilder"/> class.
        /// </summary>
        /// <param name="initialOddNumber">The initial hash code value.</param>
        /// <param name="multiplierOddNumber">The value to multiply the hash code by as field values are added.</param>
        public HashCodeBuilder(int initialOddNumber, int multiplierOddNumber)
        {
            hashCode = initialOddNumber;
            this.multiplierOddNumber = multiplierOddNumber;
        }

        /// <summary>
        /// Appends the field to the hash code calculation.
        /// </summary>
        /// <param name="value">The field to append.</param>
        /// <returns>This HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder Append(object value)
        {
            if (value != null)
            {
                hashCode = (hashCode * multiplierOddNumber) + value.GetHashCode();
            }

            return this;
        }

        /// <summary>
        /// Appends the values in a collection field to the hash code calculation.
        /// </summary>
        /// <typeparam name="T">The type of objects held in the collection field.</typeparam>
        /// <param name="values">The collection field</param>
        /// <returns>This HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder Append<T>(ICollection<T> values)
        {
            if (values != null)
            {
                foreach (T value in values)
                {
                    Append(value);
                }
            }

            return this;
        }

        /// <summary>
        /// Appends the result of the super GetHashCode method.
        /// </summary>
        /// <param name="superHashCode">The result of the super.GetHashCode method.</param>
        /// <returns>This HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder AppendSuper(int superHashCode)
        {
            hashCode = (hashCode * multiplierOddNumber) + superHashCode;

            return this;
        }

        /// <summary>
        /// Gets the hash code computed for the fields supplied to this object.
        /// <para>
        /// Note that this breaks the normal usage of GetHashCode because it doesn't return the hash code
        /// of this object.
        /// </para>
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
