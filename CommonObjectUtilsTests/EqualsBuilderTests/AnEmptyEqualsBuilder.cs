using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.EqualsBuilderTests
{
    [TestClass]
    public class AnEmptyEqualsBuilder
    {
        [TestMethod]
        public void ReportsEquality()
        {
            GivenAnEmptyEqualBuilder().IsEquals.Should().BeTrue();
        }

        private static EqualsBuilder GivenAnEmptyEqualBuilder()
        {
            return new EqualsBuilder();
        }
    }
}