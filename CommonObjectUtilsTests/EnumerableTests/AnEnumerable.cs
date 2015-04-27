using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Capgemini.CommonObjectUtils.Tests.EnumerableTests
{
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
}