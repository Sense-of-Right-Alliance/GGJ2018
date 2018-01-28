using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class HumanInput : ControlAbstractor {

    private Player mPlayer;
    private GameSettings.INSTRUMENT_ACTIONS currentAction;
    private GameSettings.INSTRUMENT_ACTIONS prevAction;

    void Start() {
        prevAction = GameSettings.INSTRUMENT_ACTIONS.NONE;
        currentAction = GameSettings.INSTRUMENT_ACTIONS.NONE;
    }

    void Update() {
        if (mPlayer != null) {
            if (mPlayer.GetButtonDown(GameSettings.REWIRED_MAIN_BTN)) {
                Debug.Log("Main button pushed!");
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_0;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_SECONDARY_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_1;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_THIRD_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_2;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_FOURTH_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_3;
            }
        }
    }

    public override GameSettings.INSTRUMENT_ACTIONS GetAction() {
        if (currentAction != GameSettings.INSTRUMENT_ACTIONS.NONE) {
            prevAction = currentAction;
            currentAction = GameSettings.INSTRUMENT_ACTIONS.NONE;
        }
        return prevAction;
    }

    public void SetPlayer(Player newPlayer) {
        mPlayer = newPlayer;
    }
}
