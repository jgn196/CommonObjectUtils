using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.CommonObjectUtils.Tests
{
    /// <summary>
    /// Tests the MemoryPipe class.
    /// </summary>
    [TestClass]
    public class MemoryPipeTests
    {
        private const int Kilobyte = 1024;
        private readonly byte[] testData = new byte[Kilobyte];

        [TestInitialize]
        public void SetUp()
        {
            var random = new Random(42);
            random.NextBytes(testData);
        }

        /// <summary>
        /// Tests that an empty pipe reports its size as zero.
        /// </summary>
        [TestMethod]
        public void MemoryPipe_Size_EmptyPipe_SizeIs0()
        {
            Assert.AreEqual(0, BuildEmptyPipe().Size);
        }

        private static MemoryPipe BuildEmptyPipe()
        {
            return new MemoryPipe();
        }

        /// <summary>
        /// Tests that an empty pipe's size increases when data is written to it.
        /// </summary>
        [TestMethod]
        public void MemoryPipe_Write1k_SizeIs1k()
        {
            var pipe = BuildEmptyPipe();

            pipe.Write(new byte[Kilobyte]);

            Assert.AreEqual(Kilobyte, pipe.Size);
        }

        /// <summary>
        /// Tests that you can't read anything from an empty pipe.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MemoryPipe_Read1kFromEmptyPipe_Throws()
        {
            BuildEmptyPipe().Read(Kilobyte);
        }

        /// <summary>
        /// Tests that you get the same 1k of data out that you put in.
        /// </summary>
        [TestMethod]
        public void MemoryPipe_Read1kFrom1kPipe()
        {
            CollectionAssert.AreEqual(testData, Build1KPipe().Read(Kilobyte));
        }

        private MemoryPipe Build1KPipe()
        {
            var pipe = BuildEmptyPipe();

            pipe.Write(testData);
            
            return pipe;
        }
    }
}
