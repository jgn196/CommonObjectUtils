using System.Collections.Generic;

using CommonObjectUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the EqualsBuilder class.
    /// </summary>
    [TestClass]
    public class EqualsBuilderTests
    {
        /// <summary>
        /// Tests comparing objects.
        /// </summary>
        [TestMethod]
        public void EqualsBuilder_Append()
        {
            Assert.IsTrue(new EqualsBuilder().Append(true, true).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append(false, false).IsEquals);

            Assert.IsFalse(new EqualsBuilder().Append(false, true).IsEquals);
            Assert.IsFalse(new EqualsBuilder().Append(true, false).IsEquals);

            Assert.IsFalse(new EqualsBuilder().Append(false, true).Append(true, true).IsEquals);

            Assert.IsTrue(new EqualsBuilder().Append((byte)1, (byte)1).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((short)1, (short)1).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((int)1, (int)1).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((long)1, (long)1).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((float)1.0, (float)1.0).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((double)1, (double)1).IsEquals);

            Assert.IsTrue(new EqualsBuilder().Append((ushort)1, (ushort)1).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((uint)1, (uint)1).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append((ulong)1, (ulong)1).IsEquals);

            Assert.IsTrue(new EqualsBuilder().Append('c', 'c').IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append("foo", "foo").IsEquals);
        }

        /// <summary>
        /// Tests comparing enumerable objects.
        /// </summary>
        [TestMethod]
        public void EqualsBuilder_AppendEnumerables()
        {
            Assert.IsTrue(new EqualsBuilder().Append((bool[])null, (bool[])null).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append(new bool[] { true }, new bool[] { true }).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append(new bool[] { false }, new bool[] { false }).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append(new bool[] { true, true }, new bool[] { true, true }).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append(new bool[] { false, false }, new bool[] { false, false }).IsEquals);
            Assert.IsTrue(new EqualsBuilder().Append(new bool[0], new bool[0]).IsEquals);

            Assert.IsTrue(new EqualsBuilder().Append((IEnumerable<bool>)new List<bool>() { true }, new List<bool>() { true }).IsEquals);

            Assert.IsTrue(new EqualsBuilder().Append((IEnumerable<bool>)new HashSet<bool>() { true }, new HashSet<bool>() { true }).IsEquals);

            Assert.IsFalse(new EqualsBuilder().Append(new bool[] { true }, null).IsEquals);
            Assert.IsFalse(new EqualsBuilder().Append(null, new bool[] { true }).IsEquals);
            Assert.IsFalse(new EqualsBuilder().Append(new bool[] { true }, new bool[] { true, true }).IsEquals);
            Assert.IsFalse(new EqualsBuilder().Append(new bool[] { true, true }, new bool[] { true, false }).IsEquals);
        }

        /// <summary>
        /// Tests the AppendSuper method.
        /// </summary>
        [TestMethod]
        public void EqualsBuilder_AppendSuper()
        {
            EqualsBuilder builder = new EqualsBuilder();

            Assert.IsTrue(builder.IsEquals);

            builder.AppendSuper(true);

            Assert.IsTrue(builder.IsEquals);

            builder.AppendSuper(false);

            Assert.IsFalse(builder.IsEquals);
        }

        /// <summary>
        /// Tests the Reset method.
        /// </summary>
        [TestMethod]
        public void EqualsBuilder_Reset()
        {
            EqualsBuilder builder = new EqualsBuilder();
            builder.AppendSuper(false);

            Assert.IsFalse(builder.IsEquals);

            builder.Reset();

            Assert.IsTrue(builder.IsEquals);
        }
    }
}
