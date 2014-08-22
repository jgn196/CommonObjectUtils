using System;

using CommonObjectUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonObjectUtilsTests
{
    /// <summary>
    /// Tests the EqualsTester class.
    /// </summary>
    [TestClass]
    public class EqualsTesterTests
    {
        /// <summary>
        /// Tests the TestEquals() method.
        /// </summary>
        [TestMethod]
        public void EqualsTester_TestEquals()
        {
            new EqualsTester()
                .AddEqualityGroup("hello", "h" + "ello")
                .AddEqualityGroup("world", "w" + "orld")
                .AddEqualityGroup(2, 1 + 1)
                .TestEquals();
        }

        /// <summary>
        /// Tests the TestEquals() method catches objects that aren't equal to themselves.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EqualsTestException))]
        public void EqualsTester_TestEqualsReflexive()
        {
            new EqualsTester()
                .AddEqualityGroup(new NeverEqual())
                .TestEquals();
        }

        /// <summary>
        /// Tests the TestEquals() method catches objects that are equal to null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EqualsTestException))]
        public void EqualsTester_TestEqualsNullEquality()
        {
            new EqualsTester()
                .AddEqualityGroup(new AlwaysEqual())
                .TestEquals();
        }
        
        /// <summary>
        /// Tests that the TestEquals() method catches objects that are equal in one direction but 
        /// not in the other.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EqualsTestException))]
        public void EqualsTester_TestEqualsSymmetric()
        {
            new EqualsTester()
                .AddEqualityGroup(new AlwaysEqual(), new NeverEqual())
                .TestEquals();
        }

        /// <summary>
        /// Tests that the TestEquals() method catches objects that don't implement a transitive equals.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EqualsTestException))]
        public void EqualsTester_TestEqualsTransitive()
        {
            object b = new AlwaysEqual();
            new EqualsTester()
                .AddEqualityGroup(new ConfigurableEquals(b), b, new AlwaysEqual())
                .TestEquals();
        }

        /// <summary>
        /// Tests that you can't pass a null equality group.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EqualsTester_AddEqualityGroupNull()
        {
            new EqualsTester().AddEqualityGroup((object[])null);
        }

        /// <summary>
        /// Tests that you can't pass an empty equality group.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EqualsTester_AddEqualityGroupEmpty()
        {
            new EqualsTester().AddEqualityGroup(new object[0]);
        }

        /// <summary>
        /// Tests that you can't pass null into an equality group.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EqualsTester_AddEqualityGroupContainsNull()
        {
            new EqualsTester().AddEqualityGroup(1, null);
        }

        /// <summary>
        /// A class that is never equal to anything.
        /// </summary>
        private class NeverEqual
        {
            /// <summary>
            /// An override that always returns false.
            /// </summary>
            /// <param name="obj">This is ignored.</param>
            /// <returns>Always false</returns>
            public override bool Equals(object obj)
            {
                return false;
            }
        }

        /// <summary>
        /// A class that is equal to everything.
        /// </summary>
        private class AlwaysEqual
        {
            /// <summary>
            /// An override that always returns true.
            /// </summary>
            /// <param name="obj">This is ignored.</param>
            /// <returns>Always true</returns>
            public override bool Equals(object obj)
            {
                return true;
            }
        }

        /// <summary>
        /// An object that can be configured at instantiation to be equal to one and only one other
        /// object instance.
        /// </summary>
        private class ConfigurableEquals
        {
            /// <summary>
            /// The one and only object instance that this object will evaluate as equal to.
            /// </summary>
            private object equalTo;

            /// <summary>
            /// Initializes a new instance of the <see cref="ConfigurableEquals"/> class.
            /// </summary>
            /// <param name="equalTo">The one and only object instance that this object will
            /// evaluate as equal to.</param>
            public ConfigurableEquals(object equalTo)
            {
                this.equalTo = equalTo;
            }

            /// <summary>
            /// An override that returns true for one other object instance only.
            /// </summary>
            /// <param name="instance">The object instance to check.</param>
            /// <returns>True if instance was the one and only instance.</returns>
            public override bool Equals(object instance)
            {
                return instance == equalTo;
            }
        }
    }
}
