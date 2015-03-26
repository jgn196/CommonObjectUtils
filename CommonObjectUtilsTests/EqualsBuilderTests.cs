using Capgemini.CommonObjectUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EqualsBuilderSpecification
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

    [TestClass]
    public class AnEqualsBuilder
    {
        [TestMethod]
        public void ConsidersTheSameObjectAsEqualWhenAppended()
        {
            var sameObject = "foo";
            GivenAnEqualsBuilder().Append(sameObject, sameObject).IsEquals.Should().BeTrue();
        }

        private static EqualsBuilder GivenAnEqualsBuilder()
        {
            return new EqualsBuilder();
        }

        [TestMethod]
        public void ConsidersTwoNullsAsEqualWhenAppended()
        {
            GivenAnEqualsBuilder().Append<string>(null, null).IsEquals.Should().BeTrue();
        }

        [TestMethod]
        public void ConsidersANullAndObjectAsUnequalWhenAppended()
        {
            GivenAnEqualsBuilder().Append(null, "foo").IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ConsidersEnumerablesWithDifferentCountsAsUnequalWhenAppended()
        {
            GivenAnEqualsBuilder()
                .AppendMany(new[] { "f00" }, new[] { "foo", "foo" }).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ConsidersEnumberablesWithDifferentContentsAsUnequalWhenAppended()
        {
            GivenAnEqualsBuilder().AppendMany(new[] { "foo" }, new[] { "bar" }).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ConsidersANullAsDifferentToAnObjectInAppendedEnumerables()
        {
            GivenAnEqualsBuilder()
                .AppendMany(new string[] { null }, new[] { "foo" }).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ConsidersTwoNullsAsEqualInAppendedEnumerables()
        {
            GivenAnEqualsBuilder()
                .AppendMany(new string[] { null }, new string[] { null }).IsEquals.Should().BeTrue();
        }

        [TestMethod]
        public void ConsidersTheSameEnumerableAsEqualWhenAppended()
        {
            var theSame = new[] { "foo" };
            GivenAnEqualsBuilder().AppendMany(theSame, theSame).IsEquals.Should().BeTrue();
        }

        [TestMethod]
        public void ConsidersANullEnumerableAsUnequalToAnEnumerableWhenAppended()
        {
            GivenAnEqualsBuilder().AppendMany(null, new[] { "foo" }).IsEquals.Should().BeFalse();
        }

        [TestMethod]
        public void ConsidersTwoNullEnumerablesAsEqualWhenAppended()
        {
            GivenAnEqualsBuilder().AppendMany((string[])null, null).IsEquals.Should().BeTrue();
        }

        [TestMethod]
        public void UsesSuppliedComparerWhenAppendingNonEquatableEnumerables()
        {
            GivenAnEqualsBuilder()
                .AppendMany(
                    new[] { new NotEquatable() },
                    new[] { new NotEquatable() },
                    new NotEquatableComparer())
                .IsEquals.Should().BeTrue();
        }

        private class NotEquatable { }

        private class NotEquatableComparer : IEqualityComparer<NotEquatable>
        {
            public bool Equals(NotEquatable x, NotEquatable y)
            {
                return true;
            }

            public int GetHashCode(NotEquatable obj)
            {
                throw new NotImplementedException();
            }
        }
    }

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