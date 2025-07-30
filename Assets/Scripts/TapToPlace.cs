using UnityEngine;
using Vuforia;

public class TapToPlace : MonoBehaviour
{
    public GameObject planeFinder;

    void Start()
    {
        var cpb = planeFinder.GetComponent<ContentPositioningBehaviour>();
        cpb.OnContentPlaced.AddListener(obj => OnPlaced(cpb.AnchorStage, obj));
    }

    void OnPlaced(AnchorBehaviour anchor, GameObject placedObject)
    {
        // Example: Disable plane finder after placement
        planeFinder.SetActive(false);
    }
}