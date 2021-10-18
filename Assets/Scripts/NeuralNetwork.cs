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
using System;

using MathNet.Numerics.LinearAlgebra;

using Random = UnityEngine.Random;

public class NeuralNetwork : MonoBehaviour
{
    // Variables needed for the Neural Network
    public Matrix<float> inputLayer = Matrix<float>.Build.Dense(1, 3);

    public List<Matrix<float>> hiddenLayers = new List<Matrix<float>>();

    public Matrix<float> outputLayer = Matrix<float>.Build.Dense(1, 2);

    public List<Matrix<float>> weights = new List<Matrix<float>>();

    public List<float> biases = new List<float>();

    public float fitness;

    // Initialize the Neural Network
    public void Init(int hiddenLayer, int hiddenNeuron)
    {
        inputLayer.Clear();
        hiddenLayers.Clear();
        outputLayer.Clear();
        weights.Clear();
        biases.Clear();

        for (int i = 0; i < hiddenLayer + 1; i++)
        {

            Matrix<float> f = Matrix<float>.Build.Dense(1, hiddenNeuron);

            hiddenLayers.Add(f);

            biases.Add(Random.Range(-1f, 1f));

            //WEIGHTS
            if (i == 0)
            {
                Matrix<float> inputToH1 = Matrix<float>.Build.Dense(3, hiddenNeuron);
                weights.Add(inputToH1);
            }

            Matrix<float> HiddenToHidden = Matrix<float>.Build.Dense(hiddenNeuron, hiddenNeuron);
            weights.Add(HiddenToHidden);

        }

        Matrix<float> OutputWeight = Matrix<float>.Build.Dense(hiddenNeuron, 2);
        weights.Add(OutputWeight);
        biases.Add(Random.Range(-1f, 1f));

        RandomWeights();
    }

    // Initialize a copy of the Neural Network
    public NeuralNetwork InitCopy(int hiddenLayer, int hiddenNeuron)
    {
        NeuralNetwork n = new NeuralNetwork();

        List<Matrix<float>> newWeights = new List<Matrix<float>>();

        for (int i = 0; i < this.weights.Count; i++)
        {
            Matrix<float> currentWeight = Matrix<float>.Build.Dense(weights[i].RowCount, weights[i].ColumnCount);

            for (int x = 0; x < currentWeight.RowCount; x++)
            {
                for (int y = 0; y < currentWeight.ColumnCount; y++)
                {
                    currentWeight[x, y] = weights[i][x, y];
                }
            }

            newWeights.Add(currentWeight);
        }

        List<float> newBiases = new List<float>();

        newBiases.AddRange(biases);

        n.weights = newWeights;
        n.biases = newBiases;

        n.InitHidden(hiddenLayer, hiddenNeuron);

        return n;
    }

    // Initialize the hidden layer and neuron
    public void InitHidden(int hiddenLayer, int hiddenNeuron)
    {
        inputLayer.Clear();
        hiddenLayers.Clear();
        outputLayer.Clear();

        for (int i = 0; i < hiddenLayer + 1; i++)
        {
            Matrix<float> newHiddenLayer = Matrix<float>.Build.Dense(1, hiddenNeuron);
            hiddenLayers.Add(newHiddenLayer);
        }

    }

    // Randomize the weights
    public void RandomWeights()
    {

        for (int i = 0; i < weights.Count; i++)
        {

            for (int x = 0; x < weights[i].RowCount; x++)
            {

                for (int y = 0; y < weights[i].ColumnCount; y++)
                {

                    weights[i][x, y] = Random.Range(-1f, 1f);

                }

            }

        }

    }

    // Run the network and output the acceleration and steering
    public (float, float) PlayNetwork(float a, float b, float c)
    {
        inputLayer[0, 0] = a;
        inputLayer[0, 1] = b;
        inputLayer[0, 2] = c;

        inputLayer = inputLayer.PointwiseTanh();

        hiddenLayers[0] = ((inputLayer * weights[0]) + biases[0]).PointwiseTanh();

        for (int i = 1; i < hiddenLayers.Count; i++)
        {
            hiddenLayers[i] = ((hiddenLayers[i - 1] * weights[i]) + biases[i]).PointwiseTanh();
        }

        outputLayer = ((hiddenLayers[hiddenLayers.Count - 1] * weights[weights.Count - 1]) + biases[biases.Count - 1]).PointwiseTanh();

        //First output is acceleration and second output is steering
        return (Sigmoid(outputLayer[0, 0]), (float)Math.Tanh(outputLayer[0, 1]));
    }

    // Sigmoid function used in PlayNetwork
    private float Sigmoid(float s)
    {
        return (1 / (1 + Mathf.Exp(-s)));
    }
}
