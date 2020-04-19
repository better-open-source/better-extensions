using System.IO;

namespace BetterExtensions.Streams
{
    /// <summary>
    /// Stream Extensions
    /// </summary>
    public static class StreamHelpers
    {
        /// <summary>
        /// Read byte array from Stream
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <returns>Byte array</returns>
        public static byte[] ReadFully(this Stream input)
        {
            using var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}