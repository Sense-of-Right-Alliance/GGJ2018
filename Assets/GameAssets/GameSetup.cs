using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    private const float STAGE_SPAWN_X = 0.3f;
    private const float STAGE_SPAWN_Y = 0.2f;

    [SerializeField]
    private GameObject StagePrefab;

	// Use this for initialization
	void Start () {
        // We're assuming there will always be 4 players, and that some of them may be AI.
        Instantiate(StagePrefab, new Vector3(-STAGE_SPAWN_X, -STAGE_SPAWN_Y, 0.0f), Quaternion.identity);
        Instantiate(StagePrefab, new Vector3(-STAGE_SPAWN_X, STAGE_SPAWN_Y, 0.0f), Quaternion.identity);
        Instantiate(StagePrefab, new Vector3(STAGE_SPAWN_X, -STAGE_SPAWN_Y, 0.0f), Quaternion.identity);
        Instantiate(StagePrefab, new Vector3(STAGE_SPAWN_X, STAGE_SPAWN_Y, 0.0f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
