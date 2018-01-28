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

    private const float FLASH_TEXT = 0.1f;
    private float flashTime = 0.0f;
    private bool flashed = false;

    void Start() {
        prevInstrument = GameSettings.INSTRUMENT.NONE;
        currentInstrument = GameSettings.INSTRUMENT.NONE;
        playerText = GetComponentInChildren<Text>();
    }

    void Update() {
        Color flashColour = Color.white;
        if (mPlayer != null) {
            if (mPlayer.GetButton(GameSettings.REWIRED_MAIN_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.DRUM;
                flashColour = GameSettings.FanColours[(int)GameSettings.INSTRUMENT.DRUM];
            } else if (mPlayer.GetButton(GameSettings.REWIRED_SECONDARY_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.BASS;
                flashColour = GameSettings.FanColours[(int)GameSettings.INSTRUMENT.BASS];
            } else if (mPlayer.GetButton(GameSettings.REWIRED_THIRD_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.TRUMPET;
                flashColour = GameSettings.FanColours[(int)GameSettings.INSTRUMENT.TRUMPET];
            } else if (mPlayer.GetButton(GameSettings.REWIRED_FOURTH_BTN)) {
                currentInstrument = GameSettings.INSTRUMENT.FLUTE;
                flashColour = GameSettings.FanColours[(int)GameSettings.INSTRUMENT.FLUTE];
            }
        }
        if (flashed) {
            flashTime += Time.deltaTime;
            if (flashTime > FLASH_TEXT) {
                flashed = false;
                playerText.color = Color.white;
            }
        }
        if (flashColour != Color.white) {
            flashed = true;
            playerText.color = flashColour;
            flashTime = 0.0f;
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
