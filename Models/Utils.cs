using System;
using System.Runtime.InteropServices;

namespace SmartMonkey.Objects
{
	public class Utils
	{
        private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
        public static readonly Random random = new Random();

        public static char GetRandomCharacter()
            {
                return CharSet[random.Next(CharSet.Length)];
            }

            public static double GetRandomDouble()
            {
                return random.NextDouble();
            }
	}
}

