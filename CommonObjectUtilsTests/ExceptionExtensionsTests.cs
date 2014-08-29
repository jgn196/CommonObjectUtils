using System;

using Capgemini.CommonObjectUtils.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the ExceptionExtensions class.
    /// </summary>
    [TestClass]
    public class ExceptionExtensionsTests
    {
        /// <summary>
        /// Tests passing various bad arguments to the IsOneOf method.
        /// </summary>
        [TestMethod]
        public void ExceptionExtensions_IsOneOf_BadArgs()
        {
            new ErrorTester()
                .Test(typeof(ArgumentNullException), () => ((Exception)null).IsOneOf(typeof(Exception)))
                .Test(typeof(ArgumentNullException), () => new Exception().IsOneOf(null));
        }

        /// <summary>
        /// Tests the IsOneOf method.
        /// </summary>
        [TestMethod]
        public void ExceptionExtensions_IsOneOf()
        {
            Exception exception = new ArgumentException();

            Assert.IsFalse(exception.IsOneOf());
            Assert.IsFalse(exception.IsOneOf(typeof(ArgumentNullException)));
            Assert.IsFalse(exception.IsOneOf(
                typeof(InvalidOperationException), 
                typeof(ArgumentNullException)));
            Assert.IsFalse(exception.IsOneOf(typeof(uint)));

            Assert.IsTrue(exception.IsOneOf(typeof(ArgumentException)));
            Assert.IsTrue(exception.IsOneOf(typeof(Exception)));
            Assert.IsTrue(exception.IsOneOf(
                typeof(ArgumentNullException),
                typeof(ArgumentException)));
        }
    }
}
