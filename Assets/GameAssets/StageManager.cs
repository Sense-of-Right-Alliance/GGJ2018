using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private Color[] emitColours = new Color[GameSettings.NUM_INSTRUMENTS];

    private ControlAbstractor mController;
    private ParticleSystem mParticle;

    public GameSettings.INSTRUMENT CurrentInstrument { get; private set; }

    void Start () {
        CurrentInstrument = GameSettings.INSTRUMENT.NONE;
        RoundController.OnRoundChange += HandleRoundChange;
        mParticle = GetComponent<ParticleSystem>();
    }

    void OnDestroy()
    {
        RoundController.OnRoundChange -= HandleRoundChange;
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
}
