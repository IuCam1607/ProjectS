using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickUp : Interactable
{
    [Header("World Item ID")]
    [SerializeField] int itemID;


    public WeaponItem weapon;
    

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventoryManager playerInventory;
        PlayerLocomotion playerLocomotion;
        PlayerAnimatorManager playerAnimation;
        playerInventory = playerManager.GetComponent<PlayerInventoryManager>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        playerAnimation = playerManager.GetComponentInChildren<PlayerAnimatorManager>();
        

        playerLocomotion.moveDirection = Vector3.zero;
        playerAnimation.PlayTargetActionAnimation("Pick Up Item", true);
        playerInventory.weaponInventory.Add(weapon);
        playerManager.itemInteractableGameObject.GetComponentInChildren<TextMeshProUGUI>().text = weapon.itemName;
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.pickupIcon.texture;
        playerManager.itemInteractableGameObject.SetActive(true);
        Destroy(gameObject);
    }
}
