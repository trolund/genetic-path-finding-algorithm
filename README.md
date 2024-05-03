# Genetic algorithm used for path-finding

The project employs a genetic algorithm to discover a path from a starting point to a goal, both identified by black spots.

The candidate solutions, represented by colored circles, begin by moving somewhat randomly. In each generation, a fitness function evaluates their performance, rewarding progress towards the goal while penalizing deviations or collisions with obstacles.

Each candidate solution is represented by a sequence of movements, described as two-dimensional vectors. One point crossover is used to combine candidate solutions.

The mutation rate determines how frequently these movements will undergo changes, crucially introducing a randomizing factor.

The top candidate solutions from the population are chosen through Tournament or Rank selection methods.

[Demo link](https://trolund.github.io/Genetic-algorithm-path-finding/)

## First generation

![alt text](docs/gif1.gif)

## After multiple generations

![alt text](docs/gif2.gif)