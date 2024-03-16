using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public static PlayerManager instance;

    [Header("Player")]
    [HideInInspector] public PlayerAnimatorManager playerAnimationManager;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public PlayerLocomotion playerLocomotion;
    [HideInInspector] public PlayerInputManager playerInput;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    [HideInInspector] public PlayerEffectManager playerEffectManager;
    [HideInInspector] public PlayerInventoryManager playerInventoryManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public PlayerEquipmentManager playerEquipment;
    [HideInInspector] public PlayerWeaponSlotManager playerWeaponSlotManager;
    [HideInInspector] public FeedBackManager feedBackManager;

    [Header("Colliders")]
    [HideInInspector] public BlockingCollider blockingCollider;

    public CharacterSaveData characterSaveData;

    [Header("UI")]
    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;



    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Awake();

        interactableUI = FindAnyObjectByType<InteractableUI>();

        feedBackManager = GetComponent<FeedBackManager>();
        characterController = GetComponent<CharacterController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInput = GetComponent<PlayerInputManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectManager = GetComponent<PlayerEffectManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerAnimationManager = GetComponent<PlayerAnimatorManager>();
        playerEquipment = GetComponent<PlayerEquipmentManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        animator = GetComponent<Animator>();
        feedBack = GetComponent<MMF_Player>();
        sfx = feedBack.GetFeedbackOfType<MMF_MMSoundManagerSound>();

        WorldSaveGameManager.instance.player = this;
    }
    private void Start()
    {

    }

    private void Update()
    {
        canDoCombo = animator.GetBool("canDoCombo");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isInvulnerable = animator.GetBool("isInvulnerable");
        isFiringSpell = animator.GetBool("isFiringSpell");
        canRotate = animator.GetBool("canRotate");

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("isDead", isDead);

        playerInput.HandleAllInputs();
        playerStatsManager.RegenerateStamina();

        CheckForInteractableObject();
    }

    protected void FixedUpdate()
    {
        playerInput.interactInput = false;
    }

    private void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
        playerInput.selectInput = false;
    }

    public void ReviveCharacter()
    {
        playerStatsManager.currentHealth = playerStatsManager.maxHealth;
        playerStatsManager.currentStamina = playerStatsManager.maxStamina;
        playerStatsManager.healthBar.SetCurrentHealth(playerStatsManager.currentHealth);

        playerAnimationManager.PlayTargetActionAnimation("Empty", false);
    }

    #region Player Interactions

    public void CheckForInteractableObject()
    {
        RaycastHit hit;
        
        if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, PlayerCamera.instance.collideWithLayers))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (playerInput.interactInput)
                    {      
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if (interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if (itemInteractableGameObject != null && playerInput.interactInput)
            {
                itemInteractableGameObject.SetActive(false);
            }
        }
    }

    public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
    {
        transform.position = playerStandsHereWhenOpeningChest.transform.position;
        playerAnimationManager.PlayTargetActionAnimation("Open Chest", true);
    }

    public void PassThroughFogWallInteraction(Transform fogWallEntrance)
    {
        playerLocomotion.yVelocity = Vector3.zero;
        Vector3 rotationDirection = fogWallEntrance.transform.forward;
        Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
        transform.rotation = turnRotation;

        playerAnimationManager.PlayTargetActionAnimation("Pass Through Fog", true);
    }

    #endregion

    #region Save/Load

    public void SaveCharacterDataToCurrentSaveData(ref CharacterSaveData currentCharacterSaveData)
    {
        currentCharacterSaveData.characterName = playerStatsManager.characterName;
        currentCharacterSaveData.characterLevel = playerStatsManager.playerLevel;

        currentCharacterSaveData.currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        //Equipment
        currentCharacterSaveData.currentRightHandWeaponID = playerInventoryManager.rightWeapon.itemID;
        currentCharacterSaveData.currentLeftHandWeaponID = playerInventoryManager.leftWeapon.itemID;

        if (playerInventoryManager.currentHelmetEquipment != null)
        {
            currentCharacterSaveData.currentHeadGearItemID = playerInventoryManager.currentHelmetEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentHeadGearItemID = -1;
        }
    }

    public void LoadCharacterDataFromCurrentCharacterSaveData(ref CharacterSaveData currentCharacterSaveData)
    {
        transform.position = currentCharacterSaveData.currentPosition;

        playerStatsManager.characterName = currentCharacterSaveData.characterName;
        playerStatsManager.playerLevel = currentCharacterSaveData.characterLevel;



        // Equipment
        playerInventoryManager.rightWeapon = WorldItemDataBase.instance.GetWeaponItemByItemID(currentCharacterSaveData.currentRightHandWeaponID);
        playerInventoryManager.leftWeapon = WorldItemDataBase.instance.GetWeaponItemByItemID(currentCharacterSaveData.currentLeftHandWeaponID);
        playerWeaponSlotManager.LoadBothWeaponOnSlots();

        EquipmentItem headEquipmentItem = WorldItemDataBase.instance.GetEquipmentItemByItemID(currentCharacterSaveData.currentHeadGearItemID);

        if (headEquipmentItem != null)
        {
            playerInventoryManager.currentHelmetEquipment = headEquipmentItem as HelmetEquipment;
            playerEquipment.EquipAllEquipmentModelsOnStart();
        }
    }

    #endregion
}
