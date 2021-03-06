﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 1.0f;
    [SerializeField]
    Animator spriteAnimator;
    [SerializeField]
    GameSettings.FAN_TYPE fanType;
    [SerializeField]
    Sprite promoterSprite;

    Tastes tastes;

    bool delaying = false;
    float delayTimer = 0.0f;

    bool moving = false;
    float moveT = 0.0f;
    Vector3 startPos;
    Vector3 endPos;
    float lerpSpeed = 0.0f;

    bool isPromoter = false;
    public bool IsPromoter { get { return isPromoter; } }
    public GameSettings.FAN_TYPE FanType { get { return fanType; } }

    bool atStage = false;
    public void SetToStage()
    {
        atStage = true;
    }

    // Use this for initialization
    void Start () {
	    tastes = new Tastes(fanType);

        RoundController.OnRoundChange += HandleRoundChange;
    }

    public GameSettings.STAGE PickStage(Dictionary<GameSettings.STAGE, StageManager> stages) { return tastes.PickStage(stages); }

    public void SetToPromoter()
    {
        isPromoter = true;

        GetComponentInChildren<SpriteRenderer>().sprite = promoterSprite;
    }

    void HandleRoundChange()
    {
        if (!atStage && isPromoter)
        {
            DoHop();
        }
    }

    void DoHop()
    {
        spriteAnimator.SetTrigger("Jump");
    }

    public void MoveTo(Vector3 pos, float delay)
    {
        startPos = transform.position;
        endPos = pos;

        float distance = Vector2.Distance(endPos, startPos);
        lerpSpeed = moveSpeed / distance;

        if (delay > 0)
        {
            delayTimer = delay;
            delaying = true;
        }
        else
        {
            StartMove();
        }
    }

    void StartMove()
    {
        moving = true;
        moveT = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        if (delaying)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0.0f)
            {
                delaying = false;
                StartMove();
            }
        }
		else if (moving) {
            UpdateMoving();
        }

        spriteAnimator.SetFloat("Speed", moving ? moveSpeed * 100.0f : 0.0f);
    }

    void UpdateMoving() {
        moveT += Time.deltaTime * lerpSpeed;
        if (moveT >= 1.0f)
        {
            moveT = 1.0f;
            moving = false;
        }

        Vector3 newPos = Vector3.Lerp(startPos, endPos, moveT);
        newPos.z = newPos.y; // sort that zzzzzzz

        transform.position = newPos;
    }
}
