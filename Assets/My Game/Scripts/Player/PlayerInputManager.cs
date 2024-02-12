using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    private PlayerInputActions playerInputActions;
    private PlayerInventoryManager playerInventoryManager;
    private WeaponSlotManager weaponSlotManager;

    public PlayerManager player;
    

    [Header("Player Camera Input")]
    [SerializeField] private Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("Flags")]
    public bool comboFlag;
    public bool isLockedOn;


    [Header("Player Movement Input")]
    [SerializeField] private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("Player Action Input")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;
    [SerializeField] bool jumpInput = false;
    [SerializeField] bool attackInput = false;
    [SerializeField] bool shiftInput = false;
    public bool switchWeapon = false;
    public bool interactInput = false;
    public bool selectInput;
    public bool lockOnInput = false;
    public bool switchRightTargetInput = false;
    public bool switchLeftTargetInput = false;
    public bool twoHandInput = false;


    private void Awake()
    {
        //playerAttack =  GetComponent<PlayerAttack>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //SceneManager.activeSceneChanged += OnSceneChange;
        player = GetComponent<PlayerManager>();

        //instance.enabled = false;
    }

    //private void OnSceneChange(Scene oldScene, Scene newScene)
    //{
    //    if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
    //    {
    //        instance.enabled = true;
    //    }
    //    else
    //    {
    //        instance.enabled = false;
    //    }
    //}

    private void OnEnable()
    {
        if(playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            playerInputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerCamera.Mouse.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerActions.Dodge.performed += i => dodgeInput = true;
            playerInputActions.PlayerActions.Jump.performed += i => jumpInput = true;
            playerInputActions.PlayerActions.SwitchWeapon.performed += i => switchWeapon = true;
            playerInputActions.PlayerActions.Interact.performed += i => interactInput = true;
            playerInputActions.PlayerActions.Selection.performed += i => selectInput = true;
            playerInputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            //playerInputActions.PlayerActions.ScrollUp.started += HandleQuickSlotsInput;
            playerInputActions.PlayerActions.Attack.started += HandleAttackInputs;
            playerInputActions.PlayerActions.TwoHandAttack.performed += i => twoHandInput = true;

            // Holding the Input, sets the bool to true
            playerInputActions.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerInputActions.PlayerActions.Shift.performed += i => shiftInput = true;

            // Releasing the Input, sets the bool to false
            playerInputActions.PlayerActions.Sprint.canceled += i => sprintInput = false;
            playerInputActions.PlayerActions.Shift.canceled += i => shiftInput = false;

            // Lock On
            playerInputActions.PlayerMovement.LockOnTargetLeft.performed += i => switchLeftTargetInput = true;
            playerInputActions.PlayerMovement.LockOnTargetRight.performed += i => switchRightTargetInput = true;
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
        HandleSprintingInput();
        HandleJumpInput();
        HandleQuickSlotsInput();
        HandleInteractingButtonInput();
        HandleInventoryInput();
        HandleLockOnInput();
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

        if (!isLockedOn || player.playerLocomotion.isSprinting)
        {
            player.playerAnimationManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerLocomotion.isSprinting);
        }
        else
        {
            player.playerAnimationManager.UpdateAnimatorMovementParameters(horizontalInput, verticalInput, player.playerLocomotion.isSprinting);
        }
    }

    private void HandleCameraMovementInput() 
    {
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;
    }

    // Lock On
    private void HandleLockOnInput()
    {
        if (lockOnInput && !isLockedOn)
        {
            lockOnInput = false;
            isLockedOn = true;
            PlayerCamera.instance.HandleLockOn();
            if (PlayerCamera.instance.nearestLockOnTarget != null)
            {
                PlayerCamera.instance.currentLockOnTarget = PlayerCamera.instance.nearestLockOnTarget;
                isLockedOn = true;
            }
        }
        else if (lockOnInput && isLockedOn)
        {
            lockOnInput = false;
            isLockedOn = false;
            PlayerCamera.instance.ClearLockOnTargets();
        }

        if (isLockedOn && switchRightTargetInput)
        {
            switchRightTargetInput = false;
            PlayerCamera.instance.HandleLockOn();
            if(PlayerCamera.instance.rightLockTarget != null)
            {
                PlayerCamera.instance.currentLockOnTarget = PlayerCamera.instance.rightLockTarget;
            }
        }

        if (isLockedOn && switchLeftTargetInput)
        {
            switchLeftTargetInput = false;
            PlayerCamera.instance.HandleLockOn();
            if (PlayerCamera.instance.leftLockTarget != null)
            {
                PlayerCamera.instance.currentLockOnTarget = PlayerCamera.instance.leftLockTarget;
            }
        }

        PlayerCamera.instance.SetCameraHeight();
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

    private void HandleSprintingInput()
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

    //private void HandleQuickSlotsInput(InputAction.CallbackContext context)
    //{
    //    if(context.phase == InputActionPhase.Started)
    //    {
    //        if (playerInputActions.PlayerActions.Shift.ReadValue<float>() == 1f)
    //        {
    //            Debug.Log("Testtt");
    //            player.playerInventoryManager.ChangeRightHandWeapon();
    //        }
    //    }
    //}

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;

            player.playerLocomotion.AttemptToPerformJump();
        }
    }

    private void HandleQuickSlotsInput()
    {
        if (player.isPerformingAction || player.isJumping || !player.isGrounded)
            return;

        if (switchWeapon)
        {
            playerInventoryManager.ChangeRightWeapon();
            switchWeapon = false;
        }
    }

    private void HandleInteractingButtonInput()
    {
        if (interactInput)
        {
            Debug.Log("E");

        }
    }

    private void HandleInventoryInput()
    {
        if (selectInput)
        {
            player.inventoryFlag = !player.inventoryFlag;

            if(player.inventoryFlag )
            {
                PlayerUIManager.instance.OpenSelectWindow();
                PlayerUIManager.instance.UpdateUI();
                PlayerUIManager.instance.hudWindow.SetActive(false);
            }
            else
            {
                PlayerUIManager.instance.CloseSelectWindow();
                PlayerUIManager.instance.CloseAllInventoryWindows();
                PlayerUIManager.instance.hudWindow.SetActive(true);
            }
        }
    }

    private void HandleAttackInputs(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (playerInputActions.PlayerActions.Shift.ReadValue<float>() == 1f)
            {
                player.playerCombatManager.HandleHeavyAttack(playerInventoryManager.rightWeapon);
            }
            else
            {
                if (player.canDoCombo)
                {
                    comboFlag = true;
                    player.playerCombatManager.HandleWeaponCombo(playerInventoryManager.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (!player.isGrounded)
                    {
                        attackInput = false;
                        return;
                    }

                    if (player.isPerformingAction || player.canDoCombo)
                        return;

                    player.playerCombatManager.HandleLightAttack(playerInventoryManager.rightWeapon);
                }
            }
        }
    }

}
