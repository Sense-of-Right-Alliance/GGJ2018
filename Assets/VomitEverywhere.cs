using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitEverywhere : MonoBehaviour {
    private const float LURCH_TIME = 0.2f;
    private const float EASE_TIME = 0.2f;
    private const float LURCH_PERCENTAGE = 1.01f;

    private Camera mCam;
    private float defaultCamSize = 3.3f;
    
    private bool lurching = false;
    private float opTime = 0.0f;
	void Start () {
        mCam = GetComponent<Camera>();
		defaultCamSize = mCam.orthographicSize;
	}
	
	void Update () {
        if (GameSettings.VOMIT_EVERYWHERE) {
            if (lurching) {
                opTime += Time.deltaTime;
                mCam.orthographicSize = Mathf.Lerp(defaultCamSize, defaultCamSize * LURCH_PERCENTAGE, opTime/LURCH_TIME);
                if (opTime > LURCH_TIME) {
                    opTime = 0.0f;
                    lurching = false;
                }
            } else {
                opTime += Time.deltaTime;
                mCam.orthographicSize = Mathf.Lerp(defaultCamSize * LURCH_PERCENTAGE, defaultCamSize / LURCH_PERCENTAGE, opTime/EASE_TIME);
                if (opTime > EASE_TIME) {
                    opTime = 0.0f;
                    lurching = true;
                }
            }
        }
	}
}
