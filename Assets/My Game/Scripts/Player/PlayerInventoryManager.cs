using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    PlayerManager player;
    PlayerWeaponSlotManager playerWeaponSlotManager;

    public List<WeaponItem> weaponInventory;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
    }

    protected override void Start()
    {
        rightWeapon = weaponsInRightHandSlots[0];
        leftWeapon = weaponsInLeftHandSlots[0];
        playerWeaponSlotManager.LoadBothWeaponOnSlots();
    }

    public void ChangeRightWeapon()
    {
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

        rightHandWeaponIndex = rightHandWeaponIndex + 1;

        if (rightHandWeaponIndex == 0 && weaponsInRightHandSlots[0] != null)
        {
            rightWeapon = weaponsInRightHandSlots[rightHandWeaponIndex];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[rightHandWeaponIndex], false);
        }

        else if (rightHandWeaponIndex == 0 && weaponsInRightHandSlots[0] == null)
        {
            rightHandWeaponIndex = rightHandWeaponIndex + 1;
        }

        else if (rightHandWeaponIndex == 1 && weaponsInRightHandSlots[1] != null)
        {
            rightWeapon = weaponsInRightHandSlots[rightHandWeaponIndex];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[rightHandWeaponIndex], false);
        }

        else if (rightHandWeaponIndex == 1 && weaponsInRightHandSlots[1] == null)
        {
            rightHandWeaponIndex = rightHandWeaponIndex + 1;
        }

        if (rightHandWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            rightHandWeaponIndex = -1;
            rightWeapon = playerWeaponSlotManager.unarmedWeapon;
            playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmedWeapon, false);
        }
    }

    public void ChangeLeftWeapon()
    {
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

        leftHandWeaponIndex = leftHandWeaponIndex + 1;

        if (leftHandWeaponIndex == 0 && weaponsInLeftHandSlots[0] != null)
        {
            leftWeapon = weaponsInLeftHandSlots[leftHandWeaponIndex];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[leftHandWeaponIndex], true);
        }

        else if (leftHandWeaponIndex == 0 && weaponsInLeftHandSlots[0] == null)
        {
            leftHandWeaponIndex = leftHandWeaponIndex + 1;
        }

        else if (leftHandWeaponIndex == 1 && weaponsInLeftHandSlots[1] != null)
        {
            leftWeapon = weaponsInRightHandSlots[leftHandWeaponIndex];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[leftHandWeaponIndex], true);
        }

        else if (leftHandWeaponIndex == 1 && weaponsInLeftHandSlots[1] == null)
        {
            leftHandWeaponIndex = leftHandWeaponIndex + 1;
        }

        if (leftHandWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            leftHandWeaponIndex = -1;
            leftWeapon = playerWeaponSlotManager.unarmedWeapon;
            playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmedWeapon, true);
        }
    }
}
