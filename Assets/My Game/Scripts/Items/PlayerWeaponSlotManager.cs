using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
{
    QuickSlotsUI quickSlotsUI;

    PlayerEffectManager playerEffectManager;
    PlayerStatsManager playerStats;
    PlayerManager player;



    protected override void Awake()
    {
        base.Awake();
        playerEffectManager = GetComponent<PlayerEffectManager>();
        playerStats = GetComponent<PlayerStatsManager>();
        player = GetComponent<PlayerManager>();
        quickSlotsUI = FindAnyObjectByType<QuickSlotsUI>();
    }


    public override void LoadBothWeaponOnSlots()
    {
        LoadWeaponOnSlot(player.playerInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(player.playerInventoryManager.leftWeapon, true);
    }

    public override void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (weaponItem != null)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                LoadLeftWeaponDamageCollider();
                player.playerAnimationManager.PlayTargetActionAnimation(weaponItem.offHandIdleAnimation, false, true, true);
            }
            else
            {
                if (player.playerInput.twoHandFlag)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                }
                else
                {
                    backSlot.UnloadWeaponAndDestroy();
                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                player.playerAnimationManager.animator.runtimeAnimatorController = weaponItem.weaponController;
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
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                player.playerAnimationManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        }
    }

    public void SuccessfullyThrowFireBomb()
    {
        Destroy(playerEffectManager.instantiatedFXModel);
        BombConsumableItem fireBombItem = player.playerInventoryManager.currentConsumable as BombConsumableItem;

        GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position, PlayerCamera.instance.cameraPivotTransform.rotation);
        BombDamageCollider damageCollider = activeModelBomb.GetComponentInChildren<BombDamageCollider>();

        if (PlayerCamera.instance.currentLockOnTarget != null)
        {
            activeModelBomb.transform.LookAt(PlayerCamera.instance.currentLockOnTarget.transform);
        }
        else
        {
            activeModelBomb.transform.rotation = Quaternion.Euler(PlayerCamera.instance.cameraPivotTransform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);
        }

        damageCollider.explosionDamage = fireBombItem.baseDamage;
        damageCollider.explosionSplashDamage = fireBombItem.explosiveDamage;
        damageCollider.bombRigidbody.AddForce(activeModelBomb.transform.forward * fireBombItem.forwardVelocity);
        damageCollider.bombRigidbody.AddForce(activeModelBomb.transform.up * fireBombItem.upwardVelocity);
        damageCollider.teamIDNumber = playerStats.teamIDNumber;

    }

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
