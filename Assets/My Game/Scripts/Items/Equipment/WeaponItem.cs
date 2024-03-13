using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public bool isUnarmed;

    [Header("Animator Replacer")]
    public AnimatorOverrideController weaponController;

    [Header("Weapon Type")]
    public WeaponType weaponType;
    public string offHandIdleAnimation = "OH_Idle";

    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Weapon Art")]
    public string weapon_Art;

    [Header("Damage")]
    public int physicalDamage;
    public int fireDamage;
    public int criticalDamageMultiplier = 4;

    [Header("Poise")]
    public float poiseBreak;
    public float offensivePoiseBonus;

    [Header("Absorption")]
    public float physicalDamageAbsorption;

    [Header("Stamina Costs")]
    public int baseStaminaCost = 20;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplire;

    [Header("Item Actions")]
    public ItemAction tap_LM_Action;
    public ItemAction hold_LM_Action;
    public ItemAction tap_RM_Action;
    public ItemAction hold_RM_Action;
    public ItemAction tap_RT_Action;
    public ItemAction hold_RT_Action;
    public ItemAction tap_LT_Action;
    public ItemAction hold_LT_Action;
}
