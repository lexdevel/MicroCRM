using System.Text;

namespace MicroCRM.Services.Encryption.Internal
{
    internal static class ByteArrayHelper
    {
        /// <summary>
        /// Converts the specified byte array into HEX string.
        /// </summary>
        /// <param name="buffer">The specified byte array.</param>
        /// <returns>The HEX string representation of the byte array.</returns>
        public static string ConvertToHexString(this byte[] buffer)
        {
            var stringBuilder = new StringBuilder();
            foreach (var ch in buffer)
            {
                stringBuilder.Append(ch.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
