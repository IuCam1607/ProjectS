using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttempToCastSpell(PlayerAnimatorManager playerAnimator, PlayerStatsManager playerStats)
    {
        base.AttempToCastSpell(playerAnimator, playerStats);
        //GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, playerAnimator.transform);
        playerAnimator.PlayTargetActionAnimation(spellAnimation, true);
        Debug.Log("Attemp to cast a Healing Spell!");
    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager playerAnimator, PlayerStatsManager playerStats)
    {
        base.SuccessfullyCastSpell(playerAnimator, playerStats);
        //GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerAnimator.transform);
        playerStats.HealPlay(healAmount);
        Debug.Log("Spell cast successful!");
    }
}
