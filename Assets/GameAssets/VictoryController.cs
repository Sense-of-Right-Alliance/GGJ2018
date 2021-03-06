﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {

    public static event RoundController.RoundAction OnGameEnd;

    ScoreManager scoreManager;

    public RoundController roundController;
    public FanController fanController;
    public int numRounds = 6;

    private int roundCount = 0;

    public bool IsLastRound { get { return roundCount == numRounds-1; } }

    void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();

        if (fanController == null)
        {
            fanController = GetComponent<FanController>();
        }
    }

    // Use this for initialization
    void Start () {
        RoundController.OnRoundChange += HandleRoundChanged;
    }
    
    void OnDestroy()
    {
        RoundController.OnRoundChange -= HandleRoundChanged;
    }

    void HandleRoundChanged()
    {
        roundCount += 1;

        if (roundCount >= numRounds)
        {
            // pause the game
            // show the score screen
            //Debug.Log("Game is over! Show the score screen!");

            Dictionary<GameSettings.STAGE, List<GameObject>> stageFans = fanController.StageFans;
            Dictionary<int, int> scores = new Dictionary<int, int>();
            foreach (var stage in stageFans) // initalize stageFans dictionary
            {
                scores[(int)stage.Key] = stage.Value.Count;
            }

            if (OnGameEnd != null)
            {
                OnGameEnd();
            }

            Time.timeScale = 0.0f;
            scoreManager.ShowScores(scores);
        }
    }
}
