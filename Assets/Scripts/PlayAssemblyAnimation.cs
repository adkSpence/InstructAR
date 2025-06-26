using UnityEngine;

public class PlayAssemblyAnimation : MonoBehaviour
{
    public Animator tableAnimator;

    public void TriggerAssembly()
    {
        tableAnimator.SetTrigger("PlayAssembly");
    }
}