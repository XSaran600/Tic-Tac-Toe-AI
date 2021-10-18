// Saran Krishnaraja 100621699
// Justin Collier 100345263
// Jarvis Ortega 100625970
// Bosco Kan 100621465

// Code referenced from:
// https://www.youtube.com/watch?v=C6SZUU8XQQ0
// https://www.youtube.com/watch?v=VYQZ-kjP1ec
// https://www.youtube.com/watch?v=J3_iAC1q1NM&list=WL&index=94&t=0s


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MathNet.Numerics.LinearAlgebra;

public class GeneticMutationManager : MonoBehaviour
{
    // Public Variables
    public BoatControls boatController;

    public int initPopulation = 85;
    [Range(0.0f, 1.0f)]
    public float geneMutationRate = 0.055f;

    public int bestSelection = 8;
    public int worstSelection = 3;
    public int numCrossover;


    public int currentGen;
    public int currentGenome = 0;

    // Private Variables
    private List<int> genePool = new List<int>();

    private int naturallySelected;

    private NeuralNetwork[] population;

    private void Start()
    {
        InitPopulation();
    }

    // Create the initial population of the Neural Network
    private void InitPopulation()
    {
        population = new NeuralNetwork[initPopulation];
        FillPopulation(population, 0);
        ResetCurrentGenome();
    }

    // Reset the current genome
    private void ResetCurrentGenome()
    {
        boatController.ResetNetwork(population[currentGenome]);
    }

    // Fill the neural network population
    private void FillPopulation(NeuralNetwork[] newPop, int index)
    {
        while (index < initPopulation)
        {
            newPop[index] = new NeuralNetwork();
            newPop[index].Init(boatController.layers, boatController.neurons);
            index++;
        }
    }

    // Call function when AI dies
    public void Death(float fitness, NeuralNetwork network)
    {

        if (currentGenome < population.Length - 1)
        {

            population[currentGenome].fitness = fitness;
            currentGenome++;
            ResetCurrentGenome();

        }
        else
        {
            Repopulate();
        }

    }

    // Repopulate neural network
    private void Repopulate()
    {
        genePool.Clear();
        currentGen++;
        naturallySelected = 0;
        SortPopulation();

        NeuralNetwork[] newPop = BestPopulation();

        Crossover(newPop);
        Mutation(newPop);

        FillPopulation(newPop, naturallySelected);

        population = newPop;

        currentGenome = 0;

        ResetCurrentGenome();

    }
    
    // Mutate the neural network AI 
    private void Mutation(NeuralNetwork[] newPop)
    {

        for (int i = 0; i < naturallySelected; i++)
        {

            for (int c = 0; c < newPop[i].weights.Count; c++)
            {

                if (Random.Range(0.0f, 1.0f) < geneMutationRate)
                {
                    newPop[i].weights[c] = MutationMatrix(newPop[i].weights[c]);
                }

            }

        }

    }

    // Mutate the matrix
    Matrix<float> MutationMatrix(Matrix<float> A)
    {

        int randomPoints = Random.Range(1, (A.RowCount * A.ColumnCount) / 7);

        Matrix<float> C = A;

        for (int i = 0; i < randomPoints; i++)
        {
            int randomColumn = Random.Range(0, C.ColumnCount);
            int randomRow = Random.Range(0, C.RowCount);

            C[randomRow, randomColumn] = Mathf.Clamp(C[randomRow, randomColumn] + Random.Range(-1f, 1f), -1f, 1f);
        }

        return C;

    }

    // Crossovers the Neural Networks weights and biases
    private void Crossover(NeuralNetwork[] newPop)
    {
        for (int i = 0; i < numCrossover; i += 2)
        {
            int AIndex = i;
            int BIndex = i + 1;

            if (genePool.Count >= 1)
            {
                for (int l = 0; l < 100; l++)
                {
                    AIndex = genePool[Random.Range(0, genePool.Count)];
                    BIndex = genePool[Random.Range(0, genePool.Count)];

                    if (AIndex != BIndex)
                        break;
                }
            }

            NeuralNetwork Child1 = new NeuralNetwork();
            NeuralNetwork Child2 = new NeuralNetwork();

            Child1.Init(boatController.layers, boatController.neurons);
            Child2.Init(boatController.layers, boatController.neurons);

            Child1.fitness = 0;
            Child2.fitness = 0;


            for (int w = 0; w < Child1.weights.Count; w++)
            {

                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    Child1.weights[w] = population[AIndex].weights[w];
                    Child2.weights[w] = population[BIndex].weights[w];
                }
                else
                {
                    Child2.weights[w] = population[AIndex].weights[w];
                    Child1.weights[w] = population[BIndex].weights[w];
                }

            }


            for (int w = 0; w < Child1.biases.Count; w++)
            {

                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    Child1.biases[w] = population[AIndex].biases[w];
                    Child2.biases[w] = population[BIndex].biases[w];
                }
                else
                {
                    Child2.biases[w] = population[AIndex].biases[w];
                    Child1.biases[w] = population[BIndex].biases[w];
                }

            }

            newPop[naturallySelected] = Child1;
            naturallySelected++;

            newPop[naturallySelected] = Child2;
            naturallySelected++;

        }
    }

    // Select the best in the population
    private NeuralNetwork[] BestPopulation()
    {

        NeuralNetwork[] newPop = new NeuralNetwork[initPopulation];

        for (int i = 0; i < bestSelection; i++)
        {
            newPop[naturallySelected] = population[i].InitCopy(boatController.layers, boatController.neurons);
            newPop[naturallySelected].fitness = 0;
            naturallySelected++;

            int f = Mathf.RoundToInt(population[i].fitness * 10);

            for (int c = 0; c < f; c++)
            {
                genePool.Add(i);
            }

        }

        for (int i = 0; i < worstSelection; i++)
        {
            int last = population.Length - 1;
            last -= i;

            int f = Mathf.RoundToInt(population[last].fitness * 10);

            for (int c = 0; c < f; c++)
            {
                genePool.Add(last);
            }

        }

        return newPop;

    }

    // Sort the Neural Netowrk population
    private void SortPopulation()
    {
        for (int i = 0; i < population.Length; i++)
        {
            for (int j = i; j < population.Length; j++)
            {
                if (population[i].fitness < population[j].fitness)
                {
                    NeuralNetwork temp = population[i];
                    population[i] = population[j];
                    population[j] = temp;
                }
            }
        }

    }
}
