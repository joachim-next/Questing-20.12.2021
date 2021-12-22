using System;

namespace Crawler.UI
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object obj, string argumentName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{argumentName} can't be null.");
            }
        }
    }
}