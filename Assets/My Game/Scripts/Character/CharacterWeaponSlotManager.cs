using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponSlotManager : MonoBehaviour
{
    protected CharacterManager character;

    [Header("Unarmed Weapon")]
    public WeaponItem unarmedWeapon;

    [Header("Weapon Slots")]
    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    public WeaponHolderSlot backSlot;

    [Header("Damage Colliders")]
    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    [Header("Attacking Weapon")]
    public WeaponItem attackingWeapon;
    
    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        InitializeWeaponSlots();
    }

    protected virtual void InitializeWeaponSlots()
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
            else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
            }
        }
    }

    public virtual void LoadBothWeaponOnSlots()
    {
        LoadWeaponOnSlot(character.characterInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(character.characterInventoryManager.leftWeapon, true);
    }

    public virtual void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (weaponItem != null)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else
            {
                if (character.isTwoHandingWeapon)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                }
                else
                {
                    backSlot.UnloadWeaponAndDestroy();
                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                //animatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        }
        else
        {
            weaponItem = unarmedWeapon;

            if (isLeft)
            {
                character.characterInventoryManager.leftWeapon = unarmedWeapon;
                leftHandSlot.currentWeapon = unarmedWeapon;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else
            {
                character.characterInventoryManager.rightWeapon = unarmedWeapon;
                rightHandSlot.currentWeapon = unarmedWeapon;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                //animatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        }
    }

    protected virtual void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        leftHandDamageCollider.physicalDamage = character.characterInventoryManager.leftWeapon.physicalDamage;
        leftHandDamageCollider.fireDamage = character.characterInventoryManager.leftWeapon.fireDamage;

        leftHandDamageCollider.characterManager = character;
        leftHandDamageCollider.teamIDNumber = character.characterStatsManager.teamIDNumber;

        leftHandDamageCollider.poiseBreak = character.characterInventoryManager.leftWeapon.poiseBreak;
    }

    protected virtual void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        rightHandDamageCollider.physicalDamage = character.characterInventoryManager.rightWeapon.physicalDamage;
        rightHandDamageCollider.fireDamage = character.characterInventoryManager.rightWeapon.fireDamage;

        rightHandDamageCollider.characterManager = character;
        rightHandDamageCollider.teamIDNumber = character.characterStatsManager.teamIDNumber;

        rightHandDamageCollider.poiseBreak = character.characterInventoryManager.rightWeapon.poiseBreak;
    }

    public virtual void OpenDamageCollider()
    {
        //character.characterSoundFXManager.PlayRandomWeaponWhoosh();

        if (character.isUsingRightHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        else if (character.isUsingLeftHand)
        {
            leftHandDamageCollider.EnableDamageCollider();
        }
    }

    public virtual void CloseDamageCollider()
    {
        if (rightHandDamageCollider != null)
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        if (leftHandDamageCollider != null)
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
    }

    public virtual void GrantWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
    }

    public virtual void ResetWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.armorPoiseBonus;
    }
}
