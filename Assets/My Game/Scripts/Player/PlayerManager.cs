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

    [Header("UI")]
    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;

    [Header("Debug Menu")]
    [SerializeField] bool respawnCharacter = false;

    [Header("Flags")]
    public bool isJumping = false;
    public bool isGrounded = true;

    public bool canDoCombo = false;
    public bool inventoryFlag;
    public bool isRolling = false;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isInvulnerable;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInput = GetComponent<PlayerInputManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectManager = GetComponentInChildren<PlayerEffectManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        interactableUI = FindAnyObjectByType<InteractableUI>();
        playerCombatManager = GetComponentInChildren<PlayerCombatManager>();
        playerAnimationManager = GetComponentInChildren<PlayerAnimatorManager>();
        playerEquipment = GetComponentInChildren<PlayerEquipmentManager>();
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
        playerAnimationManager.animator.SetBool("isBlocking", isBlocking);
        playerAnimationManager.animator.SetBool("isDead", playerStatsManager.isDead);

        playerInput.HandleAllInputs();
        playerStatsManager.RegenerateStamina();

        DebugMenu();
        CheckForInteractableObject();

    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
        playerInput.interactInput = false;
        playerInput.selectInput = false;
    }

    // Delete Later
    private void DebugMenu()
    {
        if (respawnCharacter)
        {
            respawnCharacter = false;
            ReviveCharacter();
        }

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
