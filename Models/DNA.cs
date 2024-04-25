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
        private double maxForce = 2.5;
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
        ///  single point corssover between 
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <returns></returns>
        public Ball Crossover(Ball parent1, Ball parent2)
        {
            // TODO conbine colors of the childs
            Ball child = new Ball(parent1.Start, new Vector2(0, 0), parent1.R, parent1.Color, parent1.dna.GetLifeSpan());

            // Point of Crossover
            int crossoverPoint = Utils.random.Next(parent1.dna.GetLifeSpan());

            var genes1 = parent1.dna.Genes;
            var genes2 = parent2.dna.Genes;

            // TODO combiner the two sides 
            Vector2[] childDna = genes1.Take(crossoverPoint)
                            .Concat(genes2.Skip(crossoverPoint))
                            .ToArray();

            child.dna.Genes = childDna;

            return child;
        }

    }
}

