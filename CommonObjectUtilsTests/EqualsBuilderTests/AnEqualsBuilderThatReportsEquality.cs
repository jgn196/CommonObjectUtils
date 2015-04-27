using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.EqualsBuilderTests
{
    [TestClass]
    public class AnEqualsBuilderThatReportsEquality
    {
        [TestMethod]
        public void ContinuesToReportEqualityWhenEqualObjectsAreAppended()
        {
            GivenAnEqualsBuilderThatReportsEquality().Append("foo", "foo").IsEquals.Should().BeTrue();
        }

        private static EqualsBuilder GivenAnEqualsBuilderThatReportsEquality()
        {
            return new EqualsBuilder();
        }

        [TestMethod]
        public void ReportsInequalityWhenUnequalObjectsAreAppended()
        {
            GivenAnEqualsBuilderThatReportsEquality().Append("foo", "bar").IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ReportsEqualityWhenABaseEqualityIsAppended()
        {
            GivenAnEqualsBuilderThatReportsEquality().AppendBase(true).IsEquals.Should().BeTrue();
        }

        [TestMethod]
        public void ReportsInequalityWhenABaseInequalityIsAppended()
        {
            GivenAnEqualsBuilderThatReportsEquality().AppendBase(false).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ReportsEqualityWhenEnumerablesContainingIdenticalCollectionsAreAppended()
        {
            GivenAnEqualsBuilderThatReportsEquality()
                .AppendMany(new[] { "foo" }, new[] { "foo" }).IsEquals.Should().BeTrue();
        }
    }
}