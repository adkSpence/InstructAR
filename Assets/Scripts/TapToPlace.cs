using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TapToPlace : MonoBehaviour
{
    public GameObject objectToPlace;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool hasPlaced = false;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (hasPlaced) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Debug.Log(" ✅ Plane Hit!");
                Pose hitPose = hits[0].pose;
                Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                hasPlaced = true;
            }
            else
            {
                Debug.Log(" ❌ No Plane Hit");
            }
        }
    }
}