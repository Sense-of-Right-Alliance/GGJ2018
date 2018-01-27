using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGrid : MonoBehaviour {

    [SerializeField]
    private GameObject linePrefab;
    [SerializeField]
    private int numLines = 50;
    [SerializeField]
    private float gridSize = 100.0f;
    
    private Camera mCamera;

	void Start () {
        // First attempt with a bajillion line renderers:
        // TODO-DG: Probably use GL_Lines (Assuming it works with our Post-process effects)
        // TODO-DG: Otherwise at least get horizontal lines in.
		for (int i = -numLines/2; i < numLines/2; i++) {
            GameObject vertLine = Instantiate(linePrefab);
            vertLine.transform.Translate(Vector3.left * gridSize * i);
            vertLine.transform.parent = gameObject.transform;
            /*horizLine.transform.Translate(Vector3.forward * gridSize * i);
            horizLine.transform.Rotate(new Vector3(0, 90, 0));
            horizLine.transform.Translate(Vector3.left * )
            horizLine.transform.localScale = new Vector3(1,1, 5);
            vertLine.transform.parent = gameObject.transform;*/
        }
	}
}
