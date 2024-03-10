using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : Item
{
    public GameObject spellWarmUpFX;
    public GameObject spellCastFX;

    public string spellAnimation;

    [Header("Spell Cost")]
    public int focusPointCost;

    [Header("Spell Type")]
    public bool isFaithSpell;
    public bool isMagicSpell;
    public bool isPyroSpell;

    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttempToCastSpell(PlayerAnimatorManager playerAnimator, 
        PlayerStatsManager playerStats, 
        PlayerWeaponSlotManager weaponSlotManager)
    {
        Debug.Log("You attemp to cast a Spell!");
    }

    public virtual void SuccessfullyCastSpell(PlayerAnimatorManager playerAnimator, 
        PlayerStatsManager playerStats, 
        PlayerCamera playerCamera, 
        PlayerWeaponSlotManager weaponSlotManager)
    {
        Debug.Log("You Successfully cast a Spell!");
        playerStats.DeductFocusPoints(focusPointCost);
    }
}
