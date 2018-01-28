using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {
    [SerializeField]
    GameObject bassFanPrefab;
    [SerializeField]
    GameObject drumFanPrefab;
    [SerializeField]
    GameObject fluteFanPrefab;
    [SerializeField]
    GameObject trumpetFanPrefab;
    [SerializeField]
    Transform spawnTransform;
    [SerializeField]
    Transform crowdCenterTransform;

    Vector3 spawnPosition;
    Vector3 crowdCenterPosition;

    List<GameObject> fans = new List<GameObject>();

    Dictionary<int, GameObject> playerFans = new Dictionary<int, GameObject>();

    GameObject[] fanFabs;

	// Use this for initialization
	void Start () {
        spawnPosition = (spawnTransform == null) ? Vector3.zero : spawnTransform.position;
        crowdCenterPosition = (crowdCenterTransform == null) ? Vector3.zero : crowdCenterTransform.position;

        fanFabs = new GameObject[4];
        fanFabs[0] = bassFanPrefab;
        fanFabs[1] = drumFanPrefab;
        fanFabs[2] = fluteFanPrefab;
        fanFabs[3] = trumpetFanPrefab;

        RoundController.OnRoundChange += HandleRoundChange;
        RoundController.OnFirstRoundStart += HandleRoundStart;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void HandleRoundStart()
    {
        HandleRoundChange();
    }

    void HandleRoundChange()
    {
        SpawnNewCrowd(10);
    }

    void SpawnNewCrowd(int crowdSize)
    {
        for (int i = 0; i < crowdSize; i++)
        {
            //if (i > fans.Count)
            //{

                GameObject fan = CreateNewFan(fanFabs[Random.Range(0,fanFabs.Length)]);

                fan.transform.position = spawnPosition;

                fan.GetComponent<Fan>().MoveTo(GetCrowdPosition(), i*0.06f);
            //}
        }
    }

    GameObject CreateNewFan(GameObject prefab)
    {
        GameObject newFan = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);

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
