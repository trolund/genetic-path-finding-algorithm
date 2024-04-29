﻿using BlazorCanvasTest2.Models;

namespace SmartMonkey.Objects
{
    public class Individual_old
    {
        public string Genes { get; set; }
        public double Fitness { get; set; }

        public Individual_old(int length)
        {
            Genes = string.Empty;
            Fitness = 0;
            for (int i = 0; i < length; i++)
            {
                Genes += Utils.GetRandomCharacter();
            }
        }

        public void CalculateFitness(string target)
        {
            int score = 0;
            for (int i = 0; i < target.Length; i++)
            {
                if (Genes[i] == target[i])
                {
                    score++;
                }
            }
            Fitness = (double)score / target.Length;
        }

        public Individual_old Crossover(Individual_old partner)
        {
            Individual_old child = new Individual_old(Genes.Length);
            int midpoint = Utils.random.Next(Genes.Length);
            for (int i = 0; i < Genes.Length; i++)
            {
                if (i > midpoint)
                {
                    child.Genes = child.Genes.Substring(0, i) + Genes[i] + child.Genes.Substring(i + 1);
                }
                else
                {
                    child.Genes = child.Genes.Substring(0, i) + partner.Genes[i] + child.Genes.Substring(i + 1);
                }
            }
            return child;
        }

        public void Mutate(double mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Utils.GetRandomDouble() < mutationRate)
                {
                    Genes = Genes.Substring(0, i) + Utils.GetRandomCharacter() + Genes.Substring(i + 1);
                }
            }
        }
    }
}

