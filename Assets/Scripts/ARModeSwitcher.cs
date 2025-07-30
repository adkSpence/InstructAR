using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Vuforia;
using System.Collections;

public class ARModeSwitcher : MonoBehaviour
{
    [Header("AR Components")]
    public GameObject xrOrigin;            // XR Origin (Mobile AR)
    public GameObject vuforiaCamera;       // ARCamera (Vuforia)
    public ARSession arSession;

    [Header("Debug")]
    public bool enableDebugLogs = true;

    private bool isVuforiaMode = false;

    void Start()
    {
        // Start in ARFoundation mode
        SwitchToARFoundation();
    }

    public void SwitchToVuforia()
    {
        if (isVuforiaMode) return; // Already in Vuforia mode

        StartCoroutine(SwitchToVuforiaCoroutine());
    }

    private IEnumerator SwitchToVuforiaCoroutine()
    {
        if (enableDebugLogs) Debug.Log("Switching to Vuforia...");

        // Step 1: Disable ARFoundation properly
        if (arSession != null)
        {
            arSession.enabled = false;
            if (enableDebugLogs) Debug.Log("ARSession disabled");
        }

        if (xrOrigin != null)
        {
            xrOrigin.SetActive(false);
            if (enableDebugLogs) Debug.Log("XR Origin disabled");
        }

        // Step 2: Wait a frame to ensure ARFoundation is fully disabled
        yield return new WaitForEndOfFrame();

        // Step 3: Check if Vuforia is initialized
        if (VuforiaApplication.Instance != null)
        {
            if (!VuforiaApplication.Instance.IsRunning)
            {
                if (enableDebugLogs) Debug.Log("Initializing Vuforia...");
                VuforiaApplication.Instance.Initialize();

                // Wait for initialization
                float timeout = 5f;
                float timer = 0f;
                while (!VuforiaApplication.Instance.IsRunning && timer < timeout)
                {
                    yield return new WaitForSeconds(0.1f);
                    timer += 0.1f;
                }

                if (!VuforiaApplication.Instance.IsRunning)
                {
                    Debug.LogError("Vuforia failed to initialize within timeout!");
                    yield break;
                }
            }
        }
        else
        {
            Debug.LogError("VuforiaApplication.Instance is null!");
            yield break;
        }

        // Step 4: Enable Vuforia components
        if (vuforiaCamera != null)
        {
            vuforiaCamera.SetActive(true);
            if (enableDebugLogs) Debug.Log("Vuforia camera enabled");
        }

        // Step 5: Enable VuforiaBehaviour
        if (VuforiaBehaviour.Instance != null)
        {
            VuforiaBehaviour.Instance.enabled = true;
            if (enableDebugLogs) Debug.Log("VuforiaBehaviour enabled");
        }
        else
        {
            Debug.LogError("VuforiaBehaviour.Instance is null!");
            yield break;
        }

        // Step 6: Wait another frame to ensure everything is active
        yield return new WaitForEndOfFrame();

        isVuforiaMode = true;
        if (enableDebugLogs) Debug.Log("Successfully switched to Vuforia mode");
    }

    public void SwitchToARFoundation()
    {
        if (!isVuforiaMode) return; // Already in ARFoundation mode

        StartCoroutine(SwitchToARFoundationCoroutine());
    }

    private IEnumerator SwitchToARFoundationCoroutine()
    {
        if (enableDebugLogs) Debug.Log("Switching to AR Foundation...");

        // Step 1: Disable Vuforia properly
        if (VuforiaBehaviour.Instance != null)
        {
            VuforiaBehaviour.Instance.enabled = false;
            if (enableDebugLogs) Debug.Log("VuforiaBehaviour disabled");
        }

        if (vuforiaCamera != null)
        {
            vuforiaCamera.SetActive(false);
            if (enableDebugLogs) Debug.Log("Vuforia camera disabled");
        }

        // Step 2: Wait a frame
        yield return new WaitForEndOfFrame();

        // Step 3: Enable ARFoundation
        if (xrOrigin != null)
        {
            xrOrigin.SetActive(true);
            if (enableDebugLogs) Debug.Log("XR Origin enabled");
        }

        if (arSession != null)
        {
            arSession.enabled = true;
            if (enableDebugLogs) Debug.Log("ARSession enabled");
        }

        // Step 4: Wait for ARFoundation to initialize
        yield return new WaitForEndOfFrame();

        isVuforiaMode = false;
        if (enableDebugLogs) Debug.Log("Successfully switched to ARFoundation mode");
    }

    // Helper method to check current mode
    public bool IsVuforiaMode()
    {
        return isVuforiaMode;
    }

    // Emergency reset if things go wrong
    public void ForceReset()
    {
        StopAllCoroutines();

        // Disable everything
        if (xrOrigin != null) xrOrigin.SetActive(false);
        if (vuforiaCamera != null) vuforiaCamera.SetActive(false);
        if (arSession != null) arSession.enabled = false;
        if (VuforiaBehaviour.Instance != null) VuforiaBehaviour.Instance.enabled = false;

        // Restart in ARFoundation mode
        StartCoroutine(DelayedARFoundationStart());
    }

    private IEnumerator DelayedARFoundationStart()
    {
        yield return new WaitForSeconds(1f);
        SwitchToARFoundation();
    }
}