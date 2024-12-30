using Newtonsoft.Json;
using System;

namespace UniSharp.Security.Obfuscation
{
    [JsonConverter(typeof(ObfuscateUIntJsonConverter))]
    public struct ObfuscateUInt : IEquatable<ObfuscateUInt>, IFormattable
    {
        private uint obfuscatedValue;
        private uint shadowValue;

        public static event Action<ObfuscateUInt> OnCheetDetected;


        public static implicit operator uint(ObfuscateUInt value)
        {
            return Deobfuscate(value);
        }

        public static implicit operator ObfuscateUInt(uint value)
        {
            return new ObfuscateUInt()
            {
                obfuscatedValue = Obfuscate(value),
                shadowValue = value
            };
        }


        private static uint Obfuscate(uint value)
        {
            return value.Obfuscate();
        }

        private static uint Deobfuscate(ObfuscateUInt value)
        {
            var uintValue = value.obfuscatedValue.Deobfuscate();

            if (uintValue != value.shadowValue)
                OnCheetDetected?.Invoke(value);

            return uintValue;
        }


        public bool Equals(ObfuscateUInt other)
        {
            return obfuscatedValue == other.obfuscatedValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObfuscateUInt))
                return false;

            return Equals((ObfuscateUInt)obj);
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

    public class ObfuscateUIntJsonConverter : JsonConverter<ObfuscateUInt>
    {
        public override ObfuscateUInt ReadJson(JsonReader reader, Type objectType, ObfuscateUInt existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ObfuscateUInt obfuscatedValue = Convert.ToUInt32(reader.Value);

            return obfuscatedValue;
        }

        public override void WriteJson(JsonWriter writer, ObfuscateUInt value, JsonSerializer serializer)
        {
            uint number = value;
            writer.WriteValue(number);
        }
    }
}
