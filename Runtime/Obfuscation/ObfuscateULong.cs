using Newtonsoft.Json;
using System;

namespace UniSharp.Security.Obfuscation
{
    [JsonConverter(typeof(ObfuscateUulongJsonConverter))]
    public struct ObfuscateULong : IEquatable<ObfuscateULong>, IFormattable
    {
        private ulong obfuscatedValue;
        private ulong shadowValue;

        public static event Action<ObfuscateULong> OnCheetDetected;


        public static implicit operator ulong(ObfuscateULong value)
        {
            return Deobfuscate(value);
        }

        public static implicit operator ObfuscateULong(ulong value)
        {
            return new ObfuscateULong()
            {
                obfuscatedValue = Obfuscate(value),
                shadowValue = value
            };
        }


        private static ulong Obfuscate(ulong value)
        {
            return value.Obfuscate();
        }

        private static ulong Deobfuscate(ObfuscateULong value)
        {
            var intValue = value.obfuscatedValue.Deobfuscate();

            if (intValue != value.shadowValue)
                OnCheetDetected?.Invoke(value);

            return intValue;
        }


        public bool Equals(ObfuscateULong other)
        {
            return obfuscatedValue == other.obfuscatedValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObfuscateULong))
                return false;

            return Equals((ObfuscateULong)obj);
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

    public class ObfuscateUulongJsonConverter : JsonConverter<ObfuscateULong>
    {
        public override ObfuscateULong ReadJson(JsonReader reader, Type objectType, ObfuscateULong existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ObfuscateULong obfuscatedValue = Convert.ToUInt64(reader.Value);

            return obfuscatedValue;
        }

        public override void WriteJson(JsonWriter writer, ObfuscateULong value, JsonSerializer serializer)
        {
            ulong number = value;
            writer.WriteValue(number);
        }
    }
}
