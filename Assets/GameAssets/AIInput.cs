using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : ControlAbstractor {

    private const float CHANGE_PERCENTAGE = 0.3f;
    private GameSettings.INSTRUMENT currentInstrument;

    void Start() {
        currentInstrument = (GameSettings.INSTRUMENT)Random.Range(0, GameSettings.NUM_INSTRUMENTS);
    }

    public override GameSettings.INSTRUMENT GetInstrument() {
        float choiceMakin = Random.value;
        if (choiceMakin > CHANGE_PERCENTAGE) {
            // TODO-DG: More challenging AI, actually look at the people.
            return (GameSettings.INSTRUMENT)Random.Range(0, GameSettings.NUM_INSTRUMENTS);
        } else {
            return currentInstrument;
        }
    }
}
