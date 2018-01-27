using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class GameSetup : MonoBehaviour {

    private const float STAGE_SPAWN_X = 0.3f;
    private const float STAGE_SPAWN_Y = 0.2f;

    [SerializeField]
    private GameObject StagePrefab;

    private Vector3[] stagePositions = new Vector3[] {
        new Vector3(-STAGE_SPAWN_X, -STAGE_SPAWN_Y, 0.0f),
        new Vector3(-STAGE_SPAWN_X, STAGE_SPAWN_Y, 0.0f),
        new Vector3(STAGE_SPAWN_X, -STAGE_SPAWN_Y, 0.0f),
        new Vector3(STAGE_SPAWN_X, STAGE_SPAWN_Y, 0.0f)
    };

	// Use this for initialization
	void Awake () {
        int reWiredIndex = 0;
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            GameObject newStage = Instantiate(StagePrefab, stagePositions[i], Quaternion.identity);
            switch (GameSettings.PlayerTypes[i]) {
                case GameSettings.PLAYER_TYPES.HUMAN:
                    newStage.AddComponent(typeof(HumanInput));
                    HumanInput newInput = newStage.GetComponent<HumanInput>();
                    newInput.SetPlayer(ReInput.players.GetPlayer(reWiredIndex++));
                    break;
                case GameSettings.PLAYER_TYPES.AI:
                    newStage.AddComponent(typeof(AIInput));
                    break;
                case GameSettings.PLAYER_TYPES.NETWORKED:
                    newStage.AddComponent(typeof(NetworkInput));
                break;
            }
            newStage.GetComponent<StageManager>().UpdateController();
        }
	}
}
