using UnityEngine;
using Vuforia;

public class QRMultiTargetManager : MonoBehaviour
{
    public GameObject chairPrefab;
    public GameObject tablePrefab;
    public GameObject canvasUI; 

    private ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // Optional: hide everything at start
        chairPrefab?.SetActive(false);
        tablePrefab?.SetActive(false);
        canvasUI?.SetActive(false);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            string targetName = behaviour.TargetName;

            Debug.Log("Detected target: " + targetName);

            switch (targetName)
            {
                case "chairQR":
                    chairPrefab?.SetActive(true);
                    canvasUI?.SetActive(true);  // Only show UI for chair
                    break;

                case "tableQR":
                    tablePrefab?.SetActive(true);
                    canvasUI?.SetActive(false); // Hide UI if it was visible before
                    break;

                default:
                    Debug.LogWarning("No prefab assigned for target: " + targetName);
                    canvasUI?.SetActive(false);
                    break;
            }
        }
    }
}
