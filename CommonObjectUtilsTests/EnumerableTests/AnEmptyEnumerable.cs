using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Capgemini.CommonObjectUtils.Tests.EnumerableTests
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
}