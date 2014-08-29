using System;

using CuttingEdge.Conditions;

namespace Capgemini.CommonObjectUtils.Testing
{
    /// <summary>
    /// A class that tests that actions throw expected exceptions.
    /// </summary>
    /// <remarks>
    /// You can use this class (as an alternative to the <c>ExpectedException</c> attribute) when you have several 
    /// test cases that should all throw an exception and you want to group them together into a single unit test method.
    /// </remarks>
    /// <example>
    /// <code>
    /// [TestMethod]
    /// public void MyNullArgumentTest() 
    /// {
    ///     ...
    ///     new ErrorTester()
    ///         .Test(typeof(ArgumentNullException), () => {myObj.myMethod(null, "1");})
    ///         .Test(typeof(ArgumentNullException), () => {myObj.myMethod("2", null);});
    /// }
    /// </code>
    /// </example>
    public class ErrorTester
    {
        /// <summary>
        /// Tests that an action causes a specified exception.
        /// </summary>
        /// <param name="expectedException">The type of exception that is expected.</param>
        /// <param name="action">The action to perform.</param>
        /// <returns>The ErrorTester for chaining calls.</returns>
        /// <exception cref="ErrorTestException">
        /// Either no exception was thrown by the action or the exception was a different type to the expected one.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "0",
            Justification = "Validation done by Condition")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "1",
            Justification = "Validation done by Condition")]
        public ErrorTester Test(Type expectedException, Action action)
        {
            Condition.Requires(expectedException).IsNotNull();
            Condition.Requires(action).IsNotNull();

            bool actionSuccessful = false;
            try
            {
                action.Invoke();
                actionSuccessful = true;
            }
            catch (Exception error)
            {
                if (error.GetType() != expectedException)
                {
                    throw new ErrorTestException("Expected " + expectedException.Name + " exception but got "
                        + error.GetType().Name);
                }
            }

            if (actionSuccessful)
            {
                throw new ErrorTestException("Expected " + expectedException.Name + " exception");
            }

            return this;
        }
    }
}
