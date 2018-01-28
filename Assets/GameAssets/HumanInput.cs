using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Rewired;

public class HumanInput : ControlAbstractor {

    private Player mPlayer;
    private GameSettings.INSTRUMENT_ACTIONS currentAction;
    private GameSettings.INSTRUMENT_ACTIONS prevAction;

    private Text playerText;

    private const float FLASH_TEXT = 0.3f;
    private float flashTime = 0.0f;
    private bool flashed = false;

    void Start() {
        prevAction = GameSettings.INSTRUMENT_ACTIONS.NONE;
        currentAction = GameSettings.INSTRUMENT_ACTIONS.NONE;
        playerText = GetComponentInChildren<Text>();
    }

    void Update() {
        bool pressed = false;
        if (mPlayer != null) {
            if (mPlayer.GetButtonDown(GameSettings.REWIRED_MAIN_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_0;
                pressed = true;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_SECONDARY_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_1;
                pressed = true;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_THIRD_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_2;
                pressed = true;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_FOURTH_BTN)) {
                currentAction = GameSettings.INSTRUMENT_ACTIONS.ACTION_3;
                pressed = true;
            }
        }
        if (flashed) {
            flashTime += Time.deltaTime;
            if (flashTime > FLASH_TEXT) {
                flashed = false;
                playerText.color = Color.white;
            }
        } else {
            if (pressed) {
                flashed = true;
                playerText.color = new Color(1.0f, 0.5f, 0.0f);
                flashTime = 0.0f;
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
