using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public bool isUnarmed;

    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Idle Animations")]
    public string TH_Idle;

    [Header("One Handed Attack Animations")]
    public string OH_Light_Attack_01;
    public string OH_Light_Attack_02;
    public string OH_Heavy_Attack_01;

    [Header("Two Handed Attack Animation")]
    public string TH_Light_Attack_01;
    public string TH_Light_Attack_02;
    public string TH_Light_Attack_03;

    [Header("Weapon Art")]
    public string weapon_Art;

    [Header("Damage")]
    public int baseDamage = 25;
    public int criticalDamageMultiplier = 4;

    [Header("Stamina Costs")]
    public int baseStaminaCost = 20;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplire;

    [Header("Weapon Type")]
    public bool isSpellCaster;
    public bool isFaithCaster;
    public bool isPyroCaster;
    public bool isMeleeWeapon;
    public bool isShieldWeapon;
}
