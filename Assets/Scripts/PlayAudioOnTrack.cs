using UnityEngine;
using Vuforia;

[RequireComponent(typeof(ObserverBehaviour), typeof(AudioSource))]
public class PlayAudioOnTrack : MonoBehaviour
{
    AudioSource _audio;
    ObserverBehaviour _observer;

    void Awake()
    {
        // Grab references
        _audio = GetComponent<AudioSource>();
        _observer = GetComponent<ObserverBehaviour>();
    }

    void OnEnable()
    {
        // Register to Vuforia’s status‐changed event
        _observer.OnTargetStatusChanged += OnStatusChanged;
    }

    void OnDisable()
    {
        _observer.OnTargetStatusChanged -= OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if ((status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED) && !_audio.isPlaying)
        {
            _audio.Play();
        }
        else if (status.Status == Status.NO_POSE && _audio.isPlaying)
        {
            _audio.Stop();
        }
    }
}