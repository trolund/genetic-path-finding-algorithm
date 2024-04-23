using System;
namespace SmartMonkey.Objects
{
    public class Population
    {
        public Individual[] Individuals { get; set; }
        public double BestFitness { get; set; }
        public string BestIndividual { get; set; }
        public int BestIndex { get; set; }

        public void Initialize(int populationSize, int targetLength)
        {
            Individuals = new Individual[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                Individuals[i] = new Individual(targetLength);
            }
        }

        public void CalculateFitness(string target)
        {
            BestFitness = 0;
            BestIndividual = string.Empty;
            foreach (Individual individual in Individuals)
            {
                individual.CalculateFitness(target);
                if (individual.Fitness > BestFitness)
                {
                    BestFitness = individual.Fitness;
                    BestIndividual = individual.Genes;
                    BestIndex = Array.IndexOf(Individuals, individual);
                }
            }
        }


        public Individual TournamentSelection(int tournamentSize)
        {
            Individual[] tournament = new Individual[tournamentSize];
            for (int i = 0; i < tournamentSize; i++)
            {
                if(Utils.GetRandomDouble() < 0.1) // cheat? (add previous best 10% of the time)
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
                Individual child = parent1.Crossover(parent2);
                child.Mutate(0.01); // TODO make it mach the controller
                newGeneration[i] = child;
            }

            Individuals = newGeneration;
        }

        public string GetFinal(string target)
        {
            foreach (Individual individual in Individuals)
            {
                if (individual.Genes == target)
                {
                    return individual.Genes;
                }
            }
            return "";
        }

        public bool IsFinished(string target)
        {
            foreach (Individual individual in Individuals)
            {
                if (individual.Genes == target)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

