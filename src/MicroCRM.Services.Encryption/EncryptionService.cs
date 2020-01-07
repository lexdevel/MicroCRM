using System.Security.Cryptography;
using System.Text;
using MicroCRM.Services.Encryption.Internal;

namespace MicroCRM.Services.Encryption
{
    /// <summary>
    /// The encryption service class.
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        /// <summary>
        /// Computes a hash code for the specified text.
        /// </summary>
        /// <param name="text">The specified text.</param>
        /// <returns>The HEX representation string of the hash.</returns>
        public string ComputeHexString(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                sha256.Initialize();
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(text)).ConvertToHexString();
            }
        }
    }
}
