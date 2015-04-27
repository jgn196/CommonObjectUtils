using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.BuilderTests
{
    [TestClass]
    public class ABuilder
    {
        [TestMethod]
        public void CallsTheBuildImplementationWhenBuilding()
        {
            var builder = GivenABuilderWithNoExpectations();

            builder.Build();

            builder.WasImplementationCalled.Should().BeTrue();
        }

        private static TestBuilder GivenABuilderWithNoExpectations()
        {
            return new TestBuilder();
        }

        private class TestBuilder : Builder<string>
        {
            public bool WasImplementationCalled { get; private set; }

            protected override string BuildImplementation()
            {
                WasImplementationCalled = true;
                return "foo";
            }
        }
    }
}