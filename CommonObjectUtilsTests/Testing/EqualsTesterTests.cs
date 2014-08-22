using System;

using CommonObjectUtils.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonObjectUtils.Testing.Tests
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
                .AddEqualityGroup(new AlwaysEqual(true))
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
        /// Tests that the TestEquals() method catches objects that are equal but have different hash
        /// codes.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EqualsTestException))]
        public void EqualsTester_TestEqualsBadHash()
        {
            new EqualsTester()
                .AddEqualityGroup(new AlwaysEqualConfigurableHashCode(1), new AlwaysEqualConfigurableHashCode(2))
                .TestEquals();
        }

        /// <summary>
        /// Tests that the TestEquals() method catches objects that return inconsistent hash codes.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EqualsTestException))]
        public void EqualsTester_TestEqualsInconsistentHash()
        {
            new EqualsTester()
                .AddEqualityGroup(new AlwaysEqualIncrementingHashCode())
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
#pragma warning disable 659
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
#pragma warning restore 659

        /// <summary>
        /// A class that is equal to everything.
        /// </summary>
#pragma warning disable 659
        private class AlwaysEqual
        {
            /// <summary>
            /// A flag to tell the class it should even be equal to null.
            /// </summary>
            private bool equalToNull;

            /// <summary>
            /// Initializes a new instance of the <see cref="AlwaysEqual"/> class.
            /// </summary>
            /// <remarks>
            /// Instances initialized with this constructor will not be equal to null.
            /// </remarks>
            public AlwaysEqual()
                : this(false)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="AlwaysEqual"/> class.
            /// </summary>
            /// <param name="equalToNull">A flag to tell the class that it should report equal to null.</param>
            public AlwaysEqual(bool equalToNull)
            {
                this.equalToNull = equalToNull;
            }

            /// <summary>
            /// An override that always returns true.
            /// </summary>
            /// <param name="obj">This is ignored.</param>
            /// <returns>Always true</returns>
            public override bool Equals(object obj)
            {
                return equalToNull || obj != null;
            }
        }
#pragma warning restore 659

        /// <summary>
        /// An object that can be configured at instantiation to be equal to one and only one other
        /// object instance.
        /// </summary>
#pragma warning disable 659
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
#pragma warning restore 659

        /// <summary>
        /// An object that is equal to everything but has a configurable hash code.
        /// </summary>
        private class AlwaysEqualConfigurableHashCode : AlwaysEqual
        {
            /// <summary>
            /// The hash code that is returned.
            /// </summary>
            private int hashCode;

            /// <summary>
            /// Initializes a new instance of the <see cref="AlwaysEqualConfigurableHashCode"/> class.
            /// </summary>
            /// <param name="hashCode">The hash code that will be returned.</param>
            public AlwaysEqualConfigurableHashCode(int hashCode)
            {
                this.hashCode = hashCode;
            }

            /// <summary>
            /// An override that returns the specified hash code.
            /// </summary>
            /// <returns>The specified hash code.</returns>
            public override int GetHashCode()
            {
                return this.hashCode;
            }
        }

        /// <summary>
        /// A class that is equal to everything but increments its hash code every time it is queried.
        /// </summary>
        private class AlwaysEqualIncrementingHashCode : AlwaysEqual
        {
            /// <summary>
            /// The hash code that will be returned and incremented.
            /// </summary>
            private int hashCode = 0;

            /// <summary>
            /// An override to return a hash code that increments each call.
            /// </summary>
            /// <returns>The hash code.</returns>
            public override int GetHashCode()
            {
                return this.hashCode++;
            }
        }
    }
}
