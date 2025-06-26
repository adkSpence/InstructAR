using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // For UI tap checks

public class TableSelector : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    public GameObject animationButton;

    private Dictionary<Renderer, Color> originalColors = new();
    private bool isSelected = false;

    private float tapThreshold = 0.2f; // Max duration for a tap
    private float maxMovement = 10f;   // Max movement (in pixels) for a tap

    private float touchStartTime;
    private Vector2 touchStartPos;

    void Start()
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            if (!originalColors.ContainsKey(rend))
                originalColors.Add(rend, rend.material.color);
        }

        if (animationButton != null)
            animationButton.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartTime = Time.time;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    float touchDuration = Time.time - touchStartTime;
                    float movement = Vector2.Distance(touch.position, touchStartPos);

                    if (touchDuration <= tapThreshold && movement <= maxMovement)
                    {
                        // 🛑 Check if tap was on a UI element (e.g., button)
                        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                            return;

                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.transform.IsChildOf(transform))
                            {
                                Debug.Log("🔆 Table tapped!");
                                SelectTable();
                            }
                            else
                            {
                                DeselectTable();
                            }
                        }
                        else
                        {
                            DeselectTable();
                        }
                    }
                    break;
            }
        }
    }

    void SelectTable()
    {
        if (isSelected) return;

        foreach (var pair in originalColors)
        {
            pair.Key.material.color = highlightColor;
        }

        if (animationButton != null)
            animationButton.SetActive(true);

        isSelected = true;
    }

    void DeselectTable()
    {
        if (!isSelected) return;

        foreach (var pair in originalColors)
        {
            pair.Key.material.color = pair.Value;
        }

        if (animationButton != null)
            animationButton.SetActive(false);

        Debug.Log("⚪ Table deselected.");
        isSelected = false;
    }
}