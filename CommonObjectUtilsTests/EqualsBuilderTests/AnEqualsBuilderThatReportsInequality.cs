using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.EqualsBuilderTests
{
    [TestClass]
    public class AnEqualsBuilderThatReportsInequality
    {
        [TestMethod]
        public void ContinuesToReportInequalityWhenEqualObjectsAreAppended()
        {
            GivenAnEqualsBuilderThatReportsInequality().Append("foo", "foo").IsEquals.Should().BeFalse();
        }

        private static EqualsBuilder GivenAnEqualsBuilderThatReportsInequality()
        {
            return new EqualsBuilder().Append("foo", "bar");
        }

        [TestMethod]
        public void ContinuesToReportInequalityWhenUnequalObjectsAreAppended()
        {
            GivenAnEqualsBuilderThatReportsInequality().Append("foo", "bar").IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ReportsEqualityAfterItIsReset()
        {
            var builder = GivenAnEqualsBuilderThatReportsInequality();
            builder.Reset();

            builder.IsEquals.Should().BeTrue();
        }

        [TestMethod]
        public void ContinuesToReportInequalityWhenABaseEqualityIsAppended()
        {
            GivenAnEqualsBuilderThatReportsInequality().AppendBase(true).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ContinuesToReportInequalityWhenABaseInequalityIsAppended()
        {
            GivenAnEqualsBuilderThatReportsInequality().AppendBase(false).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ContinuesToReportInequalityWhenEqualEnumerablesAreAppended()
        {
            GivenAnEqualsBuilderThatReportsInequality()
                .AppendMany(new[] { "foo" }, new[] { "foo" }).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ContinuesToReportInEqualityWhenUnequalEnumerablesAreAppended()
        {
            GivenAnEqualsBuilderThatReportsInequality()
                .AppendMany(new[] { "foo" }, new[] { "bar" }).IsEquals.Should().BeFalse();
        }
    }
}