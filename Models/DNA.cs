using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using SmartMonkey.Objects;
using System.Linq;

namespace BlazorCanvasTest2.Models
{
    public class DNA {

        private Vector2[] Genes;
        private double maxForce = 6;
        public static readonly Random random = new Random();
        public int LifeSpan;

        public DNA(int lifeSpan) {
            LifeSpan = lifeSpan;
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

        private void CreateGenomes() {
            Genes = new Vector2[LifeSpan];
            for (var i = 0; i < LifeSpan; i++) {
                var step = new Vector2((float)(Utils.GetRandomDouble() - Utils.GetRandomFloat(-3.5, 4.5)), (float)(Utils.GetRandomDouble() - 1.5));
                Genes[i] = Vector2.Multiply(step, (float)Utils.GetRandomDouble(maxForce));
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
            // TODO conbine colors of the childs
            Individual child = new Individual(parent1.Start, new Vector2(0, 0), parent1.R, parent1.Color, parent1.dna.GetLifeSpan());

            // Point of Crossover
            int crossoverPoint = Utils.random.Next(parent1.dna.GetLifeSpan());

            Vector2[] childDna = parent1.dna.Genes.Take(crossoverPoint)
                            .Concat(parent2.dna.Genes.Skip(crossoverPoint))
                            .ToArray();

            child.dna.Genes = childDna;

            return child;
        }

        public void Mutate(double mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Utils.GetRandomDouble() < mutationRate) 
                {
                    Genes[i] = Vector2.Multiply(new Vector2((float)(Utils.GetRandomDouble() - Utils.GetRandomFloat(-3.5, 4.5)), (float)(Utils.GetRandomDouble() - 1.5)), (float)Utils.GetRandomDouble(maxForce));
                }
            }
        }

    }
}

