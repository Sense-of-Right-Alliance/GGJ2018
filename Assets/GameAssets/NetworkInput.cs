using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput : ControlAbstractor {

    private GameSettings.INSTRUMENT_ACTIONS currentAction;

    void Start() {
        currentAction = (GameSettings.INSTRUMENT_ACTIONS)Random.Range(0, GameSettings.NUM_ACTIONS);
    }

    public override GameSettings.INSTRUMENT_ACTIONS GetAction() {
        // TODO-DG: ha ha ha

        return currentAction;
    }
}
