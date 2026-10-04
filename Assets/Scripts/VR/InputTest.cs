using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public InputActionProperty testActionValue;
    public InputActionProperty testActionButton;

    void Update()
    {
        float value = testActionValue.action.ReadValue<float>();
        //Debug.Log("Value: " + value);

        bool button = testActionValue.action.IsPressed();
        //Debug.Log("Button: " + button);
    }
}
