using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    public PlayerManager player;

    public PlayerInputManager playerInput;
    //public PlayerCamera playerCamera;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    [Header("Movement Settings")]
    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float sprintingSpeed;
    [SerializeField] private float rotationSpeed;
    public bool isSprinting;

    [Header("Dodge")]
    private Vector3 rollDirection;


    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        playerInput = GetComponent<PlayerInputManager>();
        //playerCamera = FindAnyObjectByType<PlayerCamera>();
    }

    private void Update()
    {
        HandleAllMovement();
    }


    private void GetVerticalAndHorizontalInputs()
    {
        verticalMovement = playerInput.verticalInput;
        horizontalMovement = playerInput.horizontalInput;
        moveAmount = playerInput.moveAmount;

    }
    public void HandleAllMovement()
    {
        //if(player.isPerformingAction)
        //{
        //    return;
        //}

        HandleGroundedMovement();
        HandleRotation();
    }

    private void HandleGroundedMovement()
    {
        if(!player.canMove)
        {
            return;
        }
        GetVerticalAndHorizontalInputs();

        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(isSprinting)
        {
            player.characterController.Move(moveDirection* sprintingSpeed * Time.deltaTime);
        }
        else
        {
            if (playerInput.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (playerInput.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }
    }
    private void HandleRotation()
    {
        if(!player.canRotate)
        {
            return;
        }

        targetRotationDirection = Vector3.zero;

        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;


        if (targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }

    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            isSprinting = false;
        }

        if (moveAmount >= 0.5)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    public void AttemptToPerformDodge()
    {
        if(player.isPerformingAction) 
        {
            return;
        }

        if(playerInput.moveAmount > 0)
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * playerInput.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * playerInput.horizontalInput;

            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            player.playerAnimationManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);

        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation("Back_Step_01", true, true);
        }
        
    }
}
