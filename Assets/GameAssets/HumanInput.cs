using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class HumanInput : ControlAbstractor {

    private Player mPlayer;
    private GameSettings.INSTRUMENT_ACTIONS currentAction;

    void Start() {
        currentAction = (GameSettings.INSTRUMENT_ACTIONS)Random.Range(0, GameSettings.NUM_ACTIONS);
    }

    public override GameSettings.INSTRUMENT_ACTIONS GetAction() {
        // TODO-DG: Get our input from ReWired

        return currentAction;
    }
}
