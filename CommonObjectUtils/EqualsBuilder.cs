using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// A class used to compare object field values inside the <c>Equals</c> method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class allows you to compare multiple fields in your class in a fluent style.
    /// </para>
    /// <para>
    /// This is a partial port of the Apache Commons EqualsBuilder class.
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
    ///     public override bool Equals(object obj)
    ///     {
    ///         ...
    ///         MyClass other = obj as MyClass;
    ///         return new EqualsBuilder()
    ///             .Append(field1, other.field1)
    ///             .AppendMany(field2, other.field2)
    ///             .IsEquals;
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public class EqualsBuilder
    {
        /// <summary>
        /// The state of the equals tests.
        /// <para>
        /// This starts off as true and is updated by each Append method.
        /// </para>
        /// </summary>
        private bool _isEqual = true;

        /// <summary>
        /// Gets a value indicating whether fields were equal.
        /// </summary>
        /// <value>Indicates whether the fields that have been appended were all equal.</value>
        public bool IsEquals
        {
            get
            {
                return _isEqual;
            }
        }

        /// <summary>
        /// Appends two objects that implement the <c>IEquatable</c> interface to the list of values to compare.
        /// </summary>
        /// <remarks>
        /// Don't pass arrays, lists or other collections to this method as the top level objects will be compared rather than
        /// the contents. Instead call <see cref="AppendMany{T}(IEnumerable{T}, IEnumerable{T})"/>.</remarks>
        /// <seealso cref="AppendMany{T}(IEnumerable{T}, IEnumerable{T})"/>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="left">The left hand object.</param>
        /// <param name="right">The right hand object.</param>
        /// <returns>The EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder Append<T>(T left, T right) where T : IEquatable<T>
        {
            CommonAppend(left, right, (l, r) => l.Equals(r));

            return this;
        }

        private void CommonAppend<T>(T left, T right, Func<T, T, bool> equalityFunc)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            var leftIsNull = left == null;
            var areSame = ReferenceEquals(left, right);
            var result = areSame || !leftIsNull && equalityFunc(left, right);

            _isEqual = _isEqual && result;
        }

        /// <summary>
        /// Appends two objects to the list of values to compare and compares them using a supplied comparer.
        /// </summary>
        /// <remarks>
        /// Don't pass arrays, lists or other collections to this method as the top level objects will be compared rather than
        /// the contents. Instead call <see cref="AppendMany{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>.
        /// </remarks>
        /// <seealso cref="AppendMany{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="left">The left hand object.</param>
        /// <param name="right">The right hand object.</param>
        /// <param name="comparer">An implementation of the <c>IEqualityComparer</c> interface that can compare
        /// elements in the enumerations.</param>
        /// <returns>The EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder Append<T>(T left, T right, IEqualityComparer<T> comparer)
        {
            Condition.Requires(comparer).IsNotNull();

            CommonAppend(left, right, comparer.Equals);
            
            return this;
        }

        /// <summary>
        /// Appends two enumerable objects that contain the same (<c>IEquatable</c>) type to the list of values 
        /// to compare.
        /// </summary>
        /// <remarks>
        /// EqualsBuilder will perform a deep comparison of the two enumerable objects.
        /// Each element pair in the enumerations is compared by calling the <c>Equals</c> method of the 
        /// <c>IEquatable</c> interface.
        /// </remarks>
        /// <typeparam name="T">The type the enumerable objects contain.</typeparam>
        /// <param name="left">The left hand enumerable.</param>
        /// <param name="right">The right hand enumerable.</param>
        /// <returns>The EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder AppendMany<T>(IEnumerable<T> left, IEnumerable<T> right) where T : IEquatable<T>
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            CommonAppendMany(left, right, (l, r) => ReferenceEquals(l, r) || l != null && l.Equals(r));
            
            return this;
        }

        private void CommonAppendMany<T>(
            IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, T, bool> equalityFunc)
        {
            bool result;

            if (ReferenceEquals(left, right))
            {
                result = true;
            }
            else if (left == null || right == null)
            {
                result = false;
            }
            else
            {
                result = AreEnumerationsEqual(left, right, equalityFunc);
            }

            _isEqual = _isEqual && result;
        }

        private static bool AreEnumerationsEqual<T>(
            IEnumerable<T> left, 
            IEnumerable<T> right, 
            Func<T, T, bool> equalityFunc)
        {
            var leftEnumerator = left.GetEnumerator();
            var rightEnumerator = right.GetEnumerator();

            var leftCount = 0;
            var rightCount = 0;
            bool gotAnyItems;
            do
            {
                var gotLeftItem = leftEnumerator.MoveNext();
                var gotRightItem = rightEnumerator.MoveNext();

                leftCount += gotLeftItem ? 1 : 0;
                rightCount += gotRightItem ? 1 : 0;

                gotAnyItems = gotLeftItem || gotRightItem;
                var gotTwoItems = gotLeftItem && gotRightItem;

                if (!gotTwoItems) continue;

                var leftItem = leftEnumerator.Current;
                var rightItem = rightEnumerator.Current;
                var itemsEqual = equalityFunc(leftItem, rightItem);

                if (!itemsEqual)
                {
                    return false;
                }
            } while (gotAnyItems);

            return leftCount == rightCount;
        }

        /// <summary>
        /// Appends two enumerable objects that contain the same type to the list of values to compare.
        /// </summary>
        /// <remarks>
        /// EqualsBuilder will perform a deep comparison of the two enumerable objects.
        /// If the enumerated objects implement <c>IEquatable</c> you can call the <c>AppendMany</c> with two
        /// arguments instead.
        /// </remarks>
        /// <typeparam name="T">The type the enumerable objects contain.</typeparam>
        /// <param name="left">The left hand enumerable.</param>
        /// <param name="right">The right hand enumerable.</param>
        /// <param name="comparer">An implementation of the <c>IEqualityComparer</c> interface that can compare
        /// elements in the enumerations.</param>
        /// <returns>The EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder AppendMany<T>(
            IEnumerable<T> left,
            IEnumerable<T> right,
            IEqualityComparer<T> comparer)
        {
            Condition.Requires(comparer).IsNotNull();

            CommonAppendMany(left, right, comparer.Equals);

            return this;
        }

        /// <summary>
        /// Appends the result of <c>base.Equals</c>.
        /// </summary>
        /// <param name="baseEquals">The result of base.Equals.</param>
        /// <returns>The EqualsBuilder for chaining calls.</returns>
        public EqualsBuilder AppendBase(bool baseEquals)
        {
            _isEqual = _isEqual && baseEquals;

            return this;
        }

        /// <summary>
        /// Resets the EqualsBuilder so that it can be used again.
        /// </summary>
        public void Reset()
        {
            _isEqual = true;
        }
    }
}
