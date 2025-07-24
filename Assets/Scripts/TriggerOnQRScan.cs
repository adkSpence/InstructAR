using System;
using UnityEngine;
using Vuforia;

public class TriggerOnQRScan : MonoBehaviour
{
    public GameObject chairRoot;

    private ObserverBehaviour observerBehaviour;
    private bool hasBeenDetected = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        if (chairRoot != null)
            chairRoot.SetActive(false); // Start hidden
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            hasBeenDetected = true;
            Console.WriteLine(hasBeenDetected);
            if (hasBeenDetected&&chairRoot != null)
            {
                chairRoot.SetActive(true);
                Console.WriteLine("Image here");
            }
               
        }
    }
}
