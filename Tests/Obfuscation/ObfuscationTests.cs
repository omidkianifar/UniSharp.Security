using UniSharp.Security.Obfuscation;
using Newtonsoft.Json;
using UnityEngine;
using NUnit.Framework;

namespace UniSharp.SecurityTest.Obfuscation
{
    public class ObfuscationTests
    {
        [Test]
        public void TestObfuscateInt()
        {
            ObfuscateInt a = 1;

            Debug.Log($"Secure Int: a -> to secure:{a}");

            ObfuscateInt b = 1;

            Debug.Log($"Secure Int: b -> to secure:{b}");

            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestObfuscateUInt() 
        {
            ObfuscateUInt a = 1;

            Debug.Log($"Secure Int: a -> to secure:{a}");

            ObfuscateUInt b = 1;

            Debug.Log($"Secure Int: b -> to secure:{b}");

            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestObfuscateLong()
        {
            ObfuscateLong a = 10000;

            Debug.Log($"Secure Long: a -> to secure:{a}");

            ObfuscateLong b = 10000;

            Debug.Log($"Secure Long: b -> to secure:{b}");

            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestObfuscateULong() 
        {
            ObfuscateULong a = 10000;

            Debug.Log($"Secure Long: a -> to secure:{a}");

            ObfuscateULong b = 10000;

            Debug.Log($"Secure Long: b -> to secure:{b}");

            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestObfuscateFloat()
        {
            ObfuscateFloat a = 1.5f;

            Debug.Log($"Secure Float: a -> to secure:{a}");

            ObfuscateFloat b = 1.5f;

            Debug.Log($"Secure Float: b -> to secure:{b}");

            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestObfuscateDouble()
        {
            ObfuscateDouble a = 1.5d;

            Debug.Log($"Secure Double: a -> to secure:{a}");

            ObfuscateDouble b = 1.5d;

            Debug.Log($"Secure Double: b -> to secure:{b}");

            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestObfuscateObject()
        {
            var sample = new Player
            {
                Coin = 100,
                Gem = 0,
                Life = 1.5d,
                Time = 20.11f
            };

            var toJson = Player.Serialize(sample);
            Debug.Log($"To Json: {toJson}");

            sample = Player.Deserialize(toJson);
            Debug.Log($"From Json:");
            Debug.Log($"Coin: {sample.Coin}");
            Debug.Log($"Gem: {sample.Gem}");
            Debug.Log($"Time: {sample.Time}");
            Debug.Log($"Life: {sample.Life}");
        }

        [Test]
        public void TestObfuscateObject1()
        {
            var sample = new People
            {
                Age = 10
            };

            var toJson = JsonConvert.SerializeObject(sample);
            Debug.Log($"To Json: {toJson}");

            sample = JsonConvert.DeserializeObject<People>(toJson);
            Debug.Log($"From Json:");
            Debug.Log($"Age: {sample.Age}");
        }

        [Test]
        public void TestObfuscateObject2() 
        {
            var ints = new ObfuscateInt[3] { 1, 2, 3 };
            foreach (var i in ints) Debug.Log(i);

            var toJson = JsonConvert.SerializeObject(ints);
            Debug.Log($"To Json: {toJson}");

            ints = JsonConvert.DeserializeObject<ObfuscateInt[]> (toJson);
            Debug.Log($"From Json:");
            foreach (var i in ints) Debug.Log(i);
        }
    }

    public class Player
    {
        [JsonIgnore] public ObfuscateInt Coin;
        [JsonIgnore] public ObfuscateLong Gem;
        [JsonIgnore] public ObfuscateFloat Time;
        [JsonIgnore] public ObfuscateDouble Life;

        [JsonProperty] int coin;
        [JsonProperty] long gem;
        [JsonProperty] float time;
        [JsonProperty] double life;

        public static string Serialize(Player obj)
        {
            obj.coin = obj.Coin;
            obj.gem = obj.Gem;
            obj.time = obj.Time;
            obj.life = obj.Life;

            return JsonConvert.SerializeObject(obj);
        }

        public static Player Deserialize(string str)
        {
            var obj = JsonConvert.DeserializeObject<Player>(str);

            obj.Coin = obj.coin;
            obj.Gem = obj.gem;
            obj.Time = obj.time;
            obj.Life = obj.life;

            return obj;
        }
    }

    public class People
    {
        public ObfuscateInt Age;
    }
}