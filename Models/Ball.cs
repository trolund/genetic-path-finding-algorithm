using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCanvasTest2.Models
{
    public class Ball
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double XVel { get; private set; }
        public double YVel { get; private set; }
        public double R { get; private set; }
        public string Color { get; private set; }

        public bool Alive { get; private set; }

        public Ball(double x, double y, double xVel, double yVel, double radius, string color)
        {
            this.Alive = true;
            (X, Y, XVel, YVel, R, Color) = (x, y, xVel, yVel, radius, color);
        }

        public void StepForward(double width, double height)
        {
            if (Alive) // only move if it is alive
            {
                X += XVel;
                Y += YVel;
                if (X < 0 || X > width)
                    XVel *= -1;
                if (Y < 0 || Y > height)
                    YVel *= -1;

                if (X < 0)
                    X += 0 - X;
                else if (X > width)
                    X -= X - width;

                if (Y < 0)
                    Y += 0 - Y;
                if (Y > height)
                    Y -= Y - height;
            }
        }

        public void Kill()
        {
            Alive = false;
        }
    }
}
