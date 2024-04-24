using System;
using System.Collections.Generic;
using System.Numerics;
using SmartMonkey.Objects;

namespace BlazorCanvasTest2.Models
{
    public class DNA {

        private List<Vector2> Genes;
        private double maxForce = 0.5;
        public static readonly Random random = new Random();

        public DNA(int lifeSpan) {
            CreateGenomes(lifeSpan);
        }

        public Vector2 GetStep(int index)
        {
            return Genes[index];
        }

        public int GetLifeSpan()
        {
            return Genes.Count;
        }

        private void CreateGenomes(int lifeSpan) {
            Genes = new List<Vector2>();
            for (var i = 0; i < lifeSpan; i++) {
                var step = new Vector2((float)(Utils.GetRandomDouble() - Utils.GetRandomFloat(-2.5, 3.5)), (float)(Utils.GetRandomDouble() - 1.5));
                Genes.Add(Vector2.Multiply(step, (float)Utils.GetRandomDouble(maxForce)));
            }
        }

    }
}

