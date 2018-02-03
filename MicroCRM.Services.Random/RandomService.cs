using System.Text;

namespace MicroCRM.Services.Random
{
    public class RandomService : IRangomService
    {
        #region Constants

        private const string Alphanum = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        #endregion

        /// <summary>
        /// Generates random alphanumeric strign.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The random string.</returns>
        public string GenerateRandomString(int length)
        {
            var random = new System.Random();
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var nextChar = Alphanum[random.Next(Alphanum.Length)];
                stringBuilder.Append(nextChar);
            }

            return stringBuilder.ToString();
        }
    }
}
