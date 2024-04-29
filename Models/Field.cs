using SmartMonkey.Objects;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace BlazorCanvasTest2.Models
{
    public class Field
    {
        public readonly Population popultation = new Population();
        public double Width { get; private set; }
        public double Height { get; private set; }

        public void Resize(double width, double height) =>
            (Width, Height) = (width, height);

        public void StepForward()
        {
            foreach (Individual individual in popultation.GetPopulation())
                individual.StepForward();
        }

        /*        private double RandomVelocity(Random rand, double min, double max)
                {
                    double v = min + (max - min) * rand.NextDouble();
                    if (rand.NextDouble() > .5)
                        v *= -1;
                    return v;
                }*/

        public void InitializeField(Vector2 start, int lifespand, int count = 10)
        {
            popultation.Initialize(start, lifespand, count);
        }
    }
}
