using System;

namespace SportsStore.Business.Extensions
{
    public static class Extensions
    {
        public static void ThrowIfNull(this object element)
        {
            if (element == null)
            {
                throw new ArgumentException("Given non existing object");
            }
        }

        public static void ThrowIfNullOrEmpty(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Given non existing object");
            }
        }
    }
}