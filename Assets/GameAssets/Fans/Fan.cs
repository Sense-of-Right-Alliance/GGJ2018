using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 1.0f;

    bool delaying = false;
    float delayTimer = 0.0f;

    bool moving = false;
    float moveT = 0.0f;
    Vector3 startPos;
    Vector3 endPos;
    float lerpSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		
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
