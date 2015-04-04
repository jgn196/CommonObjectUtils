using Capgemini.CommonObjectUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace EnumerableSpecification
{
    [TestClass]
    public class AnEmptyEnumerable
    {
        [TestMethod]
        public void DoesNotCallTheForEachAction()
        {
            var action = MockRepository.GenerateMock<Action<string>>();

            GivenAnEmptyEnumerable().ForEach(action);

            action.AssertWasNotCalled(a => a.Invoke(Arg<string>.Is.Anything));
        }

        private static IEnumerable<string> GivenAnEmptyEnumerable()
        {
            return new string[0];
        }
    }

    [TestClass]
    public class AnEnumerable
    {
        [TestMethod]
        public void CallsTheForEachActionWithEachItem()
        {
            var action = MockRepository.GenerateMock<Action<string>>();
            action.Expect(a => a.Invoke("foo"));
            action.Expect(a => a.Invoke("bar"));

            GivenAnEnumerable().ForEach(action);

            action.VerifyAllExpectations();
        }

        private static IEnumerable<string> GivenAnEnumerable()
        {
            return new[] { "foo", "bar" };
        }

        [TestMethod]
        public void DoesNotEnumerateWhenPassedANullForEachAction()
        {
            var enumerable = MockRepository.GenerateMock<IEnumerable<string>>();

            // ReSharper disable once PossibleMultipleEnumeration
            enumerable.ForEach(null);

            // ReSharper disable once PossibleMultipleEnumeration
            enumerable.AssertWasNotCalled(e => e.GetEnumerator());
        }
    }

    [TestClass]
    public class ANullEnumerable
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowsNullReferenceExceptionWhenForEachIsCalled()
        {
            GivenANullEnumerable().ForEach(s => { });
        }

        private static IEnumerable<string> GivenANullEnumerable()
        {
            return null;
        } 
    }
}