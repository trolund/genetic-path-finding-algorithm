using System;
using System.Drawing;
using System.Numerics;
using System.Linq;
using blazor_canvas_ga_path_finding.Models;

namespace Controllers
{
    public class DNA
    {

        private Vector2[] Genes;
        private double MaxForce = 10;
        public static readonly Random random = new Random();
        public int LifeSpan;

        public DNA(int lifeSpan, double maxForce)
        {
            LifeSpan = lifeSpan;
            MaxForce = maxForce;
            CreateGenomes();
        }

        public Vector2 GetStep(int index)
        {
            return Genes[index];
        }

        public int GetLifeSpan()
        {
            return LifeSpan;
        }

        private void CreateGenomes()
        {
            Genes = new Vector2[LifeSpan];
            for (var i = 0; i < LifeSpan; i++)
            {
                Genes[i] = CreateBiasVector();
            }
        }

        /// <summary>
        ///  single point corssover between parent1 and parent2
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <returns></returns>
        public Individual Crossover(Individual parent1, Individual parent2)
        {
            Individual child = new Individual(parent1.Start, new Vector2(0, 0), parent1.R, Utils.ToHex(Utils.ToColor(parent1.Color).Blend(Utils.ToColor(parent2.Color), 0.5)), parent1.Dna.GetLifeSpan(), parent1.Dna.MaxForce);

            // Point of Crossover
            int crossoverPoint = Utils.random.Next(parent1.Dna.GetLifeSpan());

            Vector2[] childDna = parent1.Dna.Genes.Take(crossoverPoint)
                            .Concat(parent2.Dna.Genes.Skip(crossoverPoint))
                            .ToArray();

            child.Dna.Genes = childDna;

            return child;
        }

        private Vector2 CreateBiasVector()
        {
            return Vector2.Add(Vector2.Multiply(new Vector2((float)(Utils.GetRandomDouble() - Utils.GetRandomFloat(-6.5, 7.5)), (float)(Utils.GetRandomDouble() - 1.5)), (float)Utils.GetRandomDouble(MaxForce)), Vector2.One);
        }

        public Color Mutate(double mutationRate, Color c)
        {
            int r = c.R;
            int g = c.G;
            int b = c.B;

            for (int i = 0; i < Genes.Length; i++)
            {
                if (Utils.GetRandomDouble() < mutationRate)
                {
                    int val = random.Next(0, 3);
                    int pm = random.Next(0, 2);

                    switch (val)
                    {
                        case 0:
                            r += pm > 0 ? 5 : -5;
                            break;
                        case 1:
                            g += pm > 0 ? 5 : -5;
                            break;
                        case 2:
                            b += pm > 0 ? 5 : -5;
                            break;
                    }

                    Genes[i] = CreateBiasVector();
                }
            }

            return Color.FromArgb(Bounds(r), Bounds(g), Bounds(b));
        }

        private int Bounds(int x)
        {
            if (x > 255)
            {
                return 255;
            }
            if (x < 0)
            {
                return 0;
            }

            return x;
        }

    }
}

