using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.EqualsBuilderTests
{
    [TestClass]
    public class AnEqualsBuilder
    {
        [TestMethod]
        public void ConsidersTheSameObjectAsEqualWhenAppended()
        {
            const string sameObject = "foo";
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
}