using UnityEngine;
using Vuforia;

public class QRDetectionTest : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    private bool hasLogged = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (!hasLogged && (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED))
        {
            Debug.Log("QR Code Detected!");
            hasLogged = true;
        }
    }
}
