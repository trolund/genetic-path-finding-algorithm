using System;
using System.Collections.Generic;
using System.Numerics;
using SmartMonkey.Objects;

namespace BlazorCanvasTest2.Models
{
    public class DNA {

        private List<Vector2> Genes = new List<Vector2>();
        private double maxForce = 0.1;
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
            for (var i = 0; i < lifeSpan; i++) {
                var step = new Vector2((float)Utils.GetRandomDouble(), (float)Utils.GetRandomDouble());
                Vector2.Multiply(step, (float)Utils.GetRandomDouble(maxForce));
            }
        }

    }
}

