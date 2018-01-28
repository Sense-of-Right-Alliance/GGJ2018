using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Dictionary<GameSettings.STAGE, StageManager> _stages = new Dictionary<GameSettings.STAGE, StageManager>();
    public Dictionary<GameSettings.STAGE, StageManager> Stages { get { return _stages; } }

    List<GameObject> fans = new List<GameObject>();
    Dictionary<GameSettings.STAGE, List<GameObject>> stageFans = new Dictionary<GameSettings.STAGE, List<GameObject>>();

    GameObject[] fanFabs;

	// Use this for initialization
	void Start () {
        spawnPosition = (spawnTransform == null) ? Vector3.zero : spawnTransform.position;
        crowdCenterPosition = (crowdCenterTransform == null) ? Vector3.zero : crowdCenterTransform.position;

        fanFabs = new GameObject[4];
	    fanFabs[0] = drumFanPrefab;
        fanFabs[1] = bassFanPrefab;
	    fanFabs[2] = trumpetFanPrefab;
        fanFabs[3] = fluteFanPrefab;

	    foreach (var stageType in Stages.Keys) // initalize stageFans dictionary
        {
            stageFans[stageType] = new List<GameObject>();
	    }

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
        SelectStages();
        SpawnNewCrowd(10);
    }

    void SelectStages()
    {
        int i = 0;
        foreach (var fanObject in fans.ToList())
        {
            var fan = fanObject.GetComponent<Fan>();
            var stageType = fan.PickStage(Stages);
            if (Stages.ContainsKey(stageType))
            {
                var stage = Stages[stageType];
                fan.MoveTo(stage.GetCrowdPosition(), i++ * 0.06f);

                fans.Remove(fanObject);
                stageFans[stageType].Add(fanObject);
            }
        }
    }

    void SpawnNewCrowd(int crowdSize)
    {
        for (int i = 0; i < crowdSize; i++)
        {
            //if (i > fans.Count)
            //{

                GameObject fan = CreateNewFan(fanFabs[UnityEngine.Random.Range(0,fanFabs.Length)]);

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
        pos.x += UnityEngine.Random.Range(-range / 2.0f, range / 2.0f);
        pos.y += UnityEngine.Random.Range(-range / 2.0f, range / 2.0f);

        return pos;
    }
}
