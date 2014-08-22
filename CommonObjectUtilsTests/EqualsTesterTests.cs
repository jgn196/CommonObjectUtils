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
    }
}
