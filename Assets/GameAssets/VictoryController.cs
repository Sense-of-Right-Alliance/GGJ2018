using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {
    public int numRounds = 6;

    private int roundCount = 0;

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
            Debug.Log("Game is over! Show the score screen!");
        }
    }
}
