using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;
    WeaponSlotManager weaponSlotManager;
    public AnimatorManager animatorManager;

    LayerMask backStabLayer = 1 << 14;
    LayerMask riposteLayer = 1 << 15;

    public string lastAttack;

    private void Awake()
    {
        animatorManager = GetComponentInParent<AnimatorManager>();  
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        player = GetComponentInParent<PlayerManager>();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;

        if (player.playerInput.comboFlag)
        {
           player.playerAnimationManager.animator.SetBool("canDoCombo", false);

            if (player.isRolling || player.isJumping)
            {
                player.canDoCombo = false;
                player.playerInput.comboFlag = false;
                return;
            }

            if (lastAttack == weapon.OH_Light_Attack_01)
            {
                player.playerAnimationManager.PlayTargetActionAnimation(weapon.OH_Light_Attack_02, true, true);
                lastAttack = weapon.OH_Light_Attack_02;
            }
            else if (lastAttack == weapon.OH_Light_Attack_02)
            {
                HandleLightAttack(weapon);
            }
            else if (lastAttack == weapon.TH_Light_Attack_01)
            {
                player.playerAnimationManager.PlayTargetActionAnimation(weapon.TH_Light_Attack_02, true, true);
                lastAttack = weapon.TH_Light_Attack_02;
            }
            else if (lastAttack == weapon.TH_Light_Attack_02)
            {
                player.playerAnimationManager.PlayTargetActionAnimation(weapon.TH_Light_Attack_03, true, true);
                lastAttack = weapon.TH_Light_Attack_03;
            }
        }   
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;

        if (weapon.isUnarmed)
        {
            return;
        }

        weaponSlotManager.attackingWeapon = weapon;

        if (player.playerInput.twoHandFlag)
        {
            player.playerAnimationManager.PlayTargetActionAnimation(weapon.TH_Light_Attack_01, true, true);
            lastAttack = weapon.TH_Light_Attack_01;
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation(weapon.OH_Light_Attack_01, true, true);
            lastAttack = weapon.OH_Light_Attack_01;
        }
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        if(player.playerStatsManager.currentStamina <= 0)
            return;

        if (weapon.isUnarmed)
        {
            return;
        }

        weaponSlotManager.attackingWeapon = weapon;

        if (player.playerInput.twoHandFlag)
        {
            
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation(weapon.OH_Heavy_Attack_01, true);
            lastAttack = weapon.OH_Heavy_Attack_01;
        }
    }

    #region Input Action
    public void HandleLMAction()
    {
        if(player.playerInventoryManager.rightWeapon.isMeleeWeapon)
        {
            PerformLMMeleeAction();
        }
        else if (player.playerInventoryManager.rightWeapon.isSpellCaster || 
            player.playerInventoryManager.rightWeapon.isPyroCaster || 
            player.playerInventoryManager.rightWeapon.isFaithCaster)
        {
            PerformLMMagicAction(player.playerInventoryManager.rightWeapon);
        }
    }

    public void HandleRMAction()
    {
        if (player.playerInventoryManager.leftWeapon.isShieldWeapon)
        {
            if (player.playerInput.shiftInput == true)
            {
                PerformRMWeaponArt(player.playerInput.twoHandFlag);
            }
            else
            {
                PerformRMBlockingAction();
            }
            
        }
        else if (player.playerInventoryManager.leftWeapon.isMeleeWeapon)
        {
            
        }
    }

    #endregion

    #region Attack Action
    private void PerformLMMeleeAction()
    {
        if (player.canDoCombo)
        {
            if (!player.isGrounded)
                return;

            else
            {
                player.playerInput.comboFlag = true;
                player.playerAnimationManager.animator.SetBool("isUsingRightHand", true);
                HandleWeaponCombo(player.playerInventoryManager.rightWeapon);
                player.playerInput.comboFlag = false;
            }
        }
        else
        {
            if (!player.isGrounded)
            {
                player.playerInput.attackInput = false;
                return;
            }

            if (player.isPerformingAction || player.canDoCombo)
                return;

            player.playerAnimationManager.animator.SetBool("isUsingRightHand", true);
            HandleLightAttack(player.playerInventoryManager.rightWeapon);
        }
    }

    private void PerformLMMagicAction(WeaponItem weapon)
    {
        if (player.isPerformingAction || !player.isGrounded)
            return;

        if (weapon.isFaithCaster)
        {
            if (player.playerInventoryManager.currentSpell != null && player.playerInventoryManager.currentSpell.isFaithSpell)
            {
                if (player.playerStatsManager.currentFocusPoint >= player.playerInventoryManager.currentSpell.focusPointCost)
                {
                    player.playerInventoryManager.currentSpell.AttempToCastSpell(player.playerAnimationManager, player.playerStatsManager, weaponSlotManager);
                }
                else
                {
                    player.playerAnimationManager.PlayTargetActionAnimation("Shrug", true);
                }
            }
        }
        else if (weapon.isPyroCaster)
        {
            if (player.playerInventoryManager.currentSpell != null && player.playerInventoryManager.currentSpell.isPyroSpell)
            {
                if (player.playerStatsManager.currentFocusPoint >= player.playerInventoryManager.currentSpell.focusPointCost)
                {
                    player.playerInventoryManager.currentSpell.AttempToCastSpell(player.playerAnimationManager, player.playerStatsManager, weaponSlotManager);
                }
                else
                {
                    player.playerAnimationManager.PlayTargetActionAnimation("Shrug", true);
                }
            }
        }
    }

    private void PerformRMWeaponArt(bool isTwoHanding)
    {
        if (player.isPerformingAction || !player.isGrounded)
            return;

        if (isTwoHanding)
        {
            
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation(player.playerInventoryManager.leftWeapon.weapon_Art, true);
        }
    }

    private void SuccesfullyCastSpell()
    {
        player.playerInventoryManager.currentSpell.SuccessfullyCastSpell(player.playerAnimationManager, player.playerStatsManager, PlayerCamera.instance, weaponSlotManager);
        player.playerAnimationManager.animator.SetBool("isFiringSpell", true);
    }

    #endregion

    #region Defense Actions

    private void PerformRMBlockingAction()
    {
        if (player.isPerformingAction)
            return;

        if (player.isBlocking)
            return;

        player.playerAnimationManager.PlayTargetActionAnimation("Block_Start", false, false , true, true);
        player.playerEquipment.OpenBlockingCollider();
        player.isBlocking = true;

    }

    #endregion

    public void AttemptBackStabOrRiposte()
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;

        RaycastHit hit;
        if (Physics.Raycast(player.playerInput.criticalAttackCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        { 
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

            if (enemyCharacterManager != null)
            {
                player.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = player.transform.root.eulerAngles;   
                rotationDirection = hit.transform.position - player.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(player.transform.rotation, tr, 500 * Time.deltaTime);
                player.transform.rotation = targetRotation;

                int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                player.playerAnimationManager.PlayTargetActionAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayTargetActionAnimation("Back Stabbed", true, false, false);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().animator.SetBool("isInteracting", true);
            }
        }
        else if (Physics.Raycast(player.playerInput.criticalAttackCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, riposteLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

            if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                player.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = player.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - player.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();

                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(player.transform.rotation, tr, 500 * Time.deltaTime);
                player.transform.rotation = targetRotation;

                int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                player.playerAnimationManager.PlayTargetActionAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayTargetActionAnimation("Riposted", true, false, false);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().animator.SetBool("isInteracting", true);
            }

        }
    }
}
