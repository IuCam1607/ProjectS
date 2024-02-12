using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    PlayerManager player;

    WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public WeaponItem unarmedWeapon;

    [Header("Quick Slots")]
    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    public int rightHandWeaponIndex;
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];
    public int leftHandWeaponIndex;

    public List<WeaponItem> weaponInventory;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }
    private void Start()
    {
        rightWeapon = weaponsInRightHandSlots[0];
        leftWeapon = weaponsInLeftHandSlots[0];
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }

    public void ChangeRightWeapon()
    {
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, true, true, true);

        rightHandWeaponIndex = rightHandWeaponIndex + 1;

        if (rightHandWeaponIndex == 0 && weaponsInRightHandSlots[0] != null)
        {
            rightWeapon = weaponsInRightHandSlots[rightHandWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[rightHandWeaponIndex], false);
        }

        else if (rightHandWeaponIndex == 0 && weaponsInRightHandSlots[0] == null)
        {
            rightHandWeaponIndex = rightHandWeaponIndex + 1;
        }

        else if (rightHandWeaponIndex == 1 && weaponsInRightHandSlots[1] != null)
        {
            rightWeapon = weaponsInRightHandSlots[rightHandWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[rightHandWeaponIndex], false);
        }

        else if (rightHandWeaponIndex == 1 && weaponsInRightHandSlots[1] == null)
        {
            rightHandWeaponIndex = rightHandWeaponIndex + 1;
        }

        if (rightHandWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            rightHandWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
        }
    }
}
