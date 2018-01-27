using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Rewired;

public class PauseController : MonoBehaviour {

    [SerializeField]
    private GameObject pauseCanvas;

    private Player[] rPlayers = new Player[GameSettings.MAX_PLAYERS];
    private bool paused = false;

    public void Resume() {
        paused = false;
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
    }

    public void Restart() {
        Resume();
        SceneManager.LoadScene(GameSettings.GAME_SCENE);
    }

    public void Quit() {
        Application.Quit();
    }

    void Start() {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            rPlayers[i] = ReInput.players.GetPlayer(i);
        }
    }

    void Update() {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++) {
            if (rPlayers[i].GetButtonDown(GameSettings.REWIRED_START)) {
                if (paused) {
                    Resume();
                } else {
                    paused = true;
                    Time.timeScale = 0.0f;
                    pauseCanvas.SetActive(true);
                }
            }
        }
    }
}
