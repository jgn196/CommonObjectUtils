using System;

using Capgemini.CommonObjectUtils.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.Testing
{
    /// <summary>
    /// Tests the ErrorTester class.
    /// </summary>
    [TestClass]
    public class ErrorTesterTests
    {
        /// <summary>
        /// Tests passing a null expected exception to the Test() method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErrorTester_Test_NullOK()
        {
            new ErrorTester().Test(null, () => { });
        }

        /// <summary>
        /// Tests passing a null action to the Test() method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErrorTester_Test_OKNull()
        {
            new ErrorTester().Test(typeof(Exception), null);
        }

        /// <summary>
        /// Tests the successful Test() method.
        /// </summary>
        [TestMethod]
        public void ErrorTester_Test_Success()
        {
            new ErrorTester().Test(typeof(Exception), () => { throw new Exception(); });
        }

        /// <summary>
        /// Tests that the Test() method catches missing exceptions.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ErrorTestException))]
        public void ErrorTester_Test_NoException()
        {
            new ErrorTester().Test(typeof(ArgumentNullException), () => { });
        }

        /// <summary>
        /// Tests that the Test() method catches sub-class exceptions when the super class
        /// was expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ErrorTestException))]
        public void ErrorTester_Test_ExpectSubClassException()
        {
            new ErrorTester().Test(typeof(Exception), () => { throw new ArgumentException(); });
        }
    }
}
