using System;
using System.IO;
using Capgemini.CommonObjectUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace ExceptionSpecification
{
    [TestClass]
    public class AnException
    {
        [TestMethod]
        public void IsOneOfItself()
        {
            new Exception().IsOneOf(typeof (Exception)).Should().BeTrue();
        }

        [TestMethod]
        public void IsOneOfItsSuperClass()
        {
            new FileNotFoundException().IsOneOf(typeof (IOException)).Should().BeTrue();
        }

        [TestMethod]
        public void IsNotOneOfItsSubClass()
        {
            new IOException().IsOneOf(typeof (FileNotFoundException)).Should().BeFalse();
        }

        [TestMethod]
        public void IsNotOneOfUnrelatedClass()
        {
            new ArgumentException().IsOneOf(typeof (IOException)).Should().BeFalse();
        }

        [TestMethod]
        public void IsOneOfAnArrayOfClassesWhereTheOthersAreUnrelated()
        {
            new IOException().IsOneOf(typeof (ArgumentException), typeof (IOException))
                .Should().BeTrue();
        }
    }

    [TestClass]
    public class ANullException
    {
        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void ThrowsNullReferenceExceptionWhenIsOneOfIsCalled()
        {
            ((Exception) null).IsOneOf(typeof (Exception));
        }
    }
}