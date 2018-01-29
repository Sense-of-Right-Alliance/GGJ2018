using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Rewired;

public class MenuController : MonoBehaviour {

    enum MENU_STATE {
        PRESS_START,
        MAIN_MENU,
        CONTROL_MAP,
        TEAM_SELECT

    }
    private MENU_STATE currentState = MENU_STATE.PRESS_START;

    [SerializeField]
    private Canvas splashCanvas;
    [SerializeField]
    private Canvas splashBackgroundCanvas;
    [SerializeField]
    private Canvas teamCanvas;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button controllerConfigButton;
    [SerializeField]
    private GameObject mainMenu;

    private GameObject mostRecentlySelectedObject;

    private Player[] rewiredPlayers = new Player[GameSettings.MAX_PLAYERS];
    private Player systemPlayer;

    private void ResetPlayers() {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            GameSettings.PlayerTypes[i] = GameSettings.PLAYER_TYPES.AI;
        }
    }

    public void Start() {
        systemPlayer = ReInput.players.GetSystemPlayer();
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            rewiredPlayers[i] = ReInput.players.GetPlayer(i);
        }
        ResetPlayers();
    }

    public void Update() {
        if (systemPlayer.GetButtonDown(GameSettings.REWIRED_SECONDARY_BTN)) {
            Back();
        } else {
            for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
                if (rewiredPlayers[i].GetButtonDown(GameSettings.REWIRED_SECONDARY_BTN)) {
                    Back();
                    break;
                }
            }
        }
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(mostRecentlySelectedObject);
        } else {
            if (mostRecentlySelectedObject != EventSystem.current.currentSelectedGameObject) {
                mostRecentlySelectedObject = EventSystem.current.currentSelectedGameObject;
            }
        }
    }

    public void ConfigOpened() {
        currentState = MENU_STATE.CONTROL_MAP;
    }

    /**
    * Used to return focus to the controller option after closing the Rewired Control Mapper
    */
    public void ConfigClosed() {
        // Refocus on options
        EventSystem.current.SetSelectedGameObject(controllerConfigButton.gameObject);
        currentState = MENU_STATE.MAIN_MENU;
    }

    private void startPressed() {
        playButton.OnSelect(null);
        playButton.Select();
        currentState = MENU_STATE.MAIN_MENU;
        startButton.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void backToStart() {
        currentState = MENU_STATE.PRESS_START;
        mainMenu.SetActive(false);
        startButton.gameObject.SetActive(true);
        startButton.Select();
    }

    /**
     * Linked to from each individual button. May want to
     * copy the back button functionality (but this may reduce double presses)
     * Also called from TeamSelectController when teams have been selected and start has been pressed.
     */
    public void Forward() {
        switch (currentState) {
            case MENU_STATE.PRESS_START:
                startPressed();
                break;
            case MENU_STATE.MAIN_MENU:
                teamCanvas.gameObject.SetActive(true);
                teamCanvas.GetComponent<TeamSelectController>().Reset();
                teamCanvas.GetComponent<TeamSelectController>().SizeControllers();
                splashCanvas.gameObject.SetActive(false);
                splashBackgroundCanvas.gameObject.SetActive(false);
                currentState = MENU_STATE.TEAM_SELECT;
                break;
            case MENU_STATE.TEAM_SELECT:
                // Start the game!
                SceneManager.LoadScene(GameSettings.GAME_SCENE);
                break;
        }
    }

    private void Back() {
        switch (currentState) {
            case MENU_STATE.MAIN_MENU:
                backToStart();
                break;
            case MENU_STATE.TEAM_SELECT:
                playButton.OnSelect(null);
                playButton.Select();
                teamCanvas.gameObject.SetActive(false);
                splashCanvas.gameObject.SetActive(true);
                splashBackgroundCanvas.gameObject.SetActive(true);
                currentState = MENU_STATE.MAIN_MENU;
                ResetPlayers();
                break;
        }
    }

    public void Exit() {
        Application.Quit();
    }

    /**
    * Team State controller needs to know about this.
    */
    public bool isTeamState() {
        return currentState == MENU_STATE.TEAM_SELECT;
    }
}
