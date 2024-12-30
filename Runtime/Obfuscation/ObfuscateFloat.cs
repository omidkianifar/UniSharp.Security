using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace UniSharp.Security.Obfuscation
{
    [JsonConverter(typeof(ObfuscateFloatJsonConverter))]
    public struct ObfuscateFloat : IEquatable<ObfuscateFloat>, IFormattable
    {
        private int obfuscatedValue;
        private float shadowValue;

        public static event Action<ObfuscateFloat> OnCheetDetected;


        public static implicit operator float(ObfuscateFloat value)
        {
            return Deobfuscate(value);
        }

        public static implicit operator ObfuscateFloat(float value)
        {
            return new ObfuscateFloat()
            {
                obfuscatedValue = Obfuscate(value),
                shadowValue = value
            };
        }


        private static int Obfuscate(float value)
        {
            var union = new FloatUnion
            {
                f = value
            };

            union.i = union.i.Obfuscate();

            return union.i;
        }

        private static float Deobfuscate(ObfuscateFloat value)
        {
            var union = new FloatUnion
            {
                i = value.obfuscatedValue
            };

            union.i = union.i.Deobfuscate();

            if (union.f.HasDiff(value.shadowValue))
                OnCheetDetected?.Invoke(value);

            return union.f;
        }


        public bool Equals(ObfuscateFloat other)
        {
            float thisObject = Deobfuscate(this);
            float otherObject = Deobfuscate(other);

            if (float.IsNaN(thisObject) && float.IsNaN(otherObject))
                return true;

            return thisObject == otherObject;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObfuscateFloat))
                return false;

            return Equals((ObfuscateFloat)obj);
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

    [StructLayout(LayoutKind.Explicit)]
    internal struct FloatUnion
    {
        [FieldOffset(0)]
        public float f;

        [FieldOffset(0)]
        public int i;
    }

    public class ObfuscateFloatJsonConverter : JsonConverter<ObfuscateFloat>
    {
        public override ObfuscateFloat ReadJson(JsonReader reader, Type objectType, ObfuscateFloat existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ObfuscateFloat obfuscatedValue = Convert.ToSingle(reader.Value);

            return obfuscatedValue;
        }

        public override void WriteJson(JsonWriter writer, ObfuscateFloat value, JsonSerializer serializer)
        {
            float number = value;
            writer.WriteValue(number);
        }
    }
}
