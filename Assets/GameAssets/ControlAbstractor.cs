using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAbstractor : MonoBehaviour {

    public virtual GameSettings.INSTRUMENT GetInstrument() {
        // Needs to be defined in abstracting classes
        return GameSettings.INSTRUMENT.NONE;
    }
}
