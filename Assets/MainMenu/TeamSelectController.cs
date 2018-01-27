using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Rewired;

public class TeamSelectController : MonoBehaviour {
    private const float INPUT_COOLDOWN = 0.18f;

    [SerializeField]
    private RectTransform controllerHolderTransform;

    private float startCooldown = 0.0f; // Used to avoid double start from main menu.

    [SerializeField]
    private MenuController menuController;
    [SerializeField]
    private GameObject startButton;

    // TODO-DG: structify these godawful arrays.
    [SerializeField]
    private GameObject nesControllerPrefab;
    private RectTransform[] nesControllerTransforms = new RectTransform[GameSettings.MAX_PLAYERS];
    private Player[] rewiredPlayers = new Player[GameSettings.MAX_PLAYERS];
    private bool[] isJoined = new bool[GameSettings.MAX_PLAYERS];
    private float[] inputCooldown = new float[GameSettings.MAX_PLAYERS];

    private int NumJoined {
    get { 
            int numJoined = 0;
            for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
                if (isJoined[i]) {
                    numJoined++;
                }
            }
            return numJoined;
        }
    }

    public void SizeControllers() {
        float controllerYRatio = 1.0f/NumJoined;
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            nesControllerTransforms[i].anchorMin = new Vector2(nesControllerTransforms[i].anchorMin.x, 1.0f - (i+1)*controllerYRatio);
            nesControllerTransforms[i].anchorMax = new Vector2(nesControllerTransforms[i].anchorMax.x, 1.0f - i*controllerYRatio);
        }
    }

    public void Reset() {
        startCooldown = 0.0f;
		for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            rewiredPlayers[i] = ReInput.players.GetPlayer(i);
            isJoined[i] = false;
        }
    }

	void Awake() {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            nesControllerTransforms[i] = Instantiate(nesControllerPrefab).GetComponent<RectTransform>();
            nesControllerTransforms[i].SetParent(controllerHolderTransform);
            nesControllerTransforms[i].GetComponentInChildren<Text>().text = "P" + (i+1);
            nesControllerTransforms[i].localScale = new Vector3(1,1,1);
            nesControllerTransforms[i].gameObject.SetActive(false);
        }
        Reset();
	}

	void Update () {
        if (menuController.isTeamState()) {        
            bool startPressed = ReInput.players.GetSystemPlayer().GetButtonDown(GameSettings.REWIRED_SYSTEM_START);
            bool changed = false;
            for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
                if (inputCooldown[i] < INPUT_COOLDOWN) {
                    inputCooldown[i] += Time.deltaTime;
                } else {
                    if (rewiredPlayers[i].GetButtonDown(GameSettings.REWIRED_MAIN_BTN)) {
                        if (!isJoined[i]) {
                            Debug.Log("Join!");
                            isJoined[i] = true;
                            nesControllerTransforms[i].gameObject.SetActive(true);
                            changed = true;
                        }
                    } else if (rewiredPlayers[i].GetButtonDown(GameSettings.REWIRED_SECONDARY_BTN)) {
                        if (isJoined[i]) {
                            isJoined[i] = false;
                            nesControllerTransforms[i].gameObject.SetActive(false);
                            changed = true;
                        }
                    }
                }

                if (rewiredPlayers[i].GetButtonDown(GameSettings.REWIRED_START)) {
                    startPressed = true;
                }
            }

            if (changed) {
                SizeControllers();
            }

            if (startValid()) {
                startButton.SetActive(true);
                if (startPressed && startCooldown > INPUT_COOLDOWN) {
                    // If any player pressed start go to the next screen
                    // Determine how many players there are and where they should live.
                    
                    menuController.Forward();
                }
            } else {
                startButton.SetActive(false);
            }
            if (startCooldown < INPUT_COOLDOWN) {
                startCooldown += Time.deltaTime;
            }
        }
	}

    /**
     * This method determines whether "Press Start" should be displayed and usable
     */
    private bool startValid() {
        // true if one of the teams has at least one human player. None for attract screen?
        return (NumJoined > 1);
    }
}
