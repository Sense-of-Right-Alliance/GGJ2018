using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private Color[] emitColours = new Color[GameSettings.NUM_ACTIONS];

    private ControlAbstractor mController;
    private ParticleSystem mParticle;

    private GameSettings.INSTRUMENT_ACTIONS currentAction;

    void Start () {
        currentAction = GameSettings.INSTRUMENT_ACTIONS.NONE;
        RoundController.OnRoundChange += HandleRoundChange;
        mParticle = GetComponent<ParticleSystem>();
    }

    void OnDestroy()
    {
        RoundController.OnRoundChange -= HandleRoundChange;
    }

    void HandleRoundChange() {
        // Use our thing to thing
        GameSettings.INSTRUMENT_ACTIONS newAction = mController.GetAction();
        if (newAction != currentAction && newAction != GameSettings.INSTRUMENT_ACTIONS.NONE) {
            var col = mParticle.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new GradientColorKey(emitColours[(int)newAction], 0.0f), new GradientColorKey(emitColours[(int)newAction], 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.05f, 1.0f) } );
            col.color = grad;
            currentAction = newAction;
        }
    }

    public void UpdateController() {
        mController = GetComponent<ControlAbstractor>();
    }

}
