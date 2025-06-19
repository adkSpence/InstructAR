using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    public GameObject[] instructionSteps;
    public Button nextButton;

    private int currentStep = 0;

    void Start()
    {
        ShowStep(0);
        nextButton.onClick.AddListener(NextStep);
    }

    void ShowStep(int step)
    {
        for (int i = 0; i < instructionSteps.Length; i++)
            instructionSteps[i].SetActive(i == step);
    }

    void NextStep()
    {
        currentStep++;
        if (currentStep >= instructionSteps.Length)
            currentStep = 0; // Loop back to start
        ShowStep(currentStep);
    }
}
