using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput : ControlAbstractor {

    private GameSettings.INSTRUMENT currentInstrument;

    void Start() {
        currentInstrument = (GameSettings.INSTRUMENT)Random.Range(0, GameSettings.NUM_INSTRUMENTS);
    }

    public override GameSettings.INSTRUMENT GetInstrument() {
        // TODO-DG: ha ha ha

        return currentInstrument;
    }
}
