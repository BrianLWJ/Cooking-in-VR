using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty inputValue;
    public InputActionProperty gripValue;

    public Animator handAnimator;
    void Start()
    {
        if (handAnimator == null) handAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        float trigger = inputValue.action.ReadValue<float>();
        float grip = gripValue.action.ReadValue<float>();

        handAnimator.SetFloat("Trigger", trigger);
        handAnimator.SetFloat("Grip", grip);

    }
}
