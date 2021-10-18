// Saran Krishnaraja 100621699
// Code from: https://github.com/tclemente/TicTacToeMinimax/blob/master/TicTacToe/Assets/script.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to hold data of the node
public class Node
{
    public int x;
    public int y;
    public int pVal;
}

// Used to hold data of the score and node
public class MoveandScore
{
    public int score;
    public Node node;
    public MoveandScore(int s, Node n)
    {
        this.score = s;
        this.node = n;
    }
}