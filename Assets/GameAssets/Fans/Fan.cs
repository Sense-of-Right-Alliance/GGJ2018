using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 1.0f;

    bool moving = false;
    float moveT = 0.0f;
    Vector3 startPos;
    Vector3 endPos;

	// Use this for initialization
	void Start () {
		
	}

    public void MoveTo(Vector3 pos)
    {
        moving = true;
        moveT = 0.0f;

        startPos = transform.position;
        endPos = pos;
    }

    // Update is called once per frame
    void Update () {
		if (moving) {
            UpdateMoving();
        }
	}

    void UpdateMoving() {
        moveT += Time.deltaTime * moveSpeed;
        if (moveT >= 1.0f)
        {
            moveT = 1.0f;
        }

        Vector3 newPos = Vector3.Lerp(startPos, endPos, moveT);

        transform.position = newPos;
    }
}
