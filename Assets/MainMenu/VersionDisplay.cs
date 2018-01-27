using UnityEngine;
using UnityEngine.UI;

public class VersionDisplay : MonoBehaviour {
    [SerializeField]
    private Tools mTools;

    private Text mText;

	void Start () {
		mText = GetComponent<Text>();
        mText.text = "v" + Application.version;
	}

    void Update() {
        // TODO: Use Events for this instead of Update: https://trello.com/c/Bhbzo1h4
        // TODO: Re-enable this when we want to hide the version:
        /*if (mTools.DebugState > GameSettings.DEBUG_STATE.RELEASE) {
            mText.enabled = true;
        } else {
            mText.enabled = false;
        }*/
    }
}
