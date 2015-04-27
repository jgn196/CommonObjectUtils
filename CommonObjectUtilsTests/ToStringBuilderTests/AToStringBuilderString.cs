using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.ToStringBuilderTests
{
    [TestClass]
    public class AToStringBuilderString
    {
        [TestMethod]
        public void StartsWithTheObjectTypeName()
        {
            new ToStringBuilder(new object()).ToString().Should().StartWith("Object");
        }

        [TestMethod]
        public void IsTheObjectTypeNameWhenNoFieldsAreAppended()
        {
            new ToStringBuilder(new object()).ToString().Should().Be("Object");
        }

        [TestMethod]
        public void EndsWithABracketedListOfAppendedFields()
        {
            var builder = new ToStringBuilder(new object());
            builder.Append("Foo", 1);

            builder.ToString().Should().EndWith("[Foo=1]");
        }

        [TestMethod]
        public void IncludesBracedListsOfEnumerableFields()
        {
            var builder = new ToStringBuilder(new object());
            builder.AppendMany("Foo", new []{1, 2, 3});

            builder.ToString().Should().Contain("Foo={1, 2, 3}");
        }

        [TestMethod]
        public void IncludesTheBaseString()
        {
            var builder = new ToStringBuilder(new object());
            builder.AppendBase("Foo");

            builder.ToString().Should().Contain("base=Foo");
        }
    }
}