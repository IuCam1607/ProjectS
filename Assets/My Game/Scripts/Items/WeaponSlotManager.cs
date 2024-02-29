using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    public WeaponItem attackingWeapon;

    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    WeaponHolderSlot backSlot;

    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    QuickSlotsUI quickSlotsUI;

    PlayerStatsManager playerStats;
    PlayerManager player;

    private void Awake()
    {
        quickSlotsUI = FindAnyObjectByType<QuickSlotsUI>();
        InitializeWeaponSlots();
        playerStats = GetComponentInParent<PlayerStatsManager>();
        player = GetComponentInParent<PlayerManager>();
    }

    private void InitializeWeaponSlots()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();

        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
            else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
            }
        }
    }

    public void LoadBothWeaponOnSlots()
    {
        LoadWeaponOnSlot(player.playerInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(player.playerInventoryManager.leftWeapon, true);
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.currentWeapon = weaponItem;
            leftHandSlot.LoadWeaponModel(weaponItem);
            quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
            LoadLeftWeaponDamageCollider();
        }
        else
        {
            

            if (player.playerInput.twoHandFlag)
            {
                backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                leftHandSlot.UnloadWeaponAndDestroy();
                player.playerAnimationManager.animator.CrossFade(weaponItem.TH_Idle, 0.2f);
            }
            else
            {
                player.playerAnimationManager.animator.CrossFade("Both Arms Empty", 0.2f);

                backSlot.UnloadWeaponAndDestroy();
            }


            rightHandSlot.currentWeapon = weaponItem;
            rightHandSlot.LoadWeaponModel(weaponItem);
            quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
            LoadRightWeaponDamageCollider();
        }
    }

    #region Handle Weapon Damage Collider

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        if (!player.playerInventoryManager.leftWeapon.isUnarmed)
        {
            leftHandDamageCollider.currentWeaponDamage = player.playerInventoryManager.leftWeapon.baseDamage;
        }
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        if (!player.playerInventoryManager.rightWeapon.isUnarmed)
        {
            rightHandDamageCollider.currentWeaponDamage = player.playerInventoryManager.rightWeapon.baseDamage;
        }

    }

    public void OpenDamageCollider()
    {
        if (player.isUsingRightHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        else if (player.isUsingLeftHand)
        {
            leftHandDamageCollider.EnableDamageCollider();
        }
    }


    public void CloseDamageCollider()
    {
         rightHandDamageCollider.DisableDamageCollider();
         leftHandDamageCollider.DisableDamageCollider();
    }

    #endregion


    #region Handle Weapon's Stamina Drainage
    public void DrainStaminaLightAttack()
    {
        playerStats.TakeStaminaCost(attackingWeapon.baseStaminaCost * attackingWeapon.lightAttackMultiplier);
    }

    public void DrainStaminaHeavyAttack()
    {
        playerStats.TakeStaminaCost(attackingWeapon.baseStaminaCost * attackingWeapon.heavyAttackMultiplire);
    }
    #endregion
}
