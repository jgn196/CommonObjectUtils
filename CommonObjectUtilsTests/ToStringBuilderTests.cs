﻿using System;
using System.Collections.Generic;

using Capgemini.CommonObjectUtils.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the ToStringBuilder class.
    /// </summary>
    [TestClass]
    public class ToStringBuilderTests
    {
        /// <summary>
        /// Tests passing null to the constructor.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStringBuilder_CreateNull()
        {
            new ToStringBuilder(null);
        }

        /// <summary>
        /// Tests passing bad arguments to the Append method.
        /// </summary>
        [TestMethod]
        public void ToStringBuilder_Append_BadArguments()
        {
            ToStringBuilder builder = new ToStringBuilder(this);

            new ErrorTester()
                .Test(typeof(ArgumentNullException), () => builder.Append(null, 1))
                .Test(typeof(ArgumentException), () => builder.Append(string.Empty, 1))
                .Test(typeof(ArgumentException), () => builder.Append(" ", 1));
        }

        /// <summary>
        /// Tests the ToString method.
        /// </summary>
        [TestMethod]
        public void ToStringBuilder_ToString()
        {
            Assert.AreEqual("ToStringBuilderTests", new ToStringBuilder(this).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo=1]",
                new ToStringBuilder(this).Append("Foo", 1).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo=bar]",
                new ToStringBuilder(this).Append("Foo", "bar").ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo=null]",
                new ToStringBuilder(this).Append("Foo", (string)null).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo=1, Bar=2]",
                new ToStringBuilder(this).Append("Foo", 1).Append("Bar", 2).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo={1, 2}]",
                new ToStringBuilder(this).AppendMany("Foo", new int[] { 1, 2 }).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo=null]",
                new ToStringBuilder(this).AppendMany("Foo", (int[])null).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo={1, 2}]",
                new ToStringBuilder(this).AppendMany("Foo", new List<int>() { 1, 2 }).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[Foo={1, 2}]",
                new ToStringBuilder(this).AppendMany("Foo", new HashSet<int>() { 1, 2 }).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[base=null]",
                new ToStringBuilder(this).AppendBase(null).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[base=]",
                new ToStringBuilder(this).AppendBase(string.Empty).ToString());
            Assert.AreEqual(
                "ToStringBuilderTests[base= ]",
                new ToStringBuilder(this).AppendBase(" ").ToString());
        }
    }
}
