using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    PlayerManager player;

    WeaponSlotManager weaponSlotManager;

    public SpellItem currentSpell;
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
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
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
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

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

    public void ChangeLeftWeapon()
    {
        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

        leftHandWeaponIndex = leftHandWeaponIndex + 1;

        if (leftHandWeaponIndex == 0 && weaponsInLeftHandSlots[0] != null)
        {
            leftWeapon = weaponsInLeftHandSlots[leftHandWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[leftHandWeaponIndex], true);
        }

        else if (leftHandWeaponIndex == 0 && weaponsInLeftHandSlots[0] == null)
        {
            leftHandWeaponIndex = leftHandWeaponIndex + 1;
        }

        else if (leftHandWeaponIndex == 1 && weaponsInLeftHandSlots[1] != null)
        {
            leftWeapon = weaponsInRightHandSlots[leftHandWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[leftHandWeaponIndex], true);
        }

        else if (leftHandWeaponIndex == 1 && weaponsInLeftHandSlots[1] == null)
        {
            leftHandWeaponIndex = leftHandWeaponIndex + 1;
        }

        if (leftHandWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            leftHandWeaponIndex = -1;
            leftWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
        }
    }
}
