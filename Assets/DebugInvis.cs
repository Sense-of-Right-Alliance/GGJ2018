using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component hides its gameobject on runtime, so we can have visual triggers in the editor that are hidden in game.
 */
public class DebugInvis : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
		GetComponent<SpriteRenderer>().color = tmp;
	}
}
