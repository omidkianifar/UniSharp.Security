using System;

namespace UniSharp.Security.Obfuscation
{
    internal static class ObfuscateExtensions
    {
        private const double FloatEpsilon = 0.0001f;
        private const double DoubleEpsilon = 0.000001d;

        private static System.Random Random { get; } = new System.Random();

        private static int ObfuscateIntKey { get; } = Random.Next(int.MinValue, int.MaxValue);
        private static uint ObfuscateUIntKey { get; } = (uint)Random.Next(int.MinValue, int.MaxValue);
        private static long ObfuscateLongKey { get; } = (long)Random.Next(int.MinValue, int.MaxValue);
        private static ulong ObfuscateULongKey { get; } = (ulong)Random.Next(int.MinValue, int.MaxValue);


        public static int Obfuscate(this int intValue)
        {
            return intValue ^ ObfuscateIntKey;
        }

        public static int Deobfuscate(this int intValue)
        {
            return intValue ^ ObfuscateIntKey;
        }


        public static uint Obfuscate(this uint intValue)
        {
            return intValue ^ ObfuscateUIntKey;
        }

        public static uint Deobfuscate(this uint intValue)
        {
            return intValue ^ ObfuscateUIntKey;
        }


        public static long Obfuscate(this long intValue)
        {
            return intValue ^ ObfuscateLongKey;
        }

        public static long Deobfuscate(this long intValue)
        {
            return intValue ^ ObfuscateLongKey;
        }


        public static ulong Obfuscate(this ulong intValue)
        {
            return intValue ^ ObfuscateULongKey;
        }

        public static ulong Deobfuscate(this ulong intValue)
        {
            return intValue ^ ObfuscateULongKey;
        }


        public static bool HasDiff(this float thisDouble, float otherDouble)
        {
            return Math.Abs(thisDouble - otherDouble) > FloatEpsilon;
        }

        public static bool HasDiff(this double thisDouble, double otherDouble)
        {
            return Math.Abs(thisDouble - otherDouble) > DoubleEpsilon;
        }


        public static int[] Convert(this ObfuscateInt[] obfuscateInts)
        {
            if (obfuscateInts == null)
                return default;

            var ints = new int[obfuscateInts.Length];
            for (int i = 0; i < obfuscateInts.Length; i++)
            {
                ints[i] = obfuscateInts[i];
            }

            return ints;

        }

        public static ObfuscateInt[] Convert(this int[] ints)
        {
            if (ints == null)
                return default;

            var obfuscateInts = new ObfuscateInt[ints.Length];
            for (int i = 0; i < ints.Length; i++)
            {
                obfuscateInts[i] = ints[i];
            }

            return obfuscateInts;

        }
    }
}
