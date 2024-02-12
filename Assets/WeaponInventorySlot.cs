using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    PlayerInventoryManager playerInventory;
    WeaponSlotManager weaponSlotManager;
    PlayerUIManager playerUIManager;

    public Image icon;
    WeaponItem item;

    private void Awake()
    {
        playerInventory = FindAnyObjectByType<PlayerInventoryManager>();
        weaponSlotManager = FindAnyObjectByType<WeaponSlotManager>();
        playerUIManager = FindAnyObjectByType<PlayerUIManager>();
    }

    public void AddItem(WeaponItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        if (playerUIManager.rightHandSlot01Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[0]);
            playerInventory.weaponsInRightHandSlots[0] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (playerUIManager.rightHandSlot02Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[1]);
            playerInventory.weaponsInRightHandSlots[1] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (playerUIManager.leftHandSlot01Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[0]);
            playerInventory.weaponsInLeftHandSlots[0] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (playerUIManager.leftHandSlot02Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[1]);
            playerInventory.weaponsInLeftHandSlots[1] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else
        {
            return;
        }

        playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlots[playerInventory.rightHandWeaponIndex];
        playerInventory.leftWeapon = playerInventory.weaponsInLeftHandSlots[playerInventory.leftHandWeaponIndex];


        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);

        playerUIManager.equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventory);
        playerUIManager.ResetAllSelectedSlots();
    }
}
