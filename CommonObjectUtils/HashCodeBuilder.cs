using System.Collections.Generic;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// A class used to generate hash codes from object field values inside the <c>GetHashCode</c> method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class allows you to add field to the hash code calculation in a fluent style.
    /// </para>
    /// <para>
    /// This is a partial port of the Apache Commons HashCodeBuilder utility class.
    /// </para>
    /// <example>
    /// <code>
    /// class Foo 
    /// {
    ///     private int field1;
    ///     private int[] field2;
    ///     
    ///     ...
    /// 
    ///     public override int GetHashCode()
    ///     {
    ///         return new HashCodeBuilder()
    ///             .Append(field1)
    ///             .AppendMany(field2)
    ///             .GetHashCode();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public class HashCodeBuilder
    {
        /// <summary>
        /// The value to multiply the hash code by while adding each field value.
        /// </summary>
        private readonly int _multiplierOddNumber;

        /// <summary>
        /// The calculated hash code that is updated during Append method calls.
        /// </summary>
        private int _hashCode;

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
        /// <param name="multiplierOddNumber">The value to multiply the hash code by as field values are appended.</param>
        public HashCodeBuilder(int initialOddNumber, int multiplierOddNumber)
        {
            _hashCode = initialOddNumber;
            _multiplierOddNumber = multiplierOddNumber;
        }

        /// <summary>
        /// Appends the field to the hash code calculation.
        /// </summary>
        /// <remarks>
        /// Don't pass arrays, lists or other collections to this method as the top level objects will be hashed rather than
        /// the contents. Instead call <see cref="Append{T}(ICollection{T})"/>.
        /// </remarks>
        /// <seealso cref="Append{T}(ICollection{T})"/>
        /// <param name="value">The field to append.</param>
        /// <returns>The HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder Append(object value)
        {
            if (value != null)
            {
                _hashCode = (_hashCode * _multiplierOddNumber) + value.GetHashCode();
            }

            return this;
        }

        /// <summary>
        /// Appends the values in a collection field to the hash code calculation.
        /// </summary>
        /// <typeparam name="T">The type of objects held in the collection field.</typeparam>
        /// <param name="values">The collection field</param>
        /// <returns>The HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder Append<T>(ICollection<T> values)
        {
            return AppendMany(values);
        }

        /// <summary>
        /// Appends the values in a collection field to the hash code calculation.
        /// </summary>
        /// <typeparam name="T">The type of objects held in the collection field.</typeparam>
        /// <param name="values">The collection field</param>
        /// <returns>The HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder AppendMany<T>(ICollection<T> values)
        {
            if (values != null)
            {
                values.ForEach(value => Append(value));
            }

            return this;
        }

        /// <summary>
        /// Appends the result of the <c>base.GetHashCode</c> method.
        /// </summary>
        /// <param name="baseHashCode">The result of the <c>base.GetHashCode</c> method.</param>
        /// <returns>The HashCodeBuilder for chaining calls.</returns>
        public HashCodeBuilder AppendBase(int baseHashCode)
        {
            _hashCode = (_hashCode * _multiplierOddNumber) + baseHashCode;

            return this;
        }

        /// <summary>
        /// Gets the hash code computed for the fields supplied to this object.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this breaks the normal usage of GetHashCode because it doesn't return the hash code
        /// of this object, it returns the calculated hash code of the fields that have been appended.
        /// </para>
        /// <para>
        /// This means that objects of this class should never be put into hash sets or other collections that rely
        /// on the <c>GetHashCode</c> contract.</para>
        /// </remarks>
        /// <returns>The computed hash code.</returns>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _hashCode;
        }
    }
}
