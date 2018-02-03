using System;

namespace MicroCRM.Extensions
{
    public static class EnvironmentExtensions
    {
        public static Environment? TryParse(this Environment self, string valueToParseFrom, bool ignoreCase = true)
        {
            if (Enum.TryParse<Environment>(valueToParseFrom, ignoreCase, out var environment))
            {
                return environment;
            }
            return null;
        }

        public static bool IsDevelopment(this Environment environment) => environment == Environment.Development;
        public static bool IsStaging(this Environment environment) => environment == Environment.Staging;
        public static bool IsProduction(this Environment environment) => environment == Environment.Production;
    }
}
