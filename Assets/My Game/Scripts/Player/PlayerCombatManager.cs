using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;

    WeaponSlotManager weaponSlotManager;

    public string lastAttack;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        player = GetComponent<PlayerManager>();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
            if (player.playerInput.comboFlag)
            {
                player.playerAnimationManager.animator.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_01)
                {
                    player.playerAnimationManager.PlayTargetActionAnimation(weapon.OH_Light_Attack_02, true);
                    lastAttack = weapon.OH_Light_Attack_02;
                }
                if (lastAttack == weapon.OH_Light_Attack_02)
                {
                    HandleLightAttack(weapon);
                }
            }   
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        if (weapon.isUnarmed)
        {
            return;
        }
        else
        {
            weaponSlotManager.attackingWeapon = weapon;
            player.playerAnimationManager.PlayTargetActionAnimation(weapon.OH_Light_Attack_01, true);
            lastAttack = weapon.OH_Light_Attack_01;
        }
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        if (weapon.isUnarmed)
        {
            return;
        }
        else
        {
            weaponSlotManager.attackingWeapon = weapon;
            player.playerAnimationManager.PlayTargetActionAnimation(weapon.OH_Heavy_Attack_01, true);
            lastAttack = weapon.OH_Heavy_Attack_01;
        }
    }
}
