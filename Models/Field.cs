using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace BlazorCanvasTest2.Models
{
    public class Field
    {
        public readonly List<Ball> Balls = new List<Ball>();
        public double Width { get; private set; }
        public double Height { get; private set; }

        public void Resize(double width, double height) =>
            (Width, Height) = (width, height);

        public void StepForward()
        {
            foreach (Ball ball in Balls)
                ball.StepForward(Width, Height);
        }

        private double RandomVelocity(Random rand, double min, double max)
        {
            double v = min + (max - min) * rand.NextDouble();
            if (rand.NextDouble() > .5)
                v *= -1;
            return v;
        }

        public void AddToStart(Vector2 start, int lifespand, int count = 10)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                Balls.Add(
                    new Ball(
                        start: start,
                        vel: new Vector2(0, 0),
                        radius: 10,
                        color: string.Format("#{0:X6}", rand.Next(0xFFFFFF)),
                        lifespand
                    )
                ); ;
            }
        }
    }
}
