using UnityEngine;

public class TableManipulator : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    private float initialRotationAngle;
    private Quaternion initialRotation;

    void Update()
    {
        if (Input.touchCount == 2)
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
    }
}