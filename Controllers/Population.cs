using System;
using System.Linq;
using System.Numerics;
using blazor_canvas_ga_path_finding.Models;

namespace Controllers
{
    public class Population
    {
        public Individual[] Individuals { get; set; }
        public double BestFitness { get; set; }
        public Individual BestIndividual { get; set; }
        public int BestIndex { get; set; }
        public int Generation { get; set; }
        public Individual BestEver { get; set; }
        private SelectionMethod SelectionMethod { get; set; }

        public Population(SelectionMethod selectionMethod)
        {
            SelectionMethod=selectionMethod;
        }

        public void Initialize(Vector2 start, int populationSize, int lifespan, double maxForce)
        {
            Individuals = new Individual[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                Individuals[i] = new Individual(start, new Vector2(0, 0), 10, Utils.RandomHexColor(), lifespan, maxForce);
            }

            // just fitness 0
            BestEver = new Individual(start, new Vector2(0, 0), 10, Utils.RandomHexColor(), lifespan, maxForce);
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

                // set new best ever
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
                if (Utils.GetRandomDouble() < 0.2)
                {
                    // tournament[i] = Utils.GetRandomDouble() < 0.1 ? Individuals[BestIndex] : BestEver; // choose alltime best or gereration best
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
            double totalProbability = Individuals.Length * (Individuals.Length + 1) / 2.0;
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

        public void GenerateNextGeneration(double mutationRate)
        {
            Individual[] newGeneration = new Individual[Individuals.Length];

            for (int i = 0; i < Individuals.Length; i++)
            {
                Individual parent1 = SelectionMethod == SelectionMethod.Ranked ? RankSelection() : TournamentSelection(5);
                Individual parent2 = SelectionMethod == SelectionMethod.Ranked ? RankSelection() : TournamentSelection(5);
                Individual child = parent1.Dna.Crossover(parent1, parent2);
                child.Color = Utils.ToHex(child.Dna.Mutate(mutationRate, Utils.ToColor(child.Color))); // TODO make it mach the controller
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
            return Individuals.All(a => a.GeneIndex >= a.Dna.LifeSpan);
        }


        public void StepForward(Vector2 target, double mutationRate, float smoothness)
        {
            if (IsGenerationDone())
            {
                CalculateFitness(target);
                Selection();
                GenerateNextGeneration(mutationRate);
                Generation++;

                Console.WriteLine($"Generation: {Generation}, Best Fitness in generation: {BestFitness}, Best Fitness: {BestEver.Fitness}");
            }
            else // make the agents move
            {
                foreach (Individual individual in Individuals)
                    individual.StepForward(smoothness);
            }

        }
    }
}

