using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    private PlayerInputActions playerInputActions;

    public PlayerManager player;

    [Header("Player Camera Input")]
    [SerializeField] private Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("Player Movement Input")]
    [SerializeField] private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("Player Action Input")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;


    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void OnEnable()
    {
        if(playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            playerInputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerCamera.Mouse.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerActions.Dodge.performed += i => dodgeInput = true;

            // Holding the Input, sets the bool to true
            playerInputActions.PlayerActions.Sprint.performed += i => sprintInput = true;
            // Releasing the Input, sets the bool to false
            playerInputActions.PlayerActions.Sprint.canceled += i => sprintInput = false;

        }

        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }


    public void HandleAllInputs()
    {
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
        HandleSprinting();
    }

    // Movement
    private void HandlePlayerMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if(moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }

        if(player == null)
        {
            return;
        }

        player.playerAnimationManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerLocomotion.isSprinting);
    }

    private void HandleCameraMovementInput() 
    {
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;
    }


    // Action
    private void HandleDodgeInput()
    {
        if(dodgeInput)
        {
            dodgeInput = false;

            player.playerLocomotion.AttemptToPerformDodge();
        }
    }

    private void HandleSprinting()
    {
        if(sprintInput)
        {
            player.playerLocomotion.HandleSprinting();
        }
        else
        {
            player.playerLocomotion.isSprinting = false;
        }
    }
}
