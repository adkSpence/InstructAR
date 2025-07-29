using System.Collections;
using UnityEngine;
using Vuforia;

public class PlayAudioOnTrack : MonoBehaviour
{
    public GameObject highlightPrefab;  // Assign in Inspector

    private AudioSource audioSource;
    private ObserverBehaviour observerBehaviour;
    private bool hasPlayed = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        audioSource = GetComponent<AudioSource>();

        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnStatusChanged;
        }
    }

    private void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnStatusChanged;
        }
    }

    private void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (!hasPlayed && status.Status == Status.TRACKED)
        {
            if (audioSource && !audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("🔊 Audio started playing");
            }

            SpawnHighlightAtAnchor();
            hasPlayed = true;
        }
    }

    private void SpawnHighlightAtAnchor()
    {
        string anchorName = observerBehaviour.TargetName switch
        {
            "lega" => "LegA_anchor",
            "legb" => "LegB_anchor",
            "legc" => "LegC_anchor",
            "legd" => "LegD_anchor",
            _ => "LegA_anchor"
        };

        GameObject anchor = GameObject.Find(anchorName);

        if (anchor != null)
        {
            Vector3 position = anchor.transform.position;
            Quaternion rotation = anchor.transform.rotation;

            GameObject highlight = Instantiate(highlightPrefab, position, rotation);
            highlight.transform.localScale = new Vector3(0.05f, 0.03f, 0.05f); // Adjust scale here if needed

            Debug.Log($"✨ Highlight marker instantiated at anchor: {anchorName}");
            Debug.Log($"Anchor '{anchorName}' position: {position}, world pos: {highlight.transform.position}");
        }
        else
        {
            Debug.LogWarning($"❌ Anchor '{anchorName}' not found in the scene.");
        }
    }
}