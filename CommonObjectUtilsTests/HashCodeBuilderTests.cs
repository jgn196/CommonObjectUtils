﻿using System;
using Capgemini.CommonObjectUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace HashCodeBuilderSpecification
{
    [TestClass]
    public class AnEmptyHashCodeBuilder
    {
        [TestMethod]
        public void GivesTheInitialOddNumber()
        {
            const int initialOddNumber = 7;
            new HashCodeBuilder(initialOddNumber, 13).GetHashCode().Should().Be(initialOddNumber);
        }
    }

    [TestClass]
    public class AHashCodeBuildersHashCode
    {
        private int _initialHashCode;

        [TestMethod]
        public void ChangesWhenYouAppendSomething()
        {
            GivenAHashCodeBuilder().Append(new Object()).GetHashCode().Should().NotBe(_initialHashCode);
        }

        private HashCodeBuilder GivenAHashCodeBuilder()
        {
            var builder = new HashCodeBuilder();
            _initialHashCode = builder.GetHashCode();
            return builder;
        }

        [TestMethod]
        public void ChangesWhenYouAppendACollectionWithSomethingInIt()
        {
            GivenAHashCodeBuilder().Append(new[] {new object()}).GetHashCode().Should().NotBe(_initialHashCode);
        }

        [TestMethod]
        public void ChangesWhenYouAppendABaseValue()
        {
            GivenAHashCodeBuilder().AppendBase(1).Should().NotBe(_initialHashCode);
        }

        [TestMethod]
        public void DoesntChangeWhenYouAppendNull()
        {
            GivenAHashCodeBuilder().Append(null).GetHashCode().Should().Be(_initialHashCode);
            GivenAHashCodeBuilder().AppendMany<int[]>(null).GetHashCode().Should().Be(_initialHashCode);
        }

        [TestMethod]
        public void DoesntChangeWhenYouAppendAnEmptyCollection()
        {
            GivenAHashCodeBuilder().Append(new Object[0]).GetHashCode().Should().Be(_initialHashCode);
        }
    }
}