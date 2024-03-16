using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    PlayerManager player;

    public List<WeaponItem> weaponInventory;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    protected override void Start()
    {
        base.Start();
        player.playerWeaponSlotManager.LoadBothWeaponOnSlots();
    }

    public void ChangeRightWeapon()
    {
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

        rightHandWeaponIndex = rightHandWeaponIndex + 1;

        if (rightHandWeaponIndex == 0 && weaponsInRightHandSlots[0] != null)
        {
            rightWeapon = weaponsInRightHandSlots[rightHandWeaponIndex];
            player.playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[rightHandWeaponIndex], false);
        }

        else if (rightHandWeaponIndex == 0 && weaponsInRightHandSlots[0] == null)
        {
            rightHandWeaponIndex = rightHandWeaponIndex + 1;
        }

        else if (rightHandWeaponIndex == 1 && weaponsInRightHandSlots[1] != null)
        {
            rightWeapon = weaponsInRightHandSlots[rightHandWeaponIndex];
            player.playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[rightHandWeaponIndex], false);
        }

        else if (rightHandWeaponIndex == 1 && weaponsInRightHandSlots[1] == null)
        {
            rightHandWeaponIndex = rightHandWeaponIndex + 1;
        }

        if (rightHandWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            rightHandWeaponIndex = -1;
            rightWeapon = player.playerWeaponSlotManager.unarmedWeapon;
            player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerWeaponSlotManager.unarmedWeapon, false);
        }
    }

    public void ChangeLeftWeapon()
    {
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

        leftHandWeaponIndex = leftHandWeaponIndex + 1;

        if (leftHandWeaponIndex == 0 && weaponsInLeftHandSlots[0] != null)
        {
            leftWeapon = weaponsInLeftHandSlots[leftHandWeaponIndex];
            player.playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[leftHandWeaponIndex], true);
        }

        else if (leftHandWeaponIndex == 0 && weaponsInLeftHandSlots[0] == null)
        {
            leftHandWeaponIndex = leftHandWeaponIndex + 1;
        }

        else if (leftHandWeaponIndex == 1 && weaponsInLeftHandSlots[1] != null)
        {
            leftWeapon = weaponsInLeftHandSlots[leftHandWeaponIndex];
            player.playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[leftHandWeaponIndex], true);
        }

        else if (leftHandWeaponIndex == 1 && weaponsInLeftHandSlots[1] == null)
        {
            leftHandWeaponIndex = leftHandWeaponIndex + 1;
        }

        if (leftHandWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            leftHandWeaponIndex = -1;
            leftWeapon = player.playerWeaponSlotManager.unarmedWeapon;
            player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerWeaponSlotManager.unarmedWeapon, true);
        }
    }
}
