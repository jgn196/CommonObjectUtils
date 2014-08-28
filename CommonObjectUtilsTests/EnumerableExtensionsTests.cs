using System;
using System.Collections.Generic;

using Capgemini.CommonObjectUtils.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the EnumerableExtensions class.
    /// </summary>
    [TestClass]
    public class EnumerableExtensionsTests
    {
        /// <summary>
        /// Tests that the ForEach method catches null arguments.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ForEach_NullArgs()
        {
            IEnumerable<int> nullEnumerable = null;
            Action<int> doNothing = (x) => { };

            new ErrorTester()
                .Test(typeof(ArgumentNullException), () => nullEnumerable.ForEach(doNothing))
                .Test(typeof(ArgumentNullException), () => new List<int>().ForEach(null));
        }

        /// <summary>
        /// Tests that the ForEach method does nothing when applied to an empty enumeration.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ForEach_Empty()
        {
            int actionCallCount = 0;
            Action<int> countCalls = (x) => { actionCallCount++; };

            new List<int>().ForEach(countCalls);

            Assert.AreEqual(0, actionCallCount);
        }

        /// <summary>
        /// Tests that the ForEach method calls the action argument for each element in the 
        /// enumeration.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ForEach()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            List<int> copyList = new List<int>();
            Action<int> copy = (x) => copyList.Add(x);

            new List<int>() { 1, 2, 3 }.ForEach(copy);

            CollectionAssert.AreEqual(list, copyList);
        }
    }
}
