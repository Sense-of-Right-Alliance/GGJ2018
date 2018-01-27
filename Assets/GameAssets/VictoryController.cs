using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {

    ScoreManager scoreManager;

    public RoundController roundController;
    public int numRounds = 6;

    private int roundCount = 0;

    void Awake()
    {
        if (roundController == null)
        {
            // roundController = new MockRoundController(); // TODO
        }

        scoreManager = GetComponent<ScoreManager>();
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Starting Victory");
        RoundController.OnRoundChange += HandleRoundChanged;
    }
    
    void OnDestroy()
    {
        RoundController.OnRoundChange -= HandleRoundChanged;
    }

    void HandleRoundChanged()
    {
        Debug.Log("Round changed: " + numRounds);
        roundCount += 1;

        if (roundCount >= numRounds)
        {
            // pause the game
            // show the score screen
            //Debug.Log("Game is over! Show the score screen!");

            Dictionary<int, int> myDict = new Dictionary<int, int>
            {
                { 1, 100 },
                { 2, 16 },
                { 3, 5 }
            };

            Time.timeScale = 0.0f;
            scoreManager.GetComponent<ScoreManager>().ShowScores(myDict);
        }
    }
}
