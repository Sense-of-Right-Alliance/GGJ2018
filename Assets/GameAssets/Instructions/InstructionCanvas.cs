using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class InstructionCanvas : MonoBehaviour {

    [SerializeField]
    private GameObject instructionCanvas;
    [SerializeField]
    private GameObject pauseController;
    [SerializeField]
    private GameObject gameUI;

    private Player[] rPlayers = new Player[GameSettings.MAX_PLAYERS];

    private bool paused = true;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++)
        {
            rPlayers[i] = ReInput.players.GetPlayer(i);
        }

        Pause();

        //pauseController.SetActive(false);
        //gameUI.SetActive(false);
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0.0f;
        instructionCanvas.SetActive(true);
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1.0f;
        instructionCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++)
        {
            if (rPlayers[i].GetButtonDown(GameSettings.REWIRED_START))
            {
                if (paused)
                {
                    Resume();
                    SceneManager.LoadScene(GameSettings.GAME_SCENE);

                    //pauseController.SetActive(true);
                    //gameUI.SetActive(true);
                }
            }
        }
    }
}
