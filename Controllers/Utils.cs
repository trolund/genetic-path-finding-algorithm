using System;
using System.Drawing;

namespace Controllers
{
    public static class Utils
    {
        public static readonly Random random = new Random();

        public static double GetRandomDouble()
        {
            return random.NextDouble();
        }

        // random between min and max 
        public static float GetRandomFloat(double min, double max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }

        public static double GetRandomDouble(double max)
        {
            return random.NextDouble() * max;
        }

        public static string RandomHexColor()
        {
            return string.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
        }

        /// <summary>Blends the specified colors together.</summary>
        /// <param name="color">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amount">How much of <paramref name="color"/> to keep,
        /// “on top of” <paramref name="backColor"/>.</param>
        /// <returns>The blended colors.</returns>
        public static Color Blend(this Color color, Color backColor, double amount)
        {
            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        public static string ToHex(Color c)
        {
            return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        public static Color ToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }
    }


}

