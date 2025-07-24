using UnityEngine;

public class PlayAssemblyAnimation : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public GameObject part;        // Part to activate (LegA, etc.)
        public string animationName;   // Animation clip name on table-model Animator
    }

    public Step[] steps;
    private int currentStep = 0;

    [Header("Animation Setup")]
    public Animator tableModelAnimator;  // Central Animator on table-model

    [Header("UI Controls")]
    public GameObject animationControlsPanel;
    public GameObject animateButton;

    // Called by Animate button
    public void ShowControls()
    {
        if (animationControlsPanel != null)
            animationControlsPanel.SetActive(true);

        if (animateButton != null)
            animateButton.SetActive(false);

        HideTableParts();
        currentStep = 0;
    }

    // Called by Next button
    public void TriggerAssembly()
    {
        Debug.Log($"▶ Triggering step {currentStep}");

        if (currentStep < steps.Length)
        {
            steps[currentStep].part.SetActive(true);
            Debug.Log($"▶ Activated: {steps[currentStep].part.name}");

            tableModelAnimator.Play(steps[currentStep].animationName);
            Debug.Log($"▶ Played animation: {steps[currentStep].animationName}");

            currentStep++;

            // Hide next button if that was the last step
            if (currentStep == steps.Length && animationControlsPanel != null)
            {
                animationControlsPanel.SetActive(false); // Hides the button group
            }
        }
    }

    // Called by Reset button
    public void ResetAssembly()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i].part.SetActive(false);
        }
        currentStep = 0;

        if (animateButton != null)
            animateButton.SetActive(true);

        if (animationControlsPanel != null)
            animationControlsPanel.SetActive(false);
    }

    private void HideTableParts()
    {
        foreach (var step in steps)
        {
            step.part.SetActive(false);
        }
    }
}