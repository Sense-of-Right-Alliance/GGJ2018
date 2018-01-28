using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Rewired;

public class HumanInput : ControlAbstractor {

    private Player mPlayer;
    private GameSettings.INSTRUMENT currentInstrument;
    private GameSettings.INSTRUMENT prevInstrument;

    private Text playerText;

    private const float FLASH_TEXT = 0.3f;
    private float flashTime = 0.0f;
    private bool flashed = false;

    void Start() {
        prevInstrument = GameSettings.INSTRUMENT.NONE;
        currentInstrument = GameSettings.INSTRUMENT.NONE;
        playerText = GetComponentInChildren<Text>();
    }

    void Update() {
        bool pressed = false;
        if (mPlayer != null) {
            if (mPlayer.GetButtonDown(GameSettings.REWIRED_MAIN_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.DRUM;
                pressed = true;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_SECONDARY_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.BASS;
                pressed = true;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_THIRD_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.TRUMPET;
                pressed = true;
            } else if (mPlayer.GetButtonDown(GameSettings.REWIRED_FOURTH_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.FLUTE;
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

    public override GameSettings.INSTRUMENT GetInstrument() {
        if (currentInstrument != GameSettings.INSTRUMENT.NONE) {
            prevInstrument = currentInstrument;
            currentInstrument = GameSettings.INSTRUMENT.NONE;
        }
        return prevInstrument;
    }

    public void SetPlayer(Player newPlayer) {
        mPlayer = newPlayer;
    }
}
