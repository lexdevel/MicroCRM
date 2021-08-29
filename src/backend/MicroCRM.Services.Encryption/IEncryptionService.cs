// using System;

namespace MicroCRM.Services.Encryption
{
    /// <summary>
    /// The encryption service interface.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Computes a hash code for the specified text.
        /// </summary>
        /// <param name="text">The specified text.</param>
        /// <returns>The HEX representation string of the hash.</returns>
        string ComputeHexString(string text);
    }
}
