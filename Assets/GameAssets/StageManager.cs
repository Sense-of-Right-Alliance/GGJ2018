using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    private const float PARTICLE_ALPHA_START = 1.0f;
    private const float PARTICLE_ALPHA_END = 0.2f;

    [SerializeField]
    Transform crowdCenterTransform;
    [SerializeField]
    GameObject[] kings;

    private ControlAbstractor mController;
    private ParticleSystem mParticle;

    Vector3 crowdCenterPosition;

    public GameSettings.INSTRUMENT CurrentInstrument { get; private set; }

    void Start () {
        CurrentInstrument = GameSettings.INSTRUMENT.NONE;
        RoundController.OnRoundChangeEarly += HandleRoundChange;
        mParticle = GetComponent<ParticleSystem>();

        crowdCenterPosition = crowdCenterTransform == null ? Vector3.zero : crowdCenterTransform.position;

        for (int i = 0; i < kings.Length; i++)
        {
            kings[i].SetActive(false);
        }
    }

    void OnDestroy()
    {
        RoundController.OnRoundChangeEarly -= HandleRoundChange;
    }

    void HandleRoundChange() {
        // display colourful effect when changing instruments
        GameSettings.INSTRUMENT newInstrument = mController.GetInstrument();
        if (newInstrument != CurrentInstrument && newInstrument != GameSettings.INSTRUMENT.NONE) {
            mParticle.startColor = GameSettings.FanColours[(int)newInstrument];
            var col = mParticle.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { 
                new GradientColorKey(GameSettings.FanColours[(int)newInstrument], 0.0f),
                new GradientColorKey(GameSettings.FanColours[(int)newInstrument], 1.0f)},
                new GradientAlphaKey[] { new GradientAlphaKey(PARTICLE_ALPHA_START, 0.0f),
                new GradientAlphaKey(PARTICLE_ALPHA_END, 1.0f) 
            } );
            col.color = grad;
            CurrentInstrument = newInstrument;

            for (int i = 0; i < kings.Length; i++)
            {
                kings[i].SetActive(false);
            }
            kings[(int)newInstrument].SetActive(true);
        }

        
    }

    public void UpdateController() {
        mController = GetComponent<ControlAbstractor>();
    }

    public Vector3 GetCrowdPosition()
    {
        Vector3 pos = crowdCenterPosition;

        float range = 0.8f;

        // To Do: Distribute position so the crowd is spread out and fans are not overlapping one another
        pos.x += Random.Range(-range / 2.0f, range / 2.0f);
        pos.y += Random.Range(-range / 2.0f, range / 2.0f);

        return pos;
    }
}
