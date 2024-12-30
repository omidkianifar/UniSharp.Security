using Newtonsoft.Json;
using System;

namespace UniSharp.Security.Obfuscation
{
    [JsonConverter(typeof(ObfuscateLongJsonConverter))]
    public struct ObfuscateLong : IEquatable<ObfuscateLong>, IFormattable
    {
        private long obfuscatedValue;
        private long shadowValue;

        public static event Action<ObfuscateLong> OnCheetDetected;


        public static implicit operator long(ObfuscateLong value)
        {
            return Deobfuscate(value);
        }

        public static implicit operator ObfuscateLong(long value)
        {
            return new ObfuscateLong()
            {
                obfuscatedValue = Obfuscate(value),
                shadowValue = value
            };
        }


        private static long Obfuscate(long value)
        {
            return value.Obfuscate();
        }

        private static long Deobfuscate(ObfuscateLong value)
        {
            var intValue = value.obfuscatedValue.Deobfuscate();

            if (intValue != value.shadowValue)
                OnCheetDetected?.Invoke(value);

            return intValue;
        }


        public bool Equals(ObfuscateLong other)
        {
            return obfuscatedValue == other.obfuscatedValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObfuscateLong))
                return false;

            return Equals((ObfuscateLong)obj);
        }

        public override int GetHashCode()
        {
            return Deobfuscate(this).GetHashCode();
        }

        public override string ToString()
        {
            return Deobfuscate(this).ToString();
        }

        public string ToString(string format)
        {
            return Deobfuscate(this).ToString(format);
        }

        public string ToString(IFormatProvider provider)
        {
            return Deobfuscate(this).ToString(provider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return Deobfuscate(this).ToString(format, provider);
        }
    }

    public class ObfuscateLongJsonConverter : JsonConverter<ObfuscateLong>
    {
        public override ObfuscateLong ReadJson(JsonReader reader, Type objectType, ObfuscateLong existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ObfuscateLong obfuscatedValue = Convert.ToInt64(reader.Value);

            return obfuscatedValue;
        }

        public override void WriteJson(JsonWriter writer, ObfuscateLong value, JsonSerializer serializer)
        {
            long number = value;
            writer.WriteValue(number);
        }
    }
}
