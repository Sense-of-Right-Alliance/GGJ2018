using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour {
	[SerializeField]
    private const float VIS_TIME = 1.0f;
    [SerializeField]
    private const float INVIS_TIME = 0.4f;

    private CanvasRenderer mRenderer;

    private float timeSinceLastBlink = 0.0f;
    private bool isVisible = true;

    void Start () {
        mRenderer = GetComponent<CanvasRenderer>();
    }

	void Update () {
        timeSinceLastBlink += Time.unscaledDeltaTime;   // We'll still blink when paused or slowed down.
        if ((isVisible && timeSinceLastBlink > VIS_TIME) || (!isVisible && timeSinceLastBlink > INVIS_TIME)) {
            timeSinceLastBlink = 0.0f;
            isVisible = !isVisible;
            mRenderer.SetAlpha(isVisible ? 1.0f : 0.0f);
        }
    }
}
