using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.EnumerableTests
{
    [TestClass]
    public class ANullEnumerable
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowsNullReferenceExceptionWhenForEachIsCalled()
        {
            GivenANullEnumerable().ForEach(s => { });
        }

        private static IEnumerable<string> GivenANullEnumerable()
        {
            return null;
        } 
    }
}