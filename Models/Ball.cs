using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace BlazorCanvasTest2.Models
{
    public class Ball
    {
        public Vector2 pos { get; set; }
        public double XVel { get; private set; }
        public double YVel { get; private set; }
        public double R { get; private set; }
        public string Color { get; private set; }
        public bool Alive { get; private set; }
        public DNA dna { get; private set; }
        private int geneIndex = 0;

        public Ball(float x, float y, double xVel, double yVel, double radius, string color)
        {
            this.Alive = true;
            dna = new DNA(500);
            pos = new Vector2(x, y);
            (XVel, YVel, R, Color) = (xVel, yVel, radius, color);
        }

        public void StepForward(double width, double height)
        {
            if (Alive) // only move if it is alive
            {
                ApplyForce();
            }
        }

        private void ApplyForce()
        {
            Console.WriteLine("applyForce 2");
            if (geneIndex < 100)
            {
                Console.WriteLine("applyForce, num of steps:", dna.GetLifeSpan());
                var step = dna.GetStep(geneIndex);
                Console.WriteLine("step:" + step);
                pos = pos + step;
                geneIndex++;
            }
        }

        public void Kill()
        {
            Alive = false;
        }
    }
}
