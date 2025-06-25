using UnityEngine;
using UnityEngine.UI;

public class StepAnimationController : MonoBehaviour
{
    public Animator chairAnimator;
    public Button nextButton;

    private int currentStep = 1;
    private int totalSteps = 3;

    void Start()
    {
        nextButton.onClick.AddListener(PlayNextStep);
    }

    void PlayNextStep()
    {
        if (currentStep <= totalSteps)
        {
            chairAnimator.Play("Step" + currentStep);
            currentStep++;
        }

        if (currentStep > totalSteps)
        {
            nextButton.interactable = false;
        }
    }
}
