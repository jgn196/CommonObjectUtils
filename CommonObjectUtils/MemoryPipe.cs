using CuttingEdge.Conditions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Capgemini.CommonObjectUtils
{
    /// <summary>
    /// A one way in memory data pipe.
    /// </summary>
    public class MemoryPipe
    {
        private readonly List<byte> dataStream = new List<byte>();

        /// <summary>
        /// The size of the pipe in bytes.
        /// </summary>
        /// <remarks>
        /// This is the total number of bytes that have been written to the pipe but haven't yet been
        /// read from it.
        /// </remarks>
        public long Size { get { return dataStream.Count; } }

        /// <summary>
        /// Writes data to the pipe.
        /// </summary>
        /// <param name="data">The data to write to the pipe.</param>
        public void Write(byte[] data)
        {
            Condition.Requires(data).IsNotNull();

            dataStream.AddRange(data);
        }

        /// <summary>
        /// Reads data from the pipe.
        /// </summary>
        /// <param name="count">The number of bytes to read from the pipe.</param>
        /// <returns>An array containing the read bytes.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// This is thrown if the caller has requested more data than the pipe contains.
        /// </exception>
        public byte[] Read(int count)
        {
            if (count > Size)
            {
                throw new ArgumentOutOfRangeException(
                    "count",
                    "The count argument (" + count + ") was larger than the pipe size (" + Size + ").");
            }

            var result = dataStream.Take(count).ToArray();
            dataStream.RemoveRange(0, count);
            dataStream.Capacity = dataStream.Count;
            return result;
        }
    }
}
