﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using CuttingEdge.Conditions;

namespace Capgemini.CommonObjectUtils.Testing
{
    /// <summary>
    /// A class for testing an object's implementation of the <c>Equals</c> and <c>GetHashCode</c> methods.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class allows you to add groups of objects that should be equal in a fluent style.
    /// </para>
    /// <para>
    /// This is a partial port of the Guava <c>EqualsTester</c> class.
    /// </para>
    /// <example>
    /// <code>
    /// new EqualsTester()
    ///     .AddEqualityGroup("hello", "h" + "ello")
    ///     .AddEqualityGroup(3, 1 + 2)
    ///     .TestEquals();
    /// </code>
    /// </example>
    /// </remarks>
    public class EqualsTester
    {
        /// <summary>
        /// The list of equality groups.
        /// </summary>
        private List<object[]> equalityGroups = new List<object[]>();

        /// <summary>
        /// Adds an equality group to the tester.
        /// </summary>
        /// <param name="groupItems">
        /// A set of objects that should all be equal according to the Equals() method.
        /// </param>
        /// <returns>The EqualsTester for chaining calls.</returns>
        public EqualsTester AddEqualityGroup(params object[] groupItems)
        {
            Condition.Requires(groupItems)
                .IsNotNull()
                .IsNotEmpty()
                .DoesNotContain(null);

            equalityGroups.Add(groupItems);

            return this;
        }

        /// <summary>
        /// Tests the correct equality of the added items.
        /// </summary>
        /// <remarks>
        /// The method makes the following checks.
        /// <list type="bullet">
        /// <item>All items in the tester equal themselves.</item>
        /// <item>None of the items in the tester equal null.</item>
        /// <item>All items in each equality group equal each other.</item>
        /// <item>None of the items equal an item in any other equality group.</item>
        /// <item>All items in each equality group have the same hash code.</item>
        /// <item>All items return the same hash code if called more than once.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="EqualsTestException">
        /// One of the tests failed.
        /// </exception>        
        public void TestEquals()
        {
            SelfEqualityTest();
            NullEqualityTest();
            HashCodeConsistencyTest();
            GroupEqualityTest();
            GroupInequalityTest();
            GroupHashCodeEqualTest();
        }

        /// <summary>
        /// Tests that two objects are equal by calling the Equals() method on the first.
        /// </summary>
        /// <param name="left">The left hand object.</param>
        /// <param name="right">The right hand object.</param>
        /// <exception cref="EqualsTestException">
        /// The two arguments are not equal.
        /// </exception>
        private static void TestEqual(object left, object right)
        {
            if (!left.Equals(right))
            {
                throw new EqualsTestException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} did not equal {1}",
                        left,
                        right));
            }
        }

        /// <summary>
        /// Tests that two objects are not equal by calling the Equals() method on the first.
        /// </summary>
        /// <param name="left">The left hand object.</param>
        /// <param name="right">The right hand object.</param>
        /// <exception cref="EqualsTestException">
        /// The two arguments are equal.
        /// </exception>
        private static void TestNotEqual(object left, object right)
        {
            if (left.Equals(right))
            {
                throw new EqualsTestException(
                    string.Format(CultureInfo.CurrentCulture, "{0} equals {1}", left, right));
            }
        }

        /// <summary>
        /// Tests that two objects return the same hash code.
        /// </summary>
        /// <param name="left">The left hand object.</param>
        /// <param name="right">The right hand object.</param>
        private static void TestHashCodeEqual(object left, object right)
        {
            int leftHashCode = left.GetHashCode();
            int rightHashCode = right.GetHashCode();
            if (leftHashCode != rightHashCode)
            {
                throw new EqualsTestException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} hash code ({1}) is not equal to {2} hash code ({3})",
                        left,
                        leftHashCode,
                        right,
                        rightHashCode));
            }
        }

        /// <summary>
        /// Applies an action to all the items in a group.
        /// </summary>
        /// <param name="group">The group to get items from.</param>
        /// <param name="action">The action to apply.</param>
        private static void ForEachGroupItem(object[] group, Action<object> action)
        {
            group.ForEach(item => action.Invoke(item));
        }

        /// <summary>
        /// Applies an action to every combination of items from two groups.
        /// </summary>
        /// <param name="leftGroup">The left hand group.</param>
        /// <param name="rightGroup">The right hand group.</param>
        /// <param name="action">The action to apply.</param>
        private static void ForEachItemCombo(
            object[] leftGroup,
            object[] rightGroup,
            Action<object, object> action)
        {
            leftGroup.ForEach(left => rightGroup.ForEach(right => action.Invoke(left, right)));
        }

        /// <summary>
        /// Applies an action to all of the equality groups.
        /// </summary>
        /// <param name="action">The action to apply.</param>
        private void ForEachGroup(Action<object[]> action)
        {
            equalityGroups.ForEach(group => action.Invoke(group));
        }

        /// <summary>
        /// Applies an action to every combination of equality groups.
        /// </summary>
        /// <param name="action">The action to apply.</param>
        private void ForEachGroupCombo(Action<object[], object[]> action)
        {
            equalityGroups.ForEach(
                leftGroup => equalityGroups.ForEach(rightGroup => action.Invoke(leftGroup, rightGroup)));
        }

        /// <summary>
        /// Tests that all items in the equality groups equal themselves.
        /// </summary>
        private void SelfEqualityTest()
        {
            ForEachGroup(group => ForEachGroupItem(group, item => TestEqual(item, item)));
        }

        /// <summary>
        /// Tests that no item in any of the equality groups equals null.
        /// </summary>
        private void NullEqualityTest()
        {
            ForEachGroup(group => ForEachGroupItem(group, item => TestNotEqual(item, null)));
        }

        /// <summary>
        /// Tests that all items in each equality group equal each other.
        /// </summary>
        private void GroupEqualityTest()
        {
            ForEachGroup(group => ForEachItemCombo(group, group, TestEqual));
        }

        /// <summary>
        /// Tests that all items are not equal to items in other groups.
        /// </summary>
        private void GroupInequalityTest()
        {
            ForEachGroupCombo((leftGroup, rightGroup) =>
                {
                    if (leftGroup != rightGroup)
                    {
                        ForEachItemCombo(leftGroup, rightGroup, TestNotEqual);
                    }
                });
        }

        /// <summary>
        /// Tests that all items in each group return the same hash code.
        /// </summary>
        private void GroupHashCodeEqualTest()
        {
            ForEachGroup(group =>
            {
                if (group.Select(item => item.GetHashCode()).Distinct().Count() > 1)
                {
                    throw new EqualsTestException("Inconsistent hash code values in equality group.");
                }
            });
        }

        /// <summary>
        /// Tests that all items return the same hash code if called twice.
        /// </summary>
        private void HashCodeConsistencyTest()
        {
            ForEachGroup(group => ForEachGroupItem(group, item => TestHashCodeEqual(item, item)));
        }
    }
}
