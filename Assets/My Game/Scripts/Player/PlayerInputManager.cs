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
    private PlayerWeaponSlotManager weaponSlotManager;
    private BlockingCollider blockingCollider;

    public PlayerManager player;
    

    [Header("Player Camera Input")]
    [SerializeField] private Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("Flags")]
    public bool comboFlag;
    public bool isLockedOn;
    public bool twoHandFlag;


    [Header("Player Movement Input")]
    [SerializeField] private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("Player Action Input")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;
    [SerializeField] bool jumpInput = false;
    public bool shiftInput = false;
    public bool attackInput = false;
    public bool interactInput = false;
    public bool selectInput;
    public bool lockOnInput = false;
    public bool switchRightTargetInput = false;
    public bool switchLeftTargetInput = false;
    public bool twoHandInput = false;
    public bool criticalAttackInput = false;
    public bool rightMouseInput = false;
    public bool useItemInput = false;

    private float scrollUp;
    private float scrollDown;

    public Transform criticalAttackCastStartPoint;



    private void Awake()
    {
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        blockingCollider = GetComponentInChildren<BlockingCollider>();

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
            playerInputActions.PlayerActions.Interact.performed += i => interactInput = true;
            playerInputActions.PlayerActions.Selection.performed += i => selectInput = true;
            playerInputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            playerInputActions.PlayerActions.UseItem.performed += i => useItemInput = true;
            playerInputActions.PlayerActions.Attack.started += HandleLeftMouseInput;
            playerInputActions.PlayerActions.RightMouse.started += HandleRightMouseInput;

            playerInputActions.PlayerActions.TwoHand.performed += i =>  twoHandInput = true;
            playerInputActions.PlayerActions.CriticalAttack.performed += i => criticalAttackInput = true;
            playerInputActions.PlayerActions.RightMouse.performed += i => rightMouseInput = true;


            // Scroll Up and Down
            playerInputActions.PlayerActions.ScrollUp.performed += OnSwitchRightWeaponPerform;
            playerInputActions.PlayerActions.ScrollDown.performed += OnSwitchLeftWeaponPerform;

            // Holding the Input, sets the bool to true
            playerInputActions.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerInputActions.PlayerActions.Shift.performed += i => shiftInput = true;
            playerInputActions.PlayerActions.RightMouse.performed += i => rightMouseInput = true;

            // Releasing the Input, sets the bool to false
            playerInputActions.PlayerActions.Sprint.canceled += i => sprintInput = false;
            playerInputActions.PlayerActions.Shift.canceled += i => shiftInput = false;
            playerInputActions.PlayerActions.RightMouse.canceled += i => rightMouseInput = false;

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
        HandleInteractingButtonInput();
        HandleInventoryInput();
        HandleLockOnInput();
        HandleTwoHandInput();
        HandleCriticalAttackInput();
        HandleUseConsumableItem();

        if (playerInputActions.PlayerActions.RightMouse.ReadValue<float>() == 0f)
        {
            player.isBlocking = false;

            if (blockingCollider.blockingCollider.enabled)
            {
                blockingCollider.DisableBlockingCollider();
            }
        }
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

            comboFlag = false;
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

    private void HandleLeftMouseInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (playerInputActions.PlayerActions.Shift.ReadValue<float>() == 1f)
            {
                player.playerCombatManager.HandleHeavyAttack(playerInventoryManager.rightWeapon);
            }
            else
            {
                player.playerCombatManager.HandleLMAction();
            }
        }
    }

    public void HandleRightMouseInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (playerInputActions.PlayerActions.Shift.ReadValue<float>() == 1f)
            {
                if (twoHandFlag)
                {

                }
                else
                {
                    player.playerCombatManager.HandleRMAction();
                }
            }

            if (playerInputActions.PlayerActions.RightMouse.ReadValue<float>() == 1f)
            {        
                player.playerCombatManager.HandleRMAction();
                
            }


        }
    }

    private void HandleTwoHandInput()
    {
        if (twoHandInput)
        {
            twoHandInput = false;

            twoHandFlag = !twoHandFlag;

            if (twoHandFlag)
            {
                weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
            }
            else
            {
                weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.leftWeapon, true);
            }
        }
    }

    private void HandleCriticalAttackInput()
    {
        if (criticalAttackInput)
        {
            criticalAttackInput = false;
            player.playerCombatManager.AttemptBackStabOrRiposte();

        }
    }   

    public void OnSwitchRightWeaponPerform(InputAction.CallbackContext context)
    {
        scrollUp = context.ReadValue<float>();

        if (player.isPerformingAction || player.isJumping || !player.isGrounded)
            return;

        if (scrollUp > 0 && shiftInput)
        {
            playerInventoryManager.ChangeRightWeapon();
        }
    }

    public void OnSwitchLeftWeaponPerform(InputAction.CallbackContext context)
    {
        scrollDown = context.ReadValue<float>();

        if (player.isPerformingAction || player.isJumping || !player.isGrounded)
            return;

        if (scrollDown > 0 && shiftInput)
        {
            playerInventoryManager.ChangeLeftWeapon();
        }
    }

    private void HandleUseConsumableItem()
    {
        if (player.isPerformingAction || !player.isGrounded)
            return;

        if (useItemInput)
        {
            useItemInput = false;
            playerInventoryManager.currentConsumable.AttempToConsumeItem(player.playerAnimationManager, weaponSlotManager, player.playerEffectManager);
        }
    }
}
