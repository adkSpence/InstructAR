using UnityEngine;

public class TableManipulator : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    private float initialRotationAngle;
    private Quaternion initialRotation;

    private bool isDragging = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // --- Pinch to Scale ---
            if (touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = transform.localScale;

                initialRotationAngle = Mathf.Atan2(
                    touch1.position.y - touch0.position.y,
                    touch1.position.x - touch0.position.x
                ) * Mathf.Rad2Deg;

                initialRotation = transform.rotation;
            }
            else
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                if (Mathf.Approximately(initialDistance, 0)) return;

                float scaleFactor = currentDistance / initialDistance;
                transform.localScale = initialScale * scaleFactor;

                // --- Two-finger rotate ---
                float currentAngle = Mathf.Atan2(
                    touch1.position.y - touch0.position.y,
                    touch1.position.x - touch0.position.x
                ) * Mathf.Rad2Deg;

                float rotationDelta = currentAngle - initialRotationAngle;
                transform.rotation = initialRotation * Quaternion.Euler(0f, -rotationDelta, 0f);
            }
        }
        else if (touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform == transform || hit.transform.IsChildOf(transform))
                        {
                            isDragging = true;
                            Debug.Log("✋ Drag start");
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging && Physics.Raycast(ray, out hit))
                    {
                        Vector3 newPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        transform.position = newPos;
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        Debug.Log("🏁 Drag end");
                    }
                    break;
            }
        }
    }
}