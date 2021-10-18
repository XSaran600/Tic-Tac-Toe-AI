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

// Make sure we have a Neural Network component
[RequireComponent(typeof(NeuralNetwork))]
public class BoatControls : MonoBehaviour
{
    // Public Variables
    [Range(-1f, 1f)]
    public float a, t;

    public float lifeTime = 0f;

    public float overallFitness;
    public float distanceMultipler = 1.4f;
    public float averageSpeedMultiplier = 0.2f;
    public float sensorMultiplier = 0.1f;

    public int layers = 1;
    public int neurons = 10;

    // Private Variable
    private Vector3 startPosition, startRotation;
    private NeuralNetwork network;

    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float averageSpeed;

    private float aSensor, bSensor, cSensor;

    // Get the start position, rotation and neural network
    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        network = GetComponent<NeuralNetwork>();


    }

    // Reset the Neural Network with the network intact
    public void ResetNetwork(NeuralNetwork net)
    {
        network = net;
        Reset();
    }

    // Reset Variables
    public void Reset()
    {

        lifeTime = 0f;
        totalDistanceTravelled = 0f;
        averageSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }

    // When the boat collides AI dies
    private void OnCollisionEnter(Collision collision)
    {
        Death();
    }

    // Update function that moves the boat and runs the Neural Network
    private void FixedUpdate()
    {

        Sensors();
        lastPosition = transform.position;


        (a, t) = network.PlayNetwork(aSensor, bSensor, cSensor);


        Move(a, t);

        lifeTime += Time.deltaTime;

        CalculateTheFitness();

        //a = 0;
        //t = 0;


    }

    // Death
    private void Death()
    {
        GameObject.FindObjectOfType<GeneticMutationManager>().Death(overallFitness, network);
    }

    // Calculates the fitness of the AI
    private void CalculateTheFitness()
    {

        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        averageSpeed = totalDistanceTravelled / lifeTime;

        overallFitness = (totalDistanceTravelled * distanceMultipler) + (averageSpeed * averageSpeedMultiplier) + (((aSensor + bSensor + cSensor) / 3) * sensorMultiplier);

        if (lifeTime > 20 && overallFitness < 40)
        {
            Death();
        }

        if (overallFitness >= 1000)
        {
            Death();
        }

    }

    // Three sensors to detect walls
    private void Sensors()
    {

        Vector3 a = (transform.forward + transform.right);
        Vector3 b = (transform.forward);
        Vector3 c = (transform.forward - transform.right);

        Ray r = new Ray(transform.position, a);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        {
            aSensor = hit.distance / 20;
            Debug.DrawLine(r.origin, hit.point, Color.red);
        }

        r.direction = b;

        if (Physics.Raycast(r, out hit))
        {
            bSensor = hit.distance / 20;
            Debug.DrawLine(r.origin, hit.point, Color.red);
        }

        r.direction = c;

        if (Physics.Raycast(r, out hit))
        {
            cSensor = hit.distance / 20;
            Debug.DrawLine(r.origin, hit.point, Color.red);
        }

    }

    // Move function that moves the boat
    private Vector3 input;
    public void Move(float v, float h)
    {
        input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, v * 11.4f), 0.02f);
        input = transform.TransformDirection(input);
        transform.position += input;

        transform.eulerAngles += new Vector3(0, (h * 90) * 0.02f, 0);
    }
}
