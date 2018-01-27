using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour {
    public delegate void RoundAction();
    public static event RoundAction OnRoundChange;

    [SerializeField]
    private float roundTime = 5.0f;

    private float roundTimer = 0.0f;

    // Use this for initialization
    void Start () {
        roundTimer = roundTime; // NOTE: Round change is not called at the very start of the game
    }
	
	// Update is called once per frame
	void Update () {
        roundTimer -= Time.deltaTime;

        if (roundTimer <= 0.0f)
        {
            ChangeRound();
        }
    }

    void ChangeRound() {
        if (OnRoundChange != null) {
            OnRoundChange();
        }

        roundTimer = roundTime;
    }
}
