using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttempToCastSpell(PlayerAnimatorManager playerAnimator, PlayerStatsManager playerStats, WeaponSlotManager weaponSlotManager)
    {
        base.AttempToCastSpell(playerAnimator, playerStats, weaponSlotManager);
        //GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, playerAnimator.transform);
        playerAnimator.PlayTargetActionAnimation(spellAnimation, true);
        Debug.Log("Attemp to cast a Healing Spell!");
    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager playerAnimator,
        PlayerStatsManager playerStats, 
        PlayerCamera playerCamera, 
        WeaponSlotManager weaponSlotManager)
    {
        base.SuccessfullyCastSpell(playerAnimator, playerStats, playerCamera, weaponSlotManager);
        //GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerAnimator.transform);
        playerStats.HealPlayer(healAmount);
        Debug.Log("Spell cast successful!");
    }
}
