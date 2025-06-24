using UnityEngine;
using Vuforia;

public class ShowChairOnScan : MonoBehaviour
{
  
    public GameObject chairAnchor;
    private ObserverBehaviour observerBehaviour;
    private bool hasBeenShown = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        chairAnchor.SetActive(false);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (!hasBeenShown && (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED))
        {
            chairAnchor.SetActive(true);
            hasBeenShown = true;
        }
    }
}
