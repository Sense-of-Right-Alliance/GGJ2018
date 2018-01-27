using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour {

    [SerializeField]
    private GameObject playerOneName;
    [SerializeField]
    private GameObject playerTwoName;
    [SerializeField]
    private GameObject playerThreeName;
    [SerializeField]
    private GameObject playerFourName;

    [SerializeField]
    private GameObject playerOneScore;
    [SerializeField]
    private GameObject playerTwoScore;
    [SerializeField]
    private GameObject playerThreeScore;
    [SerializeField]
    private GameObject playerFourScore;

    private GameObject[] playerNames;
    private GameObject[] playerScores;

    bool fieldsInitialized = false;

    // Use this for initialization
    void Awake () {
        InitFields();
    }

    void InitFields()
    {
        playerNames = new GameObject[4];
        playerNames[0] = playerOneName;
        playerNames[1] = playerTwoName;
        playerNames[2] = playerThreeName;
        playerNames[3] = playerFourName;

        playerScores = new GameObject[4];
        playerScores[0] = playerOneScore;
        playerScores[1] = playerTwoScore;
        playerScores[2] = playerThreeScore;
        playerScores[3] = playerFourScore;

        fieldsInitialized = true;
    }

    public void HidePlayer(int playerId)
    {
        //if (!fieldsInitialized) InitFields();

        playerNames[playerId].SetActive(false);
        playerScores[playerId].SetActive(false);
    }

    public void HideAllPlayers()
    {
        for(int i = 0; i < playerNames.Length; i++)
        {
            HidePlayer(i);
        }
    }

    public void ShowPlayer(int playerId)
    {
        //if (!fieldsInitialized) InitFields();

        playerNames[playerId].SetActive(true);
        playerScores[playerId].SetActive(true);
    }

    public void SetPlayerScore(int playerId, int score)
    {
        //if (!fieldsInitialized) InitFields();

        playerScores[playerId].GetComponent<Text>().text = score.ToString();
    }
}
