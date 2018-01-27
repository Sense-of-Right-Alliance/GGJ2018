using UnityEngine;
using UnityEngine.UI;

/**
 * This is the component that gets applied to the textField we'd like to insert text into.
 */
public class LocTextGrabber : MonoBehaviour {

    [SerializeField]
    private Localization.LOC_KEY locKey;

	public void Start () {
        GetComponent<Text>().text = Localization.GetLocString(locKey);
    }
}
