using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Rewired;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    private GameObject scoreCanvas;

    private Player[] rPlayers = new Player[GameSettings.MAX_PLAYERS];

    public void Resume()
    {
        Time.timeScale = 1.0f;
        scoreCanvas.SetActive(false);
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(GameSettings.GAME_SCENE);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        for (int i = 0; i < GameSettings.MAX_PLAYERS; i++)
        {
            rPlayers[i] = ReInput.players.GetPlayer(i);
        }
    }

    public void ShowScores(Dictionary<int, int> playerScores)
    {
        scoreCanvas.SetActive(true);
        scoreCanvas.GetComponent<ScoreCanvas>().HideAllPlayers();

        foreach (var item in playerScores)
        {
            scoreCanvas.GetComponent<ScoreCanvas>().ShowPlayer(item.Key);
            scoreCanvas.GetComponent<ScoreCanvas>().SetPlayerScore(item.Key, item.Value);
        }
    }
}
