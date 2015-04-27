using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.BuilderTests
{
    [TestClass]
    public class ABuilderWithOptionalExpectations
    {
        [TestMethod]
        public void BuildsWithoutThem()
        {
            new TestBuilder().Build();
        }

        [TestMethod]
        public void CanTellIfItGotThem()
        {
            new TestBuilder().SetFoo("bar").GotFoo.Should().BeTrue();
        }

        private class TestBuilder : Builder<string>
        {
            private string _foo;

            public bool GotFoo { get { return Got("foo"); } }

            public TestBuilder()
            {
                Expect("foo", Necessity.Optional);
            }

            public TestBuilder SetFoo(string foo)
            {
                _foo = Receive("foo", foo);
                return this;
            }

            protected override string BuildImplementation()
            {
                return _foo ?? "";
            }
        }
    }
}