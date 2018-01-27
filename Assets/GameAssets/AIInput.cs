using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : ControlAbstractor {

    private const float CHANGE_PERCENTAGE = 0.3f;
    private GameSettings.INSTRUMENT_ACTIONS currentAction;

    void Start() {
        currentAction = (GameSettings.INSTRUMENT_ACTIONS)Random.Range(0, GameSettings.NUM_ACTIONS);
    }

    public override GameSettings.INSTRUMENT_ACTIONS GetAction() {
        float choiceMakin = Random.value;
        if (choiceMakin > CHANGE_PERCENTAGE) {
            // TODO-DG: More challenging AI, actually look at the people.
            return (GameSettings.INSTRUMENT_ACTIONS)Random.Range(0, GameSettings.NUM_ACTIONS);
        } else {
            return currentAction;
        }
    }
}
