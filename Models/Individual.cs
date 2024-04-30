using System;
using System.Collections.Generic;
using System.Numerics;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvasTest2.Models
{
    public class Individual
    {
        public Vector2 Pos { get; set; }
        public Vector2 Vel { get; private set; }
        public double R { get; private set; }
        public string Color { get; set; }
        public bool Alive { get; private set; }
        public DNA Dna { get; set; }
        public double GeneIndex { get; set; }
        public double Fitness { get; set; }
        public Vector2 Start { get; set; }

        public Individual(Vector2 start, Vector2 vel, double radius, string color, int lifeSpan)
        {
            Alive = true;
            Dna = new DNA(lifeSpan);
            Pos = start;
            Vel = vel;
            Start = start;
            (R, Color) = (radius, color);
        }
        public void StepForward(float smoothness)
        {
            if (Alive) // only move if it is alive
            {
                ApplyForce(smoothness);
            }
        }
        public async void Display(Canvas2DContext ctx)
        {
            if (Alive) // only show ball if it is alive
            {
                await ctx.BeginPathAsync();
                await ctx.ArcAsync(Pos.X, Pos.Y, R, 0, 2 * Math.PI, false);
                await ctx.SetFillStyleAsync(Color);
                await ctx.FillAsync();
            }
        }
        public void HitObstacles(List<Wall> walls)
        {
            foreach (var wall in walls)
            {
                if(IsPointInsideRectangle(Pos.X, Pos.Y, wall.X, wall.Y, wall.X + wall.Width, wall.Y + wall.Height))
                {
                    Kill();
                }
            }
        }
        public void LeavingSpace(int width, int height)
        {
            // if individual have left the space then kill it
            if (Pos.X < 0 || Pos.Y < 0 || Pos.X > width || Pos.Y > height){
                Kill();
            }
        }
        private void ApplyForce(float smoothness)
        {
            if (GeneIndex < Dna.GetLifeSpan())
            {
                Pos = Pos + Vector2.Multiply(Dna.GetStep((int) Math.Floor(GeneIndex)), smoothness);
                GeneIndex += smoothness;
            }else {
                GeneIndex = Dna.GetLifeSpan();
            }
        }
        private void Kill()
        {
            GeneIndex = Dna.GetLifeSpan();
            Alive = false;
        }
        public double CalculateFitness(Vector2 target)
        {
            float dist = Math.Abs(Vector2.Distance(target, Pos));
            double fitness = 1.0 / (1.0 + dist);

            if (!Alive)
            {
                fitness = fitness - (fitness * 0.9);
            }

            Fitness = fitness * 100;
            return fitness;
        }
        private bool IsPointInsideRectangle(double x, double y, double x1, double y1, double x2, double y2)
        {
            // Check if x lies inside the x-coordinate range of the rectangle
            bool xInRange = (x + R) > x1 && (x + R) < x2 || (x - R) > x1 && (x - R) < x2;

            // Check if y lies inside the y-coordinate range of the rectangle
            bool yInRange = (y + R) > y1 && (y + R) < y2 || (y - R) > y1 && (y - R) < y2;

            // If both conditions are satisfied, the point is inside the rectangle
            var ShouldDie = xInRange && yInRange;

            return ShouldDie;
        }

    }
}
