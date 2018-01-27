using UnityEngine;
using System.Collections;

public class Tools : MonoBehaviour {

    public GameSettings.DEBUG_STATE DebugState = GameSettings.DEBUG_STATE.RELEASE;
    
    private GameSettings.DEBUG_STATE maxDebug = GameSettings.DEBUG_STATE.DEBUG_LITE;

    void Start() {
        if (Debug.isDebugBuild) {
            maxDebug++;
        }
    }
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1)) {
            DebugState = DebugState + 1;
            if (DebugState > maxDebug) {
                DebugState = GameSettings.DEBUG_STATE.RELEASE;
            }
        }
	}
}
