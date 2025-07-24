using UnityEngine;
using Vuforia;

public class ShowChairAndUIOnScan : MonoBehaviour
{
    public GameObject chairAnchor;     // Chair parent (or chair object)
    public GameObject canvasUI;        // The Canvas with the Next button

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
            chairAnchor.SetActive(false);

        if (canvasUI != null)
            canvasUI.SetActive(false);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (!hasBeenShown && (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED))
        {
            Debug.Log(" QR Code Detected – Showing Chair and UI");

            if (chairAnchor != null)
                chairAnchor.SetActive(true);

            if (canvasUI != null)
                canvasUI.SetActive(true);

            hasBeenShown = true;
        }
    }
}
