using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField]
    private InstrumentArray[] SongData;
    private int songIndex = 0;
    private AudioSource[] audioSources = new AudioSource[GameSettings.NUM_INSTRUMENTS];
    private bool[] instrumentPlaying = new bool[GameSettings.NUM_INSTRUMENTS];
    private ControlAbstractor[] controls = new ControlAbstractor[GameSettings.MAX_PLAYERS];
    private int numControls = 0;

    void Start() {
        songIndex = Random.Range(0,SongData.Length);
        for (int i = 0; i < GameSettings.NUM_INSTRUMENTS; i++) {
            GameObject childObj = new GameObject();
            childObj.transform.parent = transform;
            childObj.name = "AudioSource" + i;
            audioSources[i] = (AudioSource)childObj.AddComponent(typeof(AudioSource));
            audioSources[i].clip = SongData[songIndex].mArray[i];
            audioSources[i].Play();
        }

        RoundController.OnRoundChange += HandleRoundChange;
    }

    void OnDestroy() {
        RoundController.OnRoundChange -= HandleRoundChange;
    }

    public void HandleRoundChange() {
        // Mute audio depending on control data.
        for (int i = 0; i < GameSettings.NUM_INSTRUMENTS; i++) {
            instrumentPlaying[i] = false;
        }
        for (int i = 0; i < numControls; i++) {
            int instrumentIndex = (int)controls[i].GetInstrument();
            if (instrumentIndex < (int)GameSettings.INSTRUMENT.NONE) {
                instrumentPlaying[instrumentIndex] = true;
            }
        }
        for (int i = 0; i < GameSettings.NUM_INSTRUMENTS; i++) {
            if (instrumentPlaying[i]) {
                audioSources[i].mute = false;
            } else {
                audioSources[i].mute = true;
            }
        }
    }

    public void RegisterControl(ControlAbstractor control) {
        controls[numControls++] = control;
    }
}

// Necessary to see multi-dim array in inspector
[System.Serializable]
class InstrumentArray {
    public AudioClip[] mArray = new AudioClip[GameSettings.NUM_INSTRUMENTS];
}
