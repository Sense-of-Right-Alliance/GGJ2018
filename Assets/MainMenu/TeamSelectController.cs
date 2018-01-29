using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Rewired;

public class TeamSelectController : MonoBehaviour {
    private const float INPUT_COOLDOWN = 0.18f;

    [SerializeField]
    private Sprite[] stageSprites;

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
        if (NumJoined < 1) {
            return;
        }
        int numToDisplay = 0;
        float controllerYRatio = 1.0f/NumJoined;
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            if (isJoined[i]) {
                nesControllerTransforms[i].anchorMin = new Vector2(numToDisplay*controllerYRatio, nesControllerTransforms[i].anchorMin.y);
                nesControllerTransforms[i].anchorMax = new Vector2(++numToDisplay*controllerYRatio, nesControllerTransforms[i].anchorMax.y);
            }
        }
    }

    public void Reset() {
        startCooldown = 0.0f;
		for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            rewiredPlayers[i] = ReInput.players.GetPlayer(i);
            isJoined[i] = false;
            nesControllerTransforms[i].gameObject.SetActive(false);
        }
    }

	void Awake() {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            nesControllerTransforms[i] = Instantiate(nesControllerPrefab).GetComponent<RectTransform>();
            nesControllerTransforms[i].SetParent(controllerHolderTransform);
            nesControllerTransforms[i].GetComponentInChildren<Text>().text = "P" + (i+1);
            nesControllerTransforms[i].localScale = new Vector3(1,1,1);
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
                            isJoined[i] = true;
                            nesControllerTransforms[i].gameObject.GetComponentInChildren<Image>().sprite = stageSprites[i];
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
                    for (int j = 0; j < GameSettings.MAX_PLAYERS; j++) {
                        if (isJoined[j]) {
                            GameSettings.PlayerTypes[j] = GameSettings.PLAYER_TYPES.HUMAN;
                        } else {
                            GameSettings.PlayerTypes[j] = GameSettings.PLAYER_TYPES.AI;
                        }
                        
                    }
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
