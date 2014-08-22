using System;
using System.Collections.Generic;

using CuttingEdge.Conditions;

namespace CommonObjectUtils
{
    /// <summary>
    /// A class for testing an object's implementation of the Equals() and GetHashCode() methods.
    /// </summary>
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
        /// <returns>This EqualsTester for chaining calls.</returns>
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
        /// Tests that:
        /// <list>
        /// <item>All items in the tester equal themselves.</item>
        /// <item>All items in the tester do not equal null.</item>
        /// </list>
        /// </summary>
        /// <exception cref="EqualsTestException">
        /// One of the tests failed.
        /// </exception>        
        public void TestEquals()
        {
            SelfEqualityTest();
            NullEqualityTest();
            GroupEqualityTest();
            GroupInequalityTest();
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
                throw new EqualsTestException(left.ToString() + " did not equal " + right.ToString());
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
                throw new EqualsTestException(left.ToString() + " equals " + right);
            }
        }

        /// <summary>
        /// Applies an action to all the items in a group.
        /// </summary>
        /// <param name="group">The group to get items from.</param>
        /// <param name="action">The action to apply.</param>
        private static void ForEachGroupItem(object[] group, Action<object> action)
        {
            foreach (object item in group)
            {
                action.Invoke(item);
            }
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
            foreach (object left in leftGroup)
            {
                foreach (object right in rightGroup)
                {
                    action.Invoke(left, right);
                }
            }
        }

        /// <summary>
        /// Applies an action to all of the equality groups.
        /// </summary>
        /// <param name="action">The action to apply.</param>
        private void ForEachGroup(Action<object[]> action)
        {
            foreach (object[] group in equalityGroups)
            {
                action.Invoke(group);
            }
        }

        /// <summary>
        /// Applies an action to every combination of equality groups.
        /// </summary>
        /// <param name="action">The action to apply.</param>
        private void ForEachGroupCombo(Action<object[], object[]> action)
        {
            foreach (object[] leftGroup in equalityGroups)
            {
                foreach (object[] rightGroup in equalityGroups)
                {
                    action.Invoke(leftGroup, rightGroup);
                }
            }
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
    }
}
