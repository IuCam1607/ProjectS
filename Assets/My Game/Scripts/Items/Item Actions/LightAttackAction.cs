using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Light Attack Action")]
public class LightAttackAction : ItemAction
{
    //public override void PerformAction(PlayerManager player)
    //{
    //    if (player.playerStatsManager.currentStamina <= 0)
    //        return;

    //    if (player.playerLocomotion.isSprinting)
    //    {
    //        HandleRunningAttack(player);
    //        return;
    //    }

    //    if (player.canDoCombo)
    //    {
    //        if (!player.isGrounded)
    //            return;

    //        else
    //        {
    //            player.playerInput.comboFlag = true;
    //            player.playerAnimationManager.animator.SetBool("isUsingRightHand", true);
    //            HandleWeaponCombo(player);
    //            player.playerInput.comboFlag = false;
    //        }
    //    }
    //    else
    //    {
    //        if (!player.isGrounded)
    //        {
    //            player.playerInput.leftMouseInput = false;
    //            return;
    //        }

    //        if (player.isPerformingAction || player.canDoCombo)
    //            return;

    //        HandleLightAttack(player);
    //    }
    //}
    //public void HandleLightAttack(PlayerManager player)
    //{
    //    if (player.isUsingLeftHand)
    //    {
    //        player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.OH_Light_Attack_01, true, true, false, false, true);
    //        player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_01;
    //    }
    //    else if (player.isUsingRightHand)
    //    {

    //        if (player.playerInput.twoHandFlag)
    //        {
    //            player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.TH_Light_Attack_01, true);
    //            player.playerCombatManager.lastAttack = player.playerCombatManager.TH_Light_Attack_01;
    //        }
    //        else
    //        {
    //            player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.OH_Light_Attack_01, true);
    //            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_01;
    //        }
    //    }

    //}

    //public void HandleRunningAttack(PlayerManager player)
    //{
    //    if (player.isUsingLeftHand)
    //    {
    //        player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.OH_Runnging_Attack_01, true, true, false, false, true);
    //        player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Runnging_Attack_01;
    //    }
    //    else if (player.isUsingRightHand)
    //    {
    //        if (player.playerInput.twoHandFlag)
    //        {
    //            player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.TH_Running_Attack_01, true);
    //            player.playerCombatManager.lastAttack = player.playerCombatManager.TH_Running_Attack_01;
    //        }
    //        else
    //        {
    //            player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.OH_Runnging_Attack_01, true);
    //            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Runnging_Attack_01;
    //        }
    //    }
    //}

    //public void HandleWeaponCombo(PlayerManager player)
    //{
    //    if (player.playerInput.comboFlag)
    //    {
    //        player.playerAnimationManager.animator.SetBool("canDoCombo", false);

    //        if (player.isRolling || player.isJumping)
    //        {
    //            player.canDoCombo = false;
    //            player.playerInput.comboFlag = false;
    //            return;
    //        }

    //        if (player.isUsingLeftHand)
    //        {
    //            if (player.isTwoHandingWeapon)
    //            {
    //                if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_01)
    //                {
    //                    player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.OH_Light_Attack_02, true, true, false, false, true);
    //                    player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_02;
    //                }
    //                else if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_02)
    //                {
    //                    HandleLightAttack(player);
    //                }
    //            }
    //            else if (player.isUsingRightHand)
    //            {
    //                if (player.isTwoHandingWeapon)
    //                {
    //                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.TH_Light_Attack_01)
    //                    {
    //                        player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.TH_Light_Attack_02, true, true);
    //                        player.playerCombatManager.lastAttack = player.playerCombatManager.TH_Light_Attack_02;
    //                    }
    //                    else if (player.playerCombatManager.lastAttack == player.playerCombatManager.TH_Light_Attack_02)
    //                    {
    //                        player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.TH_Light_Attack_03, true, true);
    //                        player.playerCombatManager.lastAttack = player.playerCombatManager.TH_Light_Attack_03;
    //                    }
    //                }
    //                else
    //                {
    //                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_01)
    //                    {
    //                        player.playerAnimationManager.PlayTargetActionAnimation(player.playerCombatManager.OH_Light_Attack_02, true, true);
    //                        player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_02;
    //                    }
    //                    else if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_02)
    //                    {
    //                        HandleLightAttack(player);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
