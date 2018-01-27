using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {
    public RoundController roundController;
    public int numRounds = 6;

    private int roundCount = 0;

    void Awake()
    {
        if (roundController == null)
        {
            // roundController = new MockRoundController(); // TODO
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
        Debug.Log("Round changed!! OH NOOO!");
        roundCount += 1;

        if (roundCount >= numRounds)
        {
            // pause the game
            // show the score screen
            Debug.Log("Game is over! Show the score screen!");
        }
    }
}
