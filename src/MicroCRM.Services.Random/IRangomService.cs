// using System;

namespace MicroCRM.Services.Random
{
    public interface IRangomService
    {
        /// <summary>
        /// Generates random alphanumeric strign.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The random string.</returns>
        string GenerateRandomString(int length);
    }
}
