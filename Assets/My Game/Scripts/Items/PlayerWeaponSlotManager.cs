using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
{
    QuickSlotsUI quickSlotsUI;

    PlayerEffectManager playerEffectManager;
    PlayerStatsManager playerStats;
    PlayerManager player;

    [Header("Attacking Weapon")]
    public WeaponItem attackingWeapon;

    private void Awake()
    {
        playerEffectManager = GetComponent<PlayerEffectManager>();
        playerStats = GetComponent<PlayerStatsManager>();
        player = GetComponent<PlayerManager>();
        quickSlotsUI = FindAnyObjectByType<QuickSlotsUI>();
        InitializeWeaponSlots();
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
        if (weaponItem != null)
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
        else
        {
            weaponItem = unarmedWeapon;

            if (isLeft)
            {
                player.playerInventoryManager.leftWeapon = unarmedWeapon;
                leftHandSlot.currentWeapon = unarmedWeapon;
                leftHandSlot.LoadWeaponModel(weaponItem);
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else
            {
                player.playerInventoryManager.rightWeapon = unarmedWeapon;
                rightHandSlot.currentWeapon = unarmedWeapon;
                rightHandSlot.LoadWeaponModel(weaponItem);
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                LoadRightWeaponDamageCollider();
            }
        }     
    }

    public void SuccessfullyThrowFireBomb()
    {
        Destroy(playerEffectManager.instantiatedFXModel);
        BombConsumableItem fireBombItem = player.playerInventoryManager.currentConsumable as BombConsumableItem;

        GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position, PlayerCamera.instance.cameraPivotTransform.rotation);
        activeModelBomb.transform.rotation = Quaternion.Euler(PlayerCamera.instance.cameraPivotTransform.eulerAngles.x, player.lockOnTransform.eulerAngles.y, 0);

    }

    #region Handle Weapon Damage Collider

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        leftHandDamageCollider.physicalDamage = player.playerInventoryManager.leftWeapon.physicalDamage;
        leftHandDamageCollider.fireDamage = player.playerInventoryManager.leftWeapon.fireDamage;

        leftHandDamageCollider.teamIDNumber = playerStats.teamIDNumber;

        leftHandDamageCollider.poiseBreak = player.playerInventoryManager.leftWeapon.poiseBreak;
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        rightHandDamageCollider.physicalDamage = player.playerInventoryManager.rightWeapon.physicalDamage;
        rightHandDamageCollider.fireDamage = player.playerInventoryManager.rightWeapon.fireDamage;

        rightHandDamageCollider.teamIDNumber = playerStats.teamIDNumber;

        rightHandDamageCollider.poiseBreak = player.playerInventoryManager.rightWeapon.poiseBreak;
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
        if (rightHandDamageCollider != null)
        {
            rightHandDamageCollider.DisableDamageCollider();
        }
        
        if (leftHandDamageCollider != null)
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
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

    #region Handle Weapon's Poise Bonus

    public void GrantWeaponAttackingPoiseBonus()
    {
        playerStats.totalPoiseDefence = playerStats.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
    }

    public void ResetWeaponAttackingPoiseBonus() 
    {
        playerStats.totalPoiseDefence = playerStats.armorPoiseBonus;
    }

    #endregion
}
