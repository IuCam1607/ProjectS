using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : CharacterWeaponSlotManager
{
    public WeaponItem rightHandWeapon;
    public WeaponItem leftHandWeapon;

    EnemyStatsManager enemyStats;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStatsManager>();
        LoadWeaponHolderSlots();
    }

    private void Start()
    {
        LoadWeaponOnBothHands();
    }

    private void LoadWeaponHolderSlots()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();

        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.currentWeapon = weapon;
            leftHandSlot.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider(true);
        }
        else
        {
            rightHandSlot.currentWeapon = weapon;
            rightHandSlot.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider(false);
        }
    }

    public void LoadWeaponOnBothHands()
    {
        if (rightHandWeapon != null)
        {
            LoadWeaponOnSlot(rightHandWeapon, false);
        }
        if (leftHandWeapon != null)
        {
            LoadWeaponOnSlot(leftHandWeapon, true);
        }
    }

    public void LoadWeaponDamageCollider(bool isLeft)
    {
        if (isLeft)
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();

            leftHandDamageCollider.physicalDamage = leftHandWeapon.physicalDamage;
            leftHandDamageCollider.fireDamage = leftHandWeapon.fireDamage;

            leftHandDamageCollider.teamIDNumber = enemyStats.teamIDNumber;

        }
        else
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();

            rightHandDamageCollider.physicalDamage = rightHandWeapon.physicalDamage;
            rightHandDamageCollider.fireDamage = rightHandWeapon.fireDamage;

            rightHandDamageCollider.teamIDNumber = enemyStats.teamIDNumber;
        }
    }

    public void OpenDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void CloseDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void DrainStaminaLightAttack()
    {

    }

    public void DrainStaminaHeavyAttack()
    {

    }


    public void GrantWeaponAttackingPoiseBonus()
    {
        enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence + enemyStats.offensivePoiseBonus;
    }

    public void ResetWeaponAttackingPoiseBonus()
    {
        enemyStats.totalPoiseDefence = enemyStats.armorPoiseBonus;
    }
}
