﻿using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void incrementScore(int points)
    {
        score += points;
    }

    public int getScore()
    {
        return score;
    }
}
