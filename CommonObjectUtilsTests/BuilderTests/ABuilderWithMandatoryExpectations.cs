using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.BuilderTests
{
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
            private string _foo;
            public TestBuilder()
            {
                Expect("foo", Necessity.Mandatory);
            }

            public TestBuilder SetFoo(string foo)
            {
                _foo = Receive("foo", foo);
                return this;
            }

            protected override string BuildImplementation()
            {
                return _foo;
            }
        }
    }
}