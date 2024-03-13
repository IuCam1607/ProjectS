using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponSlotManager : MonoBehaviour
{
    protected CharacterManager characterManager;
    protected CharacterInventoryManager characterInventoryManager;
    protected CharacterStatsManager characterStatsManager;
    protected AnimatorManager animatorManager;
    

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
        characterManager = GetComponent<CharacterManager>();
        characterInventoryManager = GetComponent<CharacterInventoryManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        animatorManager = GetComponent<AnimatorManager>();
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
        LoadWeaponOnSlot(characterInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(characterInventoryManager.leftWeapon, true);
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
                if (characterManager.isTwoHandingWeapon)
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
                characterInventoryManager.leftWeapon = unarmedWeapon;
                leftHandSlot.currentWeapon = unarmedWeapon;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else
            {
                characterInventoryManager.rightWeapon = unarmedWeapon;
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

        leftHandDamageCollider.physicalDamage = characterInventoryManager.leftWeapon.physicalDamage;
        leftHandDamageCollider.fireDamage = characterInventoryManager.leftWeapon.fireDamage;

        leftHandDamageCollider.characterManager = characterManager;
        leftHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

        leftHandDamageCollider.poiseBreak = characterInventoryManager.leftWeapon.poiseBreak;
    }

    protected virtual void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        rightHandDamageCollider.physicalDamage = characterInventoryManager.rightWeapon.physicalDamage;
        rightHandDamageCollider.fireDamage = characterInventoryManager.rightWeapon.fireDamage;

        rightHandDamageCollider.characterManager = characterManager;
        rightHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

        rightHandDamageCollider.poiseBreak = characterInventoryManager.rightWeapon.poiseBreak;
    }

    public virtual void OpenDamageCollider()
    {
        if (characterManager.isUsingRightHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        else if (characterManager.isUsingLeftHand)
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
        characterStatsManager.totalPoiseDefence = characterStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
    }

    public virtual void ResetWeaponAttackingPoiseBonus()
    {
        characterStatsManager.totalPoiseDefence = characterStatsManager.armorPoiseBonus;
    }
}
