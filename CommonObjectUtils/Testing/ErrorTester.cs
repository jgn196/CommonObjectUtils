using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CuttingEdge.Conditions;

namespace CommonObjectUtils.Testing
{
    /// <summary>
    /// A test utility to make testing many actions that are supposed to throw an exception
    /// in a single unit test.
    /// </summary>
    public class ErrorTester
    {
        /// <summary>
        /// Tests that an action causes a specified exception.
        /// </summary>
        /// <param name="expectedException">The type of exception that is expected.</param>
        /// <param name="action">The action to perform.</param>
        /// <returns>This ErrorTester for chaining calls.</returns>
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
