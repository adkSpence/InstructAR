using UnityEngine;

public class ChairInteraction : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float zoomSpeed = 0.5f;
    public float moveSpeed = 0.01f;
    public float minScale = 0.1f;
    public float maxScale = 3f;

    private Vector3 lastMousePosition;

    void Update()
    {
        // Rotate with left mouse drag
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotX = delta.y * rotationSpeed * Time.deltaTime;
            float rotY = -delta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotY, Space.World);
            transform.Rotate(Vector3.right, rotX, Space.World);
        }

        // Move with right mouse drag
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(delta.x * moveSpeed, delta.y * moveSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newScale = Mathf.Clamp(transform.localScale.x + scroll * zoomSpeed, minScale, maxScale);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }

        lastMousePosition = Input.mousePosition;
    }
}
