using System;
using System.Linq;

namespace Crawler.Common
{
    public static class ArrayExtensions
    {
        public static void ThrowIfEmpty(this object[] array, string argumentName)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException($"{argumentName} can't be empty.");
            }
        }

        public static void ThrowIfAnyNullEntry(this object[] array, string argumentName)
        {
            if (array.Any(x => x == null))
            {
                throw new ArgumentException($"{argumentName} can't contain null entries.");
            }
        }
    }
}
