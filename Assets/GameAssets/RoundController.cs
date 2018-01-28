using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour {
    public delegate void RoundAction();
    public static event RoundAction OnRoundChange;

    [SerializeField]
    private float roundTime = 5.0f;
    [SerializeField]
    private float startDelay = 2.0f;
    [SerializeField]
    private GameObject timerText;

    private float roundTimer = 0.0f;

    bool animatingIn = false;
    float timerT = 0.0f;

    float timerAnimateSpeed = 2.0f;

    float startDelayTimer = 0.0f;
    bool starting = false;

    // Use this for initialization
    void Start () {
        startDelayTimer = startDelay;
        starting = true;

        SetTimerText(roundTime);
    }

    void BeginRoundTimer()
    {
        roundTimer = roundTime; // NOTE: Round change is not called at the very start of the game
        animatingIn = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (starting)
        {
            startDelayTimer -= Time.deltaTime;
            if (startDelayTimer <= 0.0f)
            {
                starting = false;
                BeginRoundTimer();
            }
        } else
        {
            UpdateRoundTimer();

            if (animatingIn)
            {
                UpdateTimerAnimation();
            }
        }
        

       
    }

    void UpdateRoundTimer()
    {
        roundTimer -= Time.deltaTime;

        if (roundTimer <= 0.0f)
        {
            ChangeRound();
        }

        SetTimerText(roundTimer);
    }

    void SetTimerText(float time)
    {
        timerText.GetComponent<Text>().text = string.Format("{0:#0}.{1:0}",
                     Mathf.Floor(time) % 60,//seconds
                     Mathf.Floor((time * 10) % 10));//miliseconds
    }

    void UpdateTimerAnimation()
    {
        timerT += Time.deltaTime * timerAnimateSpeed;
        if (timerT >= 1.0f)
        {
            animatingIn = false;
            timerT = 1.0f;
        }

        Vector2 newPos = new Vector2(0.5f,0.5f);
        newPos.y = Mathf.Lerp(0.5f, 0.90f, Mathf.Pow(timerT,3.0f));

        Vector3 newScale = new Vector3(1f, 1f, 1f);
        newScale.x = Mathf.Lerp(1f, 0.50f, timerT);
        newScale.y = Mathf.Lerp(1f, 0.50f, timerT);

        timerText.GetComponent<RectTransform>().anchorMax = newPos;
        timerText.GetComponent<RectTransform>().anchorMin = newPos;

        timerText.GetComponent<RectTransform>().localScale = newScale;
    }

    void ChangeRound() {
        if (OnRoundChange != null) {
            OnRoundChange();
        }

        roundTimer = roundTime;
    }
}
