using MoreMountains.Feedbacks;
using System;
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

    public AudioFeedBack[] oh_attack_sfx;
    public AudioFeedBack[] th_attack_sfx;
}

[Serializable]
public class AudioFeedBack
{
    public AudioClip audio;
    
    [Range(0, 1)] public float minVolume = 1;
    [Range(0, 1)] public float maxVolume = 1;
    public float delay;
}
