﻿using Microsoft.Win32.SafeHandles;
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

            // random between min and max 
            public static float GetRandomFloat(double min, double max)
            {
                return (float) (random.NextDouble() * (max - min) + min);
        }

            public static double GetRandomDouble(double max)
            {
                return random.NextDouble() * max;
            }
	}
}

