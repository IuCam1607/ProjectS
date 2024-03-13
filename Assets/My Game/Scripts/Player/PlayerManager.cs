using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : CharacterManager
{
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

    [Header("UI")]
    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInput = GetComponent<PlayerInputManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectManager = GetComponent<PlayerEffectManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        interactableUI = FindAnyObjectByType<InteractableUI>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerAnimationManager = GetComponent<PlayerAnimatorManager>();
        playerEquipment = GetComponent<PlayerEquipmentManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        playerAnimationManager.animator.SetBool("isGrounded", isGrounded);
        canDoCombo = playerAnimationManager.animator.GetBool("canDoCombo");
        isUsingRightHand = playerAnimationManager.animator.GetBool("isUsingRightHand");
        isUsingLeftHand = playerAnimationManager.animator.GetBool("isUsingLeftHand");
        isInvulnerable = playerAnimationManager.animator.GetBool("isInvulnerable");
        isFiringSpell = playerAnimationManager.animator.GetBool("isFiringSpell");
        canRotate = playerAnimationManager.animator.GetBool("canRotate");
        playerAnimationManager.animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
        playerAnimationManager.animator.SetBool("isBlocking", isBlocking);
        playerAnimationManager.animator.SetBool("isDead", playerStatsManager.isDead);

        playerInput.HandleAllInputs();
        playerStatsManager.RegenerateStamina();

        CheckForInteractableObject();

    }
    //protected override void FixedUpdate()
    //{

    //}

    private void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
        playerInput.interactInput = false;
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






    //public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    //{
    //    currentCharacterData.characterName = characterName.ToString();
    //    currentCharacterData.xPosition = transform.position.x;
    //    currentCharacterData.yPosition = transform.position.y;
    //    currentCharacterData.zPosition = transform.position.z;
    //}

    //public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    //{
    //    characterName = currentCharacterData.characterName;
    //    Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
    //    transform.position = myPosition;
    //}
}
