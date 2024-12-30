using System;

namespace UniSharp.Utilities
{
    public static class StringUtility
    {
        public static string GetRandomDigits(int lenght, Random random = null)
        {
            if (lenght <= 0)
                return string.Empty;

            if (random == null)
                random = new Random();

            var str = new char[lenght];

            for (int i = 0; i < str.Length; i++)
                str[i] = (char)(random.Next(0, 9) + '0');

            return new string(str);
        }
    }
}
