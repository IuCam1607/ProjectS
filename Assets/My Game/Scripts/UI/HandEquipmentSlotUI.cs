using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{
    PlayerUIManager playerUIManager;
    public Image icon;
    WeaponItem weapon;

    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool leftHandSlot01;
    public bool leftHandSlot02;

    private void Awake()
    {
        playerUIManager = FindAnyObjectByType<PlayerUIManager>();
    }

    public void AddItem(WeaponItem newWeapon)
    {
        weapon = newWeapon;
        icon.sprite = weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void SelectThisSlot()
    {
        if (rightHandSlot01)
        {
            playerUIManager.rightHandSlot01Selected = true;
        }
        else if (rightHandSlot02) 
        {
            playerUIManager.rightHandSlot02Selected = true;
        }
        else if (leftHandSlot01)
        {
            playerUIManager.leftHandSlot01Selected = true;
        }
        else
        {
            playerUIManager.leftHandSlot02Selected = true;
        }
    }
}
