using Newtonsoft.Json;
using System;

namespace UniSharp.Security.Obfuscation
{
    [JsonConverter(typeof(ObfuscateIntJsonConverter))]
    public struct ObfuscateInt : IEquatable<ObfuscateInt>, IFormattable
    {
        private int obfuscatedValue;
        private int shadowValue;

        public static event Action<ObfuscateInt> OnCheetDetected;


        public static implicit operator int(ObfuscateInt value)
        {
            return Deobfuscate(value);
        }

        public static implicit operator ObfuscateInt(int value)
        {
            return new ObfuscateInt()
            {
                obfuscatedValue = Obfuscate(value),
                shadowValue = value
            };
        }

        private static int Obfuscate(int value)
        {
            return value.Obfuscate();
        }

        private static int Deobfuscate(ObfuscateInt value)
        {
            var intValue = value.obfuscatedValue.Deobfuscate();

            if (intValue != value.shadowValue)
                OnCheetDetected?.Invoke(value);

            return intValue;
        }


        public bool Equals(ObfuscateInt other)
        {
            return obfuscatedValue == other.obfuscatedValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObfuscateInt))
                return false;

            return Equals((ObfuscateInt)obj);
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

    public class ObfuscateIntJsonConverter : JsonConverter<ObfuscateInt>
    {
        public override ObfuscateInt ReadJson(JsonReader reader, Type objectType, ObfuscateInt existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ObfuscateInt obfuscatedValue = Convert.ToInt32(reader.Value);

            return obfuscatedValue;
        }

        public override void WriteJson(JsonWriter writer, ObfuscateInt value, JsonSerializer serializer)
        {
            int number = value;
            writer.WriteValue(number);
        }
    }
}
