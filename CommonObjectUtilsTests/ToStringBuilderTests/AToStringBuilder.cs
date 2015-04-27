using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.ToStringBuilderTests
{
    [TestClass]
    public class AToStringBuilder
    {
        [TestMethod]
        public void BuildsAStringFromAnObject()
        {
            new ToStringBuilder(new object()).ToString().Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WontBuildStringsForNullObjects()
        {
            new ToStringBuilder(null);
        }
    }
}