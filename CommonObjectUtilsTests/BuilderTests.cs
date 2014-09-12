using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the Builder class.
    /// </summary>
    [TestClass]
    public class BuilderTests
    {
        /// <summary>
        /// Tests a build with all arguments.
        /// </summary>
        [TestMethod]
        public void Builder_Build()
        {
            Assert.AreEqual(12, new IntBuilder().SetValue(4).SetMultiplier(3).Build());
        }

        /// <summary>
        /// Tests that mandatory arguments can't be omitted.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(MissingBuilderArgumentException))]
        public void Builder_BuildMissingMandatoryArguments()
        {
            new IntBuilder()
                .SetMultiplier(3)
                .Build();
        }

        /// <summary>
        /// Tests that optional arguments may be omitted.
        /// </summary>
        [TestMethod]
        public void Builder_BuildMissingOptionalArguments()
        {
            Assert.AreEqual(4, new IntBuilder().SetValue(4).Build());
        }

        /// <summary>
        /// A simple test Builder.
        /// </summary>
        private class IntBuilder : Builder<int>
        {
            /// <summary>
            /// The value to give the built integer.
            /// </summary>
            private int value;

            /// <summary>
            /// The (optional) value to multiply the built integer by.
            /// </summary>
            private int multiplier;

            /// <summary>
            /// Initializes a new instance of the <see cref="IntBuilder"/> class.
            /// </summary>
            public IntBuilder()
            {
                Expect("value", Necessity.Mandatory);
                Expect("multiplier", Necessity.Optional);
            }

            /// <summary>
            /// Sets the built integer value.
            /// </summary>
            /// <param name="value">The value to give built integers.</param>
            /// <returns>This builder for chaining calls.</returns>
            public IntBuilder SetValue(int value)
            {
                this.value = Receive("value", value);
                return this;
            }

            /// <summary>
            /// Sets the built integer multiplier.
            /// </summary>
            /// <param name="multiplier">The value to multiply the built integer by.</param>
            /// <returns>This builder for chaining calls.</returns>
            public IntBuilder SetMultiplier(int multiplier)
            {
                this.multiplier = Receive("multiplier", multiplier);
                return this;
            }

            /// <summary>
            /// The building implementation.
            /// </summary>
            /// <returns>The new integer.</returns>
            protected override int BuildImplementation()
            {
                return value * (Got("multiplier") ? multiplier : 1);
            }
        }
    }
}
