using UnityEngine;
using Vuforia;

public class ShowChairOnScan : MonoBehaviour
{
    public GameObject chairAnchor; // This will be your Cube
    private ObserverBehaviour observerBehaviour;
    private bool hasBeenShown = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        if (chairAnchor != null)
        {
            chairAnchor.SetActive(false); // Hide cube at start
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (!hasBeenShown && (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED))
        {
            Debug.Log(" QR Code Detected – Showing Cube");
            if (chairAnchor != null)
                chairAnchor.SetActive(true);

            hasBeenShown = true; // only once
        }
    }
}
