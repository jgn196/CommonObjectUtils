using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.ExceptionTests
{
    [TestClass]
    public class ANullException
    {
        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void ThrowsNullReferenceExceptionWhenIsOneOfIsCalled()
        {
            ((Exception) null).IsOneOf(typeof (Exception));
        }
    }
}