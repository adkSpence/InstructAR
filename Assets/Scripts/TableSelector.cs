using System.Collections.Generic;
using UnityEngine;

public class TableSelector : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Dictionary<Renderer, Color> originalColors = new();
    private bool isSelected = false;

    void Start()
    {
        // Store all renderers and their original colors
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            if (!originalColors.ContainsKey(rend))
                originalColors.Add(rend, rend.material.color);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.IsChildOf(transform))
                {
                    Debug.Log("🔆 Table selected!");
                    Highlight(true);
                    isSelected = true;
                }
                else
                {
                    Deselect();
                }
            }
            else
            {
                Deselect();
            }
        }
    }

    void Highlight(bool enable)
    {
        foreach (var pair in originalColors)
        {
            pair.Key.material.color = enable ? highlightColor : pair.Value;
        }
    }

    void Deselect()
    {
        if (isSelected)
        {
            Debug.Log("⚪ Table deselected!");
            Highlight(false);
            isSelected = false;
        }
    }
}