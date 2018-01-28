using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private Color[] emitColours = new Color[GameSettings.NUM_INSTRUMENTS];
    [SerializeField]
    Transform crowdCenterTransform;

    private ControlAbstractor mController;
    private ParticleSystem mParticle;

    Vector3 crowdCenterPosition;

    public GameSettings.INSTRUMENT CurrentInstrument { get; private set; }

    void Start () {
        CurrentInstrument = GameSettings.INSTRUMENT.NONE;
        RoundController.OnRoundChangeEarly += HandleRoundChange;
        mParticle = GetComponent<ParticleSystem>();

        crowdCenterPosition = crowdCenterTransform == null ? Vector3.zero : crowdCenterTransform.position;
    }

    void OnDestroy()
    {
        RoundController.OnRoundChangeEarly -= HandleRoundChange;
    }

    void HandleRoundChange() {
        // display colourful effect when changing instruments
        GameSettings.INSTRUMENT newInstrument = mController.GetInstrument();
        if (newInstrument != CurrentInstrument && newInstrument != GameSettings.INSTRUMENT.NONE) {
            var col = mParticle.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new GradientColorKey(emitColours[(int)newInstrument], 0.0f), new GradientColorKey(emitColours[(int)newInstrument], 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.05f, 1.0f) } );
            col.color = grad;
            CurrentInstrument = newInstrument;
        }
    }

    public void UpdateController() {
        mController = GetComponent<ControlAbstractor>();
    }

    public Vector3 GetCrowdPosition()
    {
        Vector3 pos = crowdCenterPosition;

        float range = 0.15f;

        // To Do: Distribute position so the crowd is spread out and fans are not overlapping one another
        pos.x += Random.Range(-range / 2.0f, range / 2.0f);
        pos.y += Random.Range(-range / 2.0f, range / 2.0f);

        return pos;
    }
}
