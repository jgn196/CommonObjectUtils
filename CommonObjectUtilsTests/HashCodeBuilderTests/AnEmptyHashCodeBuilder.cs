using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.HashCodeBuilderTests
{
    [TestClass]
    public class AnEmptyHashCodeBuilder
    {
        [TestMethod]
        public void GivesTheInitialOddNumber()
        {
            const int initialOddNumber = 7;
            new HashCodeBuilder(initialOddNumber, 13).GetHashCode().Should().Be(initialOddNumber);
        }
    }
}