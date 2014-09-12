using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// An abstract base class for Builder classes.
    /// </summary>
    /// <typeparam name="T">The type that the builder builds.</typeparam>
    /// <remarks>
    /// <para>
    /// You can inherit from this class when writing your own builders.
    /// </para>
    /// <para>
    /// Call the <see cref="M:Capgemini.CommonObjectUtils.Builder`1.Expect(System.String,Capgemini.CommonObjectUtils.Builder{`0}.Necessity)"/> 
    /// method to tell the class what arguments your builder needs and which ones are
    /// mandatory.
    /// </para>
    /// <para>
    /// Call the <see cref="M:Capgemini.CommonObjectUtils.Builder`1.Receive``1(System.String,``0)"/>
    /// method as your builder gets each argument to tell the builder which arguments
    /// you have received.
    /// </para>
    /// <para>
    /// When client code calls the <see cref="M:Capgemini.CommonObjectUtils.Builder`1.Build"/>
    /// method the Builder class will check that all mandatory arguments
    /// have beens supplied and if they haven't it will throw a 
    /// <see cref="T:Capgemini.CommonObjectUtils.MissingBuilderArgumentException"/>.
    /// If all the required arguments have been supplied the builder calls the 
    /// <see cref="M:Capgemini.CommonObjectUtils.Builder`1.BuildImplementation"/>
    /// method
    /// that the sub-class supplies and building finishes.
    /// </para>
    /// <para>
    /// If you need to know if a particular argument got supplied you can call the
    /// <see cref="M:Capgemini.CommonObjectUtils.Builder`1.Got(System.String)"/>
    /// method instead of 
    /// tracking that yourself.
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example that builds integers with a mandatory starting value and an optional multiplier.
    /// <code>
    /// private class IntBuilder : Builder&lt;int>
    /// {
    ///     private int value;
    ///     private int multiplier;
    ///     
    ///     public IntBuilder()
    ///     {
    ///         Expect("value", Necessity.Mandatory);
    ///         Expect("multiplier", Necessity.Optional);
    ///     }
    ///     
    ///     public IntBuilder SetValue(int value)
    ///     {
    ///         this.value = Receive("value", value);
    ///         return this;
    ///     }
    ///     
    ///     public IntBuilder SetMultiplier(int multiplier)
    ///     {
    ///         this.multiplier = Receive("multiplier", multiplier);
    ///         return this;
    ///     }
    ///     
    ///     protected override int BuildImplementation()
    ///     {
    ///         return value * (Got("multiplier") ? multiplier : 1);
    ///     }
    /// }
    /// </code>
    /// </example>
    public abstract class Builder<T>
    {
        /// <summary>
        /// A look up of expected argument names to their necessity.
        /// </summary>
        private Dictionary<string, Necessity> expectedArguments = new Dictionary<string, Necessity>();

        /// <summary>
        /// The set of received argument names.
        /// </summary>
        private HashSet<string> receivedArguments = new HashSet<string>();

        /// <summary>
        /// An enumeration that specifies the necessity of a builder argument (whether the argument is
        /// optional or mandatory).
        /// </summary>
        protected enum Necessity
        {
            /// <summary>
            /// An argument is mandatory.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage(
                "Microsoft.Design",
                "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
                Justification = "Caller will not have to specify a type argument.")]
            Mandatory,

            /// <summary>
            /// An argument is optional.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage(
                "Microsoft.Design",
                "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
                Justification = "Caller will not have to specify a type argument.")]
            Optional
        }

        /// <summary>
        /// Builds the target object.
        /// </summary>
        /// <returns>The built object.</returns>
        public T Build()
        {
            CheckArguments();

            return BuildImplementation();
        }

        /// <summary>
        /// Tells the builder that an argument is expected.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        /// <param name="necessity">Whether the argument is mandatory or not.</param>
        protected void Expect(string argumentName, Necessity necessity)
        {
            expectedArguments.Add(argumentName, necessity);
        }

        /// <summary>
        /// Tells the builder that an argument was received.
        /// </summary>
        /// <typeparam name="TArg">The argument type.</typeparam>
        /// <param name="argumentName">The argument name.</param>
        /// <param name="value">The argument value.</param>
        /// <returns>The unchanged argument value.</returns>
        protected TArg Receive<TArg>(string argumentName, TArg value)
        {
            receivedArguments.Add(argumentName);
            return value;
        }

        /// <summary>
        /// Checks if an argument has received.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        /// <returns>True if the argument was received.</returns>
        protected bool Got(string argumentName)
        {
            return receivedArguments.Contains(argumentName);
        }

        /// <summary>
        /// The internal implementation that builds the target object.
        /// </summary>
        /// <returns>The built object.</returns>
        protected abstract T BuildImplementation();

        /// <summary>
        /// Checks that all mandatory arguments have been supplied.
        /// </summary>
        /// <exception cref="MissingBuilderArgumentException">
        /// One of the builder's mandatory arguments wasn't supplied.
        /// </exception>
        private void CheckArguments()
        {
            List<string> missingArguments = new List<string>();

            foreach (string argument in expectedArguments.Keys)
            {
                if (expectedArguments[argument] == Necessity.Mandatory)
                {
                    if (!receivedArguments.Contains(argument))
                    {
                        missingArguments.Add(argument);
                    }
                }
            }

            if (missingArguments.Count > 0)
            {
                throw new MissingBuilderArgumentException(missingArguments.ToArray());
            }
        }
    }
}
