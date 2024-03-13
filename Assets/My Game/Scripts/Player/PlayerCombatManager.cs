using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;
    PlayerWeaponSlotManager weaponSlotManager;
    public AnimatorManager animatorManager;

    LayerMask backStabLayer = 1 << 14;
    LayerMask riposteLayer = 1 << 15;

    public string lastAttack;

    [Header("Attack Animations")]
    string OH_Light_Attack_01 = "OH_Light_Attack_01";
    string OH_Light_Attack_02 = "OH_Light_Attack_02";
    string OH_Heavy_Attack_01 = "OH_Heavy_Attack_01";
    string OH_Runnging_Attack_01 = "OH_Running_Attack_01";

    string TH_Light_Attack_01 = "TH_Light_Attack_01";
    string TH_Light_Attack_02 = "TH_Light_Attack_02";
    string TH_Light_Attack_03 = "TH_Light_Attack_03";
    string TH_Running_Attack_01 = "TH_Running_Attack_01";

    string weapon_Art = "Weapon_Art";

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();  
        weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        player = GetComponent<PlayerManager>();
    }

    public void HandleLMAction()
    {
        if (player.playerInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword 
            || player.playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
        {
            PerformLMMeleeAction();
        }
        else if (player.playerInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster ||
            player.playerInventoryManager.rightWeapon.weaponType == WeaponType.PyromancyCaster ||
            player.playerInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster)
        {
            PerformMagicAction(player.playerInventoryManager.rightWeapon, false);
        }
    }

    public void HandleRMAction()
    {
        if (player.isTwoHandingWeapon)
        {
            // Bow
        }
        else
        { 
            if ((player.playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield
            || player.playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSword))
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
            else if (player.playerInventoryManager.leftWeapon.weaponType == WeaponType.SpellCaster ||
            player.playerInventoryManager.leftWeapon.weaponType == WeaponType.PyromancyCaster ||
            player.playerInventoryManager.leftWeapon.weaponType == WeaponType.FaithCaster)
            {
                PerformMagicAction(player.playerInventoryManager.leftWeapon, true);
                player.playerAnimationManager.animator.SetBool("isUsingLeftHand", true);
            }
        }
    }


    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;

        if (weapon.isUnarmed)
        {
            return;
        }

        if (player.playerInput.comboFlag)
        {
           player.playerAnimationManager.animator.SetBool("canDoCombo", false);

            if (player.isRolling || player.isJumping)
            {
                player.canDoCombo = false;
                player.playerInput.comboFlag = false;
                return;
            }

            if (lastAttack == OH_Light_Attack_01)
            {
                player.playerAnimationManager.PlayTargetActionAnimation(OH_Light_Attack_02, true, true);
                lastAttack = OH_Light_Attack_02;
            }
            else if (lastAttack == OH_Light_Attack_02)
            {
                HandleLightAttack(weapon);
            }
            else if (lastAttack == TH_Light_Attack_01)
            {
                player.playerAnimationManager.PlayTargetActionAnimation(TH_Light_Attack_02, true, true);
                lastAttack = TH_Light_Attack_02;
            }
            else if (lastAttack == TH_Light_Attack_02)
            {
                player.playerAnimationManager.PlayTargetActionAnimation(TH_Light_Attack_03, true, true);
                lastAttack = TH_Light_Attack_03;
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
            player.playerAnimationManager.PlayTargetActionAnimation(TH_Light_Attack_01, true, true);
            lastAttack = TH_Light_Attack_01;
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation(OH_Light_Attack_01, true, true);
            lastAttack = OH_Light_Attack_01;
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
            player.playerAnimationManager.PlayTargetActionAnimation(OH_Heavy_Attack_01, true);
            lastAttack = OH_Heavy_Attack_01;
        }
    }

    public void HandleRunningAttack(WeaponItem weapon)
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
            player.playerAnimationManager.PlayTargetActionAnimation(TH_Running_Attack_01, true, true);
            lastAttack = TH_Running_Attack_01;
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation(OH_Runnging_Attack_01, true, true);
            lastAttack = OH_Runnging_Attack_01;
        }
    }


    private void PerformLMMeleeAction()
    {
        if (player.playerLocomotion.isSprinting)
        {
            HandleRunningAttack(player.playerInventoryManager.rightWeapon);
            return;
        }

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
                player.playerInput.leftMouseInput = false;
                return;
            }

            if (player.isPerformingAction || player.canDoCombo)
                return;

            player.playerAnimationManager.animator.SetBool("isUsingRightHand", true);
            HandleLightAttack(player.playerInventoryManager.rightWeapon);
        }
    }

    private void PerformMagicAction(WeaponItem weapon, bool isLeftHanded)
    {
        if (player.isPerformingAction || !player.isGrounded)
            return;

        if (weapon.weaponType == WeaponType.FaithCaster)
        {
            if (player.playerInventoryManager.currentSpell != null && player.playerInventoryManager.currentSpell.isFaithSpell)
            {
                if (player.playerStatsManager.currentFocusPoint >= player.playerInventoryManager.currentSpell.focusPointCost)
                {
                    player.playerInventoryManager.currentSpell.AttempToCastSpell(player.playerAnimationManager, player.playerStatsManager, weaponSlotManager, isLeftHanded);
                }
                else
                {
                    player.playerAnimationManager.PlayTargetActionAnimation("Shrug", true);
                }
            }
        }
        else if (weapon.weaponType == WeaponType.PyromancyCaster)
        {
            if (player.playerInventoryManager.currentSpell != null && player.playerInventoryManager.currentSpell.isPyroSpell)
            {
                if (player.playerStatsManager.currentFocusPoint >= player.playerInventoryManager.currentSpell.focusPointCost)
                {
                    player.playerInventoryManager.currentSpell.AttempToCastSpell(player.playerAnimationManager, player.playerStatsManager, weaponSlotManager, isLeftHanded);
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
            player.playerAnimationManager.PlayTargetActionAnimation(weapon_Art, true);
        }
    }

    private void PerformRMBlockingAction()
    {
        if (player.isPerformingAction)
            return;

        if (player.isBlocking)
            return;

        player.playerAnimationManager.PlayTargetActionAnimation("Block_Start", false, false, true, true, false);
        player.playerEquipment.OpenBlockingCollider();
        player.isBlocking = true;
    }



    private void SuccesfullyCastSpell()
    {
        player.playerInventoryManager.currentSpell.SuccessfullyCastSpell(player.playerAnimationManager, player.playerStatsManager, PlayerCamera.instance, weaponSlotManager, player.isUsingLeftHand);
        player.playerAnimationManager.animator.SetBool("isFiringSpell", true);
    }

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

                int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
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

                int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                player.playerAnimationManager.PlayTargetActionAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayTargetActionAnimation("Riposted", true, false, false);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().animator.SetBool("isInteracting", true);
            }

        }
    }
}
