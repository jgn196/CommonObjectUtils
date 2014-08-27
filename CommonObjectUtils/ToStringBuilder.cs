using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CuttingEdge.Conditions;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// A class used to generate string representations of objects for debugging purposes in the <c>ToString</c> method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Strings built by this class start with the subject object's (short) class name. If fields are appended, the class 
    /// name will be followed by an square bracket delimited list of the fields. Each field will be represented by its
    /// name followed by an equals sign and then its value. The text "null" follows the equals sign if a field is null.
    /// A brace delimited list of values follows the equals sign if a field is a collection.
    /// </para>
    /// <para>
    /// This class allows you to append fields to the string in a fluent style.
    /// </para>
    /// <para>
    /// This is a partial port of the Apache commons ToStringBuilder class.
    /// </para>
    /// <example>
    /// <code>
    /// class Foo 
    /// {
    ///     private int field1 = "bar";
    ///     private int[] field2 = new int[]{1, 2};
    ///     
    ///     ...
    /// 
    ///     public override string ToString()
    ///     {
    ///         return new ToStringBuilder(this)
    ///             .Append("field1", field1)
    ///             .AppendMany("field2", field2)
    ///             .ToString();
    ///     }
    /// }
    /// </code>
    /// This would produce the following output:
    /// <code>
    /// Foo[field1=bar, field2={1, 2}]
    /// </code>
    /// </example>
    /// </remarks>
    public class ToStringBuilder
    {
        /// <summary>
        /// The string representation builder.
        /// </summary>
        private StringBuilder builder = new StringBuilder();

        /// <summary>
        /// Used to flag when the first field has been appended.
        /// </summary>
        private bool hasFields = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToStringBuilder"/> class.
        /// </summary>
        /// <param name="value">The object being rendered as text.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "0",
            Justification = "Validation done by Condition")]
        public ToStringBuilder(object value)
        {
            Condition.Requires(value).IsNotNull();

            builder.Append(value.GetType().Name);
        }

        /// <summary>
        /// Append a field to the string representation.
        /// </summary>
        /// <typeparam name="T">The field type.</typeparam>
        /// <param name="fieldName">The field name.</param>
        /// <param name="field">The field value.</param>
        /// <returns>The ToStringBuilder to chain calls.</returns>
        public ToStringBuilder Append<T>(string fieldName, T field)
        {
            Condition.Requires(fieldName).IsNotNullOrWhiteSpace();

            StartWritingField();

            string fieldValue = field == null ? "null" : field.ToString();
            builder.Append(string.Format(CultureInfo.CurrentCulture, "{0}={1}", fieldName, fieldValue));

            return this;
        }

        /// <summary>
        /// Append an enumerable field to the string representation.
        /// </summary>
        /// <typeparam name="T">The type of items in the field.</typeparam>
        /// <param name="fieldName">The field name.</param>
        /// <param name="field">The field value.</param>
        /// <returns>The ToStringBuilder to chain calls.</returns>
        public ToStringBuilder AppendMany<T>(string fieldName, IEnumerable<T> field)
        {
            Condition.Requires(fieldName).IsNotNullOrWhiteSpace();

            StartWritingField();

            string fieldValue = "null";
            if (field != null)
            {
                fieldValue = "{" + string.Join(", ", field.Select(x => x.ToString()).ToArray()) + "}";
            }

            builder.Append(string.Format(CultureInfo.CurrentCulture, "{0}={1}", fieldName, fieldValue));

            return this;
        }

        /// <summary>
        /// Append a base class string representation.
        /// </summary>
        /// <param name="baseValue">The base class representation.</param>
        /// <returns>The ToStringBuilder to chain calls.</returns>
        public ToStringBuilder AppendBase(string baseValue)
        {
            StartWritingField();

            builder.Append(string.Format(CultureInfo.CurrentCulture, "base={0}", baseValue ?? "null"));

            return this;
        }

        /// <summary>
        /// Gets the built string representation.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            if (hasFields)
            {
                return builder.ToString() + "]";
            }
            else
            {
                return builder.ToString();
            }
        }

        /// <summary>
        /// If this is the first field being written this method writes the opening '[' character
        /// otherwise it writes the separating ", " string.
        /// </summary>
        private void StartWritingField()
        {
            if (!hasFields)
            {
                builder.Append("[");
                hasFields = true;
            }
            else
            {
                builder.Append(", ");
            }
        }
    }
}
