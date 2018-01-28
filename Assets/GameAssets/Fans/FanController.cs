using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {
    [SerializeField]
    GameObject fanPrefab;
    [SerializeField]
    Transform spawnTransform;
    [SerializeField]
    Transform crowdCenterTransform;

    Vector3 spawnPosition;
    Vector3 crowdCenterPosition;

    List<GameObject> fans = new List<GameObject>();

    Dictionary<int, GameObject> playerFans = new Dictionary<int, GameObject>();

	// Use this for initialization
	void Start () {
        spawnPosition = (spawnTransform == null) ? Vector3.zero : spawnTransform.position;
        crowdCenterPosition = (crowdCenterTransform == null) ? Vector3.zero : crowdCenterTransform.position;
    }
	
	// Update is called once per frame
	void Update () {
        // THIS IS ONLY TO TEST. DON'T WORRY
		if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnNewCrowd(10);
        }
	}

    void SpawnNewCrowd(int crowdSize)
    {
        for (int i = 0; i < crowdSize; i++)
        {
            //if (i > fans.Count)
            //{
                GameObject fan = CreateNewFan();

                fan.transform.position = spawnPosition;

                fan.GetComponent<Fan>().MoveTo(GetCrowdPosition(), i*0.06f);
            //}
        }
    }

    GameObject CreateNewFan()
    {
        GameObject newFan = (GameObject)Instantiate(fanPrefab, Vector3.zero, Quaternion.identity);

        fans.Add(newFan);

        return newFan;
    }

    Vector3 GetCrowdPosition()
    {
        Vector3 pos = crowdCenterPosition;

        float range = 0.15f;

        // To Do: Distribute position so the crowd is spread out and fans are not overlapping one another
        pos.x += Random.Range(-range / 2.0f, range / 2.0f);
        pos.y += Random.Range(-range / 2.0f, range / 2.0f);

        return pos;
    }
}
