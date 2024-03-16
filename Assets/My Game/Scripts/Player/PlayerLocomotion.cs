using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager player;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    public Collider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;

    [Header("Ground Check & Jumping")]
    [SerializeField] public float gravityForce = -5.55f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSphereRadius = 1;
    [SerializeField] public Vector3 yVelocity;
    [SerializeField] private float groundedYVelocity = -20;
    [SerializeField] private float fallStartYVelocity = -5;
    private bool fallingVelocityHasBeenSet = false;
    private float inAirTime = 0;

    [Header("Movement Settings")]
    public Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    public bool isSprinting;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float sprintingSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float sprintingStaminaCost = 1;

    [Header("Dodge")]
    private Vector3 rollDirection;
    [SerializeField] private float dodgeStaminaCost = 15;
    [SerializeField] private float backStepStaminaCost = 12;

    [Header("Jump")]
    [SerializeField] private float jumpStaminaCost = 15;
    [SerializeField] public float jumpHeight;
    [SerializeField] private float jumpForwardSpeed;
    [SerializeField] private float freeFallSpeed = 2;
    private Vector3 jumpDirection;


    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
    }

    private void Update()
    {
        HandleAllMovement();

        if(player.isGrounded)
        {
            if(yVelocity.y < 0)
            {
                inAirTime = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            if(!player.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTime += Time.deltaTime;
            player.animator.SetFloat("InAirTime", inAirTime);

            yVelocity.y += gravityForce * Time.deltaTime;
        }

        player.characterController.Move(yVelocity * Time.deltaTime);
    }


    private void GetVerticalAndHorizontalInputs()
    {
        verticalMovement = player.playerInput.verticalInput;
        horizontalMovement = player.playerInput.horizontalInput;
        moveAmount = player.playerInput.moveAmount;

    }
    public void HandleAllMovement()
    {
        if (player.isDead)
            return;

        HandleGroundedMovement();
        HandleRotation();
        HandleGroundCheck();
        HandleJumpingMovement();
        HandleFreeFallMovement();
    }

    private void HandleGroundedMovement()
    {
        if (player.canMove || player.canRotate)
        {
            GetVerticalAndHorizontalInputs();
        }

        if(!player.canMove)
        {
            return;
        }


        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(isSprinting)
        {
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
        }
        else
        {
            if (player.playerInput.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (player.playerInput.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleJumpingMovement()
    {
        if (player.isJumping)
        {
            player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
        }
    }

    private void HandleFreeFallMovement()
    {
        if (!player.isGrounded)
        {
            Vector3 freeFallDirection;

            freeFallDirection = PlayerCamera.instance.transform.forward * player.playerInput.verticalInput;
            freeFallDirection = freeFallDirection + PlayerCamera.instance.transform.right * player.playerInput.horizontalInput;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        if (player.canRotate)
        {
            if (player.playerInput.isLockedOn)
            {
                if (isSprinting || player.isRolling || player.isJumping)
                {
                    Vector3 targetDirection = Vector3.zero;
                    targetDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
                    targetDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
                    targetDirection.Normalize();
                    targetDirection.y = 0;

                    if (targetDirection == Vector3.zero)
                    {
                        targetDirection = transform.forward;
                    }

                    Quaternion tr = Quaternion.LookRotation(targetDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                    transform.rotation = targetRotation;
                }
                else
                {
                    Vector3 rotationDirection = moveDirection;
                    if (PlayerCamera.instance.nearestLockOnTarget == null)
                    {
                        return;
                    }
                    rotationDirection = PlayerCamera.instance.currentLockOnTarget.transform.position - transform.position;
                    rotationDirection.Normalize();
                    rotationDirection.y = 0;
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                    transform.rotation = targetRotation;
                }
            }
            else
            {
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
        }
    }

    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            isSprinting = false;
        }

        if(player.playerStatsManager.currentStamina <= 0)
        {
            isSprinting = false;
            return;
        }

        if(player.isGrounded)
        {
            if (moveAmount >= 0.5)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }

            if (isSprinting)
            {
                player.playerStatsManager.TakeStaminaCost(sprintingStaminaCost);
            }
        }
    }

    public void AttemptToPerformDodge()
    {
        if(player.isPerformingAction || !player.isGrounded) 
        {
            return;
        }

        if(player.playerStatsManager.currentStamina <= 0)
        {
            return;
        }    

        if(player.playerInput.moveAmount > 0)
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * player.playerInput.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * player.playerInput.horizontalInput;

            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            player.playerAnimationManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
            player.PlaySFX(player.feedBackManager.rollSFX);
            player.playerStatsManager.TakeStaminaCost(dodgeStaminaCost);
            player.isRolling = true;
        }
        else
        {
            if (!player.isGrounded)
                return;

            player.playerAnimationManager.PlayTargetActionAnimation("Back_Step_01", true, true);
            player.playerStatsManager.TakeStaminaCost(backStepStaminaCost);
        }
    }

    public void AttemptToPerformJump()
    {
        if (player.isPerformingAction)
        {
            return;
        }

        if (player.playerStatsManager.currentStamina <= 0)
        {
            return;
        }

        if (player.isJumping)
        {
            return;
        }

        if (!player.isGrounded)
        {
            return;
        }

        player.playerAnimationManager.PlayTargetActionAnimation("Main_Jump_01", false, true, true);

        player.isJumping = true;
 

        jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * player.playerInput.verticalInput;
        jumpDirection += PlayerCamera.instance.cameraObject.transform.right * player.playerInput.horizontalInput;
        jumpDirection.y = 0;

        if(jumpDirection != Vector3.zero)
        {
            if (isSprinting)
            {
                jumpDirection *= 1.5f;
            }
            else if (player.playerInput.moveAmount > 0.5)
            {
                jumpDirection *= 1f;
            }
            else if (player.playerInput.moveAmount <= 0.5)
            {
                jumpDirection *= 0.5f;
            }

            player.playerStatsManager.TakeStaminaCost(jumpStaminaCost);

        }
    }

    private void HandleGroundCheck()
    {
        player.isGrounded = Physics.CheckSphere(player.transform.position, groundCheckSphereRadius, groundLayer);
    }
}
