using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputMovement : MonoBehaviour
{
    [Header("Input Settings (Pls CHOOSE 1 ONLY)")]
    [SerializeField] private bool InputSystemEnabled = true;

    [Header("Player Settings")]
    public CharacterController playerCharacterController;
    [Header("Movement Settings")]
    //Input System references
    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction crouchAction;

    void Start()
    {
        GetComponentsStart();
        InputSystemStart();
        CursorStart();
    }

    void GetComponentsStart()
    {
        playerCharacterController = GetComponent<CharacterController>();
    }

    void InputSystemStart()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        crouchAction = playerInput.actions["Crouch"];
    }

    void CursorStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {

    }
}
