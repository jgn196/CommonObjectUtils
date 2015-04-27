using System;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests.ExceptionTests
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
}