﻿using SmartMonkey.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorCanvasTest2.Models
{
    public class Individual
    {
        public Vector2 Pos { get; set; }
        public Vector2 Vel { get; private set; }
        public double R { get; private set; }
        public string Color { get; private set; }
        public bool Alive { get; private set; }
        public DNA dna { get; set; }
        public int geneIndex { get; set; }
        public double Fitness { get; set; }
        public Vector2 Start { get; set; }

        public Individual(Vector2 start, Vector2 vel, double radius, string color, int lifeSpan)
        {
            this.Alive = true;
            dna = new DNA(lifeSpan);
            Pos = start;
            Vel = vel;
            Start = start;
            (R, Color) = (radius, color);
        }

        public void StepForward()
        {
            if (Alive) // only move if it is alive
            {
                ApplyForce();
            }
        }

        private void ApplyForce()
        {
            if (geneIndex < dna.GetLifeSpan())
            {
                Pos = Pos + dna.GetStep(geneIndex);
                geneIndex++;
            }
        }

        public void Kill()
        {
            geneIndex = dna.GetLifeSpan();
            Alive = false;
        }

        public double CalculateFitness(Vector2 target)
        {
            float dist = Math.Abs(Vector2.Distance(target, Pos));
            double fitness = 1.0 / (1.0 + dist);

            if (!Alive)
            {
                fitness = fitness - (fitness * 0.2);
            }

            Fitness = fitness;
            return fitness;
        }

    }
}
