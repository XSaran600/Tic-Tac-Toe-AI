// Saran Krishnaraja 100621699
// Code from: https://github.com/tclemente/TicTacToeMinimax/blob/master/TicTacToe/Assets/script.cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Main code used to run the game
public class TicTacToe : MonoBehaviour
{
    // X and O Prefab
    public GameObject XPrefab;
    public GameObject OPrefab;

    // Sound Variables
    public AudioClip sound;
    private AudioSource audioSource;

    // TicTacToe Grid
    Node[,] TicTacToeGrid = new Node[3, 3];

    // List of Scores
    public List<MoveandScore> scores;

    // Win & Lose text
    public Text textUI;

    bool seven = false;
    bool eight = false;
    bool nine = false;
    bool four = false;
    bool five = false;
    bool six = false;
    bool one = false;
    bool two = false;
    bool three = false;

    //void functions
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        GameLoop();
    }

    public void OnClick7()
    {
        seven = true;
    }
    public void OnClick8()
    {
        eight = true;
    }
    public void OnClick9()
    {
        nine = true;
    }
    public void OnClick4()
    {
        four = true;
    }
    public void OnClick5()
    {
        five = true;
    }
    public void OnClick6()
    {
        six = true;
    }
    public void OnClick1()
    {
        one = true;
    }
    public void OnClick2()
    {
        two = true;
    }
    public void OnClick3()
    {
        three = true;
    }

    public void GameLoop()
    {
        // If game isn't done
        if (!gameOver())
        {
            // If corresponding key on keypad is pressed check if the move is possible
            // Put X prefab on correct spot
            // Then check the Min and Max and let the AI do his move and place the O prefab
            if (seven && canMove(0, 0))
            {
                Instantiate(XPrefab, new Vector3(0, 1, 0), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 0;
                node.y = 0;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;

                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (eight && canMove(0, 1))
            {
                Instantiate(XPrefab, new Vector3(0, 1, 1), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 0;
                node.y = 1;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;

                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (nine && canMove(0, 2))
            {
                Instantiate(XPrefab, new Vector3(0, 1, 2), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 0;
                node.y = 2;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;

                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (four && canMove(1, 0))
            {
                Instantiate(XPrefab, new Vector3(1, 1, 0), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 1;
                node.y = 0;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;

                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (five && canMove(1, 1))
            {
                Instantiate(XPrefab, new Vector3(1, 1, 1), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 1;
                node.y = 1;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;
               
                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (six && canMove(1, 2))
            {
                Instantiate(XPrefab, new Vector3(1, 1, 2), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 1;
                node.y = 2;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;

                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (one && canMove(2, 0))
            {
                Instantiate(XPrefab, new Vector3(2, 1, 0), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 2;
                node.y = 0;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;

                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (two && canMove(2, 1))
            {
                Instantiate(XPrefab, new Vector3(2, 1, 1), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 2;
                node.y = 1;
                node.pVal = 1;

                TicTacToeGrid[node.x, node.y] = node;
              
                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }

            if (three && canMove(2, 2))
            {
                Instantiate(XPrefab, new Vector3(2, 1, 2), Quaternion.identity);
                audioSource.PlayOneShot(sound);

                Node node = new Node();
                node.x = 2;
                node.y = 2;
                node.pVal = 1;
                TicTacToeGrid[node.x, node.y] = node;
               
                checkMinimumAndMaximum(0, 1);
                Node move = AIMove();
                if (canMove(move.x, move.y))
                {
                    Instantiate(OPrefab, new Vector3(move.x, 1, move.y), Quaternion.identity);
                    audioSource.PlayOneShot(sound);
                    move.pVal = 2;
                    TicTacToeGrid[move.x, move.y] = move;
                }
            }
        }
    }

    // Check the min and max
    public void checkMinimumAndMaximum(int d, int t)
    {
        scores = new List<MoveandScore>();
        MinMax(d, t);
    }

    // Checks if the game is over
    public bool gameOver()
    {
        // If all the places are taken it's a draw
        if (getMoves().Capacity == 0)
        {
            textUI.text = "Draw";
            return true;
        }

        // Checks if AI lost
        if (getAILost())
        {
            textUI.text = "Win";
            return true;
        }

        // Checks if Player lost
        if (getPlayerLost())
        {
            textUI.text = "Lose";
            return true;
        }

        return false;
    }

    // Function to check win
    bool CheckWin(int num)
    {
        // Diagonal check
        if (TicTacToeGrid[0, 0] != null && TicTacToeGrid[1, 1] != null && TicTacToeGrid[2, 2] != null)
        {
            if (TicTacToeGrid[0, 0].pVal == TicTacToeGrid[1, 1].pVal && TicTacToeGrid[0, 0].pVal == TicTacToeGrid[2, 2].pVal && TicTacToeGrid[0, 0].pVal == num)
            {
                return true;
            }
        }
        if (TicTacToeGrid[0, 2] != null && TicTacToeGrid[1, 1] != null && TicTacToeGrid[2, 0] != null)
        {
            if (TicTacToeGrid[0, 2].pVal == TicTacToeGrid[1, 1].pVal && TicTacToeGrid[0, 2].pVal == TicTacToeGrid[2, 0].pVal && TicTacToeGrid[0, 2].pVal == num)
            {
                return true;
            }
        }

        // Check columns and rows
        for (int c = 0; c < 3; c++)
        {
            if (TicTacToeGrid[c, 0] != null && TicTacToeGrid[c, 1] != null && TicTacToeGrid[c, 2] != null)
            {
                if (TicTacToeGrid[c, 0].pVal == TicTacToeGrid[c, 1].pVal && TicTacToeGrid[c, 0].pVal == TicTacToeGrid[c, 2].pVal && TicTacToeGrid[c, 0].pVal == num)
                {
                    return true;
                }
            }

            if (TicTacToeGrid[0, c] != null && TicTacToeGrid[1, c] != null && TicTacToeGrid[2, c] != null)
            {
                if (TicTacToeGrid[0, c].pVal == TicTacToeGrid[1, c].pVal && TicTacToeGrid[0, c].pVal == TicTacToeGrid[2, c].pVal && TicTacToeGrid[0, c].pVal == num)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Function to check if the player lost
    public bool getPlayerLost()
    {
        return CheckWin(2);
    }

    // Function to check if AI lost
    public bool getAILost()
    {
        return CheckWin(1);
    }

    // Function to check if you can move
    public bool canMove(int x, int y)
    {
        if (TicTacToeGrid[x, y] == null)
        {
            return true;
        }

        else
            return false;
    }

    // Moves the AI
    public Node AIMove()
    {
        int maximum = -999999999;
        int minimum = -1;

        // Go through them to find the best possible move
        for (int i = 0; i < scores.Count; i++)
        {
           if (maximum < scores[i].score && canMove(scores[i].node.x, scores[i].node.y))
           {
                maximum = scores[i].score;
                minimum = i;
           }
        }

        if (minimum > -1)
        {
            return scores[minimum].node;
        }

        Node empty = new Node();
        empty.x = 0;
        empty.y = 0;

        return empty;

    }

    // Returns open moves
    List<Node> getMoves()
    {
        List<Node> node = new List<Node>();

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (TicTacToeGrid[x, y] == null)
                {
                    Node newNode = new Node();

                    newNode.x = x;
                    newNode.y = y;

                    node.Add(newNode);
                }
            }
        }
        return node;
    }

    // Finds the minimum from the list
    public int getMinimum(List<int> list)
    {
        int min = 999999999;
        int currentIndex = -1;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] < min)
            {
                // Set the min
                min = list[i];
                // Set current index to i
                currentIndex = i;
            }
        }
        return list[currentIndex];
    }

    // Finds the maximum from the list
    public int getMaximun(List<int> list)
    {
        int max = -999999999;
        int currentIndex = -1;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] > max)
            {
                // Set the max
                max = list[i];
                // Set current index to i
                currentIndex = i;
            }
        }
        return list[currentIndex];
    }

    // Function function the Min and Max
    public int MinMax(int d, int t)
    {
        if (getPlayerLost()) 
            return +1;

        if (getAILost()) 
            return -1;

        List<Node> availableSpaces = getMoves();
        
        //if no more spaces available then return 0 for our minmax
        if (availableSpaces.Capacity == 0)
            return 0;

        List<int> finalScores = new List<int>();

        for (int s = 0; s < availableSpaces.Count; s++)
        {
            Node space = availableSpaces[s];

            // If first turn then select highest value
            if (t == 1)
            {
                Node xPlayer = new Node();

                //set node int values
                xPlayer.x = space.x;
                xPlayer.y = space.y;
                xPlayer.pVal = 2;

                TicTacToeGrid[space.x, space.y] = xPlayer;

                // Set current score and add to the list
                int currentScore = MinMax(d + 1, 2);
                finalScores.Add(currentScore);

                if (d == 0)
                {
                    // Create a MoveandScore to set our space and current score
                    MoveandScore mv = new MoveandScore(currentScore, space);
                    mv.node = space;
                    mv.score = currentScore;

                    // Add the MoveandScore to our list
                    scores.Add(mv);
                }

            }

            // If its the second turn the make sure to set the lowest value
            else if (t == 2)
            {
                Node oPlayer = new Node();
                oPlayer.x = space.x;
                oPlayer.y = space.y;
                oPlayer.pVal = 1;

                TicTacToeGrid[space.x, space.y] = oPlayer;

                int currScore = MinMax(d + 1, 1);
                finalScores.Add(currScore);
            }
            
            // Reset space
            TicTacToeGrid[space.x, space.y] = null;

        }

        return t == 1 ? getMaximun(finalScores) : getMinimum(finalScores);
    }

}

