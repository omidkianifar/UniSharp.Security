using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace UniSharp.Security.Obfuscation
{
    [JsonConverter(typeof(ObfuscateDoubleJsonConverter))]
    public struct ObfuscateDouble : IEquatable<ObfuscateDouble>, IFormattable
    {
        private long obfuscatedValue;
        private double shadowValue;

        public static event Action<ObfuscateDouble> OnCheetDetected;


        public static implicit operator double(ObfuscateDouble value)
        {
            return Deobfuscate(value);
        }

        public static implicit operator ObfuscateDouble(double value)
        {
            return new ObfuscateDouble()
            {
                obfuscatedValue = Obfuscate(value),
                shadowValue = value
            };
        }


        private static long Obfuscate(double value)
        {
            var union = new DoubleUnion
            {
                d = value
            };

            union.l = union.l.Obfuscate();

            return union.l;
        }

        private static double Deobfuscate(ObfuscateDouble value)
        {
            var union = new DoubleUnion
            {
                l = value.obfuscatedValue
            };

            union.l = union.l.Deobfuscate();

            if (union.d.HasDiff(value.shadowValue))
                OnCheetDetected?.Invoke(value);

            return union.d;
        }


        public bool Equals(ObfuscateDouble other)
        {
            double thisObject = Deobfuscate(this);
            double otherObject = Deobfuscate(other);

            if (double.IsNaN(thisObject) && double.IsNaN(otherObject))
                return true;

            return thisObject == otherObject;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObfuscateDouble))
                return false;

            return Equals((ObfuscateDouble)obj);
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
    internal struct DoubleUnion
    {
        [FieldOffset(0)]
        public double d;

        [FieldOffset(0)]
        public long l;
    }

    public class ObfuscateDoubleJsonConverter : JsonConverter<ObfuscateDouble>
    {
        public override ObfuscateDouble ReadJson(JsonReader reader, Type objectType, ObfuscateDouble existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ObfuscateDouble obfuscatedValue = Convert.ToDouble(reader.Value);

            return obfuscatedValue;
        }

        public override void WriteJson(JsonWriter writer, ObfuscateDouble value, JsonSerializer serializer)
        {
            double number = value;
            writer.WriteValue(number);
        }
    }
}
