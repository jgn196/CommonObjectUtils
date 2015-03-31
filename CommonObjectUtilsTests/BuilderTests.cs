using Capgemini.CommonObjectUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuilderSpecification
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

    [TestClass]
    public class ABuilderWithMandatoryExpectations
    {
        [TestMethod]
        [ExpectedException(typeof(MissingBuilderArgumentException))]
        public void ThrowsAnExceptionIfAnyAreMissingWhenBuilding()
        {
            new TestBuilder().Build();
        }

        [TestMethod]
        public void BuildsAfterItGetsThem()
        {
            new TestBuilder().SetFoo("bar").Build();
        }

        private class TestBuilder : Builder<string>
        {
            private string foo;
            public TestBuilder()
            {
                Expect("foo", Necessity.Mandatory);
            }

            public TestBuilder SetFoo(string foo)
            {
                this.foo = Receive("foo", foo);
                return this;
            }

            protected override string BuildImplementation()
            {
                return foo;
            }
        }
    }

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
            private string foo;

            public bool GotFoo { get { return Got("foo"); } }

            public TestBuilder()
            {
                Expect("foo", Necessity.Optional);
            }

            public TestBuilder SetFoo(string foo)
            {
                this.foo = Receive("foo", foo);
                return this;
            }

            protected override string BuildImplementation()
            {
                return foo ?? "";
            }
        }
    }

}