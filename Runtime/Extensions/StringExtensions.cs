using UniSharp.Utilities;
using UniSharp.Security.Hash;
using System;
using System.Text;

namespace UniSharp.Security.Extensions
{
    public static class StringExtensions
    {
        public static string ToHashChars(this string str, int fixedLenght)
        {
            var keyHash = FastHash.CalculateHash(str.GetBytes());

            var keyString = keyHash.ToString();

            if (keyString.Length < fixedLenght)
            {
                var randomSualt = StringUtility.GetRandomDigits(fixedLenght, new Random((int)keyHash));

                keyString += randomSualt;
            }

            return keyString.Substring(0, fixedLenght);
        }

        public static byte[] GetBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}

