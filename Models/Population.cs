﻿using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
namespace BlazorCanvasTest2.Models
{
    public class Population
    {
        public Individual[] Individuals { get; set; }
        public double BestFitness { get; set; }
        public Individual BestIndividual { get; set; }
        public int BestIndex { get; set; }
        public int Generation {  get; set; }   
        public Individual BestEver { get; set; }

        public void Initialize(Vector2 start, int populationSize, int lifespan)
        {
            Individuals = new Individual[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                Individuals[i] = new Individual(start, new Vector2(0,0), 10, Utils.RandomHexColor(), lifespan);
            }

            // just fitness 0
            BestEver = new Individual(start, new Vector2(0, 0), 10, Utils.RandomHexColor(), lifespan);
        }

        public void CalculateFitness(Vector2 target)
        {
            BestFitness = 0;
            foreach (Individual individual in Individuals)
            {
                individual.CalculateFitness(target);
                if (individual.Fitness > BestFitness)
                {
                    BestFitness = individual.Fitness;
                    BestIndividual = individual;
                    BestIndex = Array.IndexOf(Individuals, individual);
                }

                if (individual.Fitness > BestEver.Fitness)
                {
                    BestEver = individual;
                }
            }
        }


        public Individual TournamentSelection(int tournamentSize)
        {
            Individual[] tournament = new Individual[tournamentSize];
            for (int i = 0; i < tournamentSize; i++)
            {
                if(Utils.GetRandomDouble() < 0.1) 
                {
                    tournament[i] = Individuals[BestIndex];
                }
                else
                {
                    tournament[i] = Individuals[Utils.random.Next(Individuals.Length)];
                }
            }

            Array.Sort(tournament, (x, y) => y.Fitness.CompareTo(x.Fitness));
            return tournament[0]; // Return the fittest individual from the tournament
        }

        public void Selection()
        {
            Individual[] newPopulation = new Individual[Individuals.Length];
            for (int i = 0; i < Individuals.Length; i++)
            {
                newPopulation[i] = TournamentSelection(5); // Adjust tournament size as needed
            }

            newPopulation = newPopulation.OrderBy(x => x.Fitness).ToArray();

            // replace the x weakest agents with the best 
            for (int i = newPopulation.Length - 3; i < Individuals.Length; i++)
            {
                newPopulation[i] = BestEver;
            }

            Individuals = newPopulation;
        }

        public Individual RankSelection()
        {
            // Sort individuals based on their fitness
            Array.Sort(Individuals, (x, y) => y.Fitness.CompareTo(x.Fitness));

            // Calculate selection probabilities based on rank
            double totalProbability = (Individuals.Length * (Individuals.Length + 1)) / 2.0;
            double[] selectionProbabilities = new double[Individuals.Length];
            for (int i = 0; i < Individuals.Length; i++)
            {
                selectionProbabilities[i] = (i + 1) / totalProbability;
            }

            // Select an individual based on the selection probabilities
            double randomNumber = Utils.GetRandomDouble();
            double cumulativeProbability = 0;
            for (int i = 0; i < Individuals.Length; i++)
            {
                cumulativeProbability += selectionProbabilities[i];
                if (randomNumber <= cumulativeProbability)
                {
                    return Individuals[i];
                }
            }

            // If no individual is selected, return the first one (should not happen)
            return Individuals[0];
        }

        public void GenerateNextGeneration()
        {
            Individual[] newGeneration = new Individual[Individuals.Length];

            for (int i = 0; i < Individuals.Length; i++)
            {
                Individual parent1 = RankSelection();
                Individual parent2 = RankSelection();
                Individual child = parent1.dna.Crossover(parent1, parent2);
                child.Color = Utils.ToHex(child.dna.Mutate(0.1, Utils.ToColor(child.Color))); // TODO make it mach the controller
                newGeneration[i] = child;
            }

            Individuals = newGeneration;
        }

        public Individual[] GetPopulation()
        {
            return Individuals;
        }

        public bool IsGenerationDone()
        {
            return Individuals.All(a => a.geneIndex == a.dna.LifeSpan);
        }


        public void StepForward(Vector2 target)
        {

            if (IsGenerationDone())
            {
                CalculateFitness(target);
                Selection();
                GenerateNextGeneration();
                Generation++;

                Console.WriteLine($"Generation: {Generation}, Best Fitness: {BestFitness}");
            }
            else // make the agents move
            {
                foreach (Individual individual in Individuals)
                    individual.StepForward();
            }

        }

        /*        public bool IsFinished(string target)
                {
                    foreach (Individual_old individual in Individuals)
                    {
                        if (individual.Genes == target)
                        {
                            return true;
                        }
                    }
                    return false;
                }*/
    }
}

