﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Rewired;

public class GameSetup : MonoBehaviour {

    private const float STAGE_SPAWN_X = 0.25f;
    private const float STAGE_SPACING = 3f; //0.2f;
    private const float STAGE_SPAWN_Y = 1f; //0.2f;

    //private const float TEXT_DIST_X = 5.0f;

    [SerializeField]
    private GameObject Stage1Prefab;
    [SerializeField]
    private GameObject Stage2Prefab;
    [SerializeField]
    private GameObject Stage3Prefab;
    [SerializeField]
    private GameObject Stage4Prefab;

    GameObject[] stageFabs;

    private FanController _fanController;

    private Vector3[] stagePositions = new Vector3[] {
        new Vector3(-STAGE_SPACING * 1.5f, STAGE_SPAWN_Y, 0.0f),//new Vector3(-STAGE_SPAWN_X, STAGE_SPAWN_Y, 0.0f),
        new Vector3(-STAGE_SPACING * 0.5f, STAGE_SPAWN_Y, 0.0f),//new Vector3(STAGE_SPAWN_X, STAGE_SPAWN_Y, 0.0f),
        new Vector3(STAGE_SPACING * 0.5f, STAGE_SPAWN_Y, 0.0f),//new Vector3(STAGE_SPAWN_X, -STAGE_SPAWN_Y, 0.0f),
        new Vector3(STAGE_SPACING * 1.5f, STAGE_SPAWN_Y, 0.0f),//new Vector3(-STAGE_SPAWN_X, -STAGE_SPAWN_Y, 0.0f),
    };

    /*private Vector3[] textPositions = new Vector3[] {
        new Vector3(-TEXT_DIST_X, 0.0f, 0.0f),
        new Vector3(TEXT_DIST_X, 0.0f, 0.0f),
        new Vector3(TEXT_DIST_X, 0.0f, 0.0f),
        new Vector3(-TEXT_DIST_X, 0.0f, 0.0f),
    };*/

    [SerializeField]
    private MusicController musicController;

	// Use this for initialization
	void Awake () {
	    _fanController = GetComponent<FanController>();

        stageFabs = new GameObject[4];
        stageFabs[0] = Stage1Prefab;
        stageFabs[1] = Stage2Prefab;
        stageFabs[2] = Stage3Prefab;
        stageFabs[3] = Stage4Prefab;

        int reWiredIndex = 0;
        for (int i = 0; i < Math.Min(GameSettings.MAX_PLAYERS, GameSettings.PlayerTypes.Length); i++) {
            GameObject newStage = Instantiate(stageFabs[i % stageFabs.Length], stagePositions[i], Quaternion.identity);
            Text playerText = newStage.GetComponentInChildren<Text>();
            switch (GameSettings.PlayerTypes[i]) {
                case GameSettings.PLAYER_TYPES.HUMAN:
                    HumanInput newInput = (HumanInput)newStage.AddComponent(typeof(HumanInput));
                    newInput.SetPlayer(ReInput.players.GetPlayer(reWiredIndex++));
                    playerText.text = "P" + reWiredIndex;
                    break;
                case GameSettings.PLAYER_TYPES.AI:
                    newStage.AddComponent(typeof(AIInput));
                    playerText.text = "CPU";
                    break;
                case GameSettings.PLAYER_TYPES.NETWORKED:
                    newStage.AddComponent(typeof(NetworkInput));
                break;
            }
            ControlAbstractor control = newStage.GetComponent<ControlAbstractor>();
            if (musicController && control) {
                musicController.RegisterControl(control);
            }
            newStage.GetComponent<StageManager>().UpdateController();

            _fanController.Stages.Add((GameSettings.STAGE)i, newStage.GetComponent<StageManager>());
        }
	}
}
