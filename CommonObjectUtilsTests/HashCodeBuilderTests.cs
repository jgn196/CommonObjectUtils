using System.Collections.Generic;

using CommonObjectUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the HashCodeBuilder class.
    /// </summary>
    [TestClass]
    public class HashCodeBuilderTests
    {
        /// <summary>
        /// Tests the Append method.
        /// </summary>
        [TestMethod]
        public void HashCodeBuilder_Append()
        {
            const int HashPart = 629;

            Assert.AreEqual(630, new HashCodeBuilder().Append(true).GetHashCode());
            Assert.AreEqual(HashPart, new HashCodeBuilder().Append(false).GetHashCode());
            Assert.AreEqual(632, new HashCodeBuilder().Append((byte)3).GetHashCode());
            Assert.AreEqual(
                HashPart + ((short)3).GetHashCode(),
                new HashCodeBuilder().Append((short)3).GetHashCode());
            Assert.AreEqual(632, new HashCodeBuilder().Append(3).GetHashCode());
            Assert.AreEqual(632, new HashCodeBuilder().Append((long)3).GetHashCode());
            Assert.AreEqual(
                HashPart + ((float)3.0).GetHashCode(),
                new HashCodeBuilder().Append((float)3.0).GetHashCode());
            Assert.AreEqual(
                HashPart + ((double)3.0).GetHashCode(),
                new HashCodeBuilder().Append((double)3.0).GetHashCode());

            Assert.AreEqual(632, new HashCodeBuilder().Append((ushort)3).GetHashCode());
            Assert.AreEqual(632, new HashCodeBuilder().Append((uint)3).GetHashCode());
            Assert.AreEqual(632, new HashCodeBuilder().Append((ulong)3).GetHashCode());

            Assert.AreEqual(HashPart + 'c'.GetHashCode(), new HashCodeBuilder().Append('c').GetHashCode());
            Assert.AreEqual(HashPart + "foo".GetHashCode(), new HashCodeBuilder().Append("foo").GetHashCode());

            Assert.AreEqual(17, new HashCodeBuilder().Append(null).GetHashCode());
        }

        /// <summary>
        /// Tests the AppendSuper method.
        /// </summary>
        [TestMethod]
        public void HashCodeBuilder_AppendSuper()
        {
            Assert.AreEqual(632, new HashCodeBuilder().AppendSuper(3).GetHashCode());
        }

        /// <summary>
        /// Tests the Append method that takes collections.
        /// </summary>
        [TestMethod]
        public void HashCodeBuilder_AppendCollections()
        {
            Assert.AreEqual(17, new HashCodeBuilder().Append((bool[])null).GetHashCode());
            Assert.AreEqual(630, new HashCodeBuilder().Append(new bool[] { true }).GetHashCode());
            Assert.AreEqual(629, new HashCodeBuilder().Append(new bool[] { false }).GetHashCode());
            Assert.AreEqual(23311, new HashCodeBuilder().Append(new bool[] { true, true }).GetHashCode());
            Assert.AreEqual(23273, new HashCodeBuilder().Append(new bool[] { false, false }).GetHashCode());
            Assert.AreEqual(17, new HashCodeBuilder().Append(new bool[0]).GetHashCode());

            Assert.AreEqual(630, new HashCodeBuilder().Append(new List<bool>() { true }).GetHashCode());
            Assert.AreEqual(630, new HashCodeBuilder().Append(new HashSet<bool>() { true }).GetHashCode());
        }
    }
}
