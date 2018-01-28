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

    [SerializeField]
    float crowdSpread = 1.0f;

    Vector3 spawnPosition;
    Vector3 crowdCenterPosition;

    private Dictionary<GameSettings.STAGE, StageManager> _stages = new Dictionary<GameSettings.STAGE, StageManager>();
    public Dictionary<GameSettings.STAGE, StageManager> Stages { get { return _stages; } }

    List<GameObject> fans = new List<GameObject>();
    Dictionary<GameSettings.STAGE, List<GameObject>> _stageFans = new Dictionary<GameSettings.STAGE, List<GameObject>>();
    public Dictionary<GameSettings.STAGE, List<GameObject>> StageFans { get { return _stageFans; } }

    GameObject[] fanFabs;

	// Use this for initialization
	void Start () {
        spawnPosition = (spawnTransform == null) ? new Vector3(0.0f, -3.88f, 0.0f) : spawnTransform.position;
        crowdCenterPosition = (crowdCenterTransform == null) ? new Vector3(0.0f, -0.91f, 0.0f) : crowdCenterTransform.position;

        fanFabs = new GameObject[4];
	    fanFabs[0] = drumFanPrefab;
        fanFabs[1] = bassFanPrefab;
	    fanFabs[2] = trumpetFanPrefab;
        fanFabs[3] = fluteFanPrefab;

	    foreach (var stageType in Stages.Keys) // initalize stageFans dictionary
        {
            _stageFans[stageType] = new List<GameObject>();
	    }

        RoundController.OnRoundChange += HandleRoundChange;
        RoundController.OnFirstRoundStart += HandleRoundStart;
    }

    private void OnDestroy()
    {
        RoundController.OnRoundChange -= HandleRoundChange;
        RoundController.OnFirstRoundStart -= HandleRoundStart;

        fans.Clear();
        _stageFans.Clear();
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
        if (fans.Count > 0) { SelectStages(); }
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
                _stageFans[stageType].Add(fanObject);
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

        // To Do: Distribute position so the crowd is spread out and fans are not overlapping one another
        pos.x += UnityEngine.Random.Range(-crowdSpread / 2.0f, crowdSpread / 2.0f);
        pos.y += UnityEngine.Random.Range(-crowdSpread / 2.0f, crowdSpread / 2.0f);

        return pos;
    }
}
