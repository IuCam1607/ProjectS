using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryManager : MonoBehaviour
{

    [Header("Quick Slots Item")]
    public SpellItem currentSpell;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public ConsumableItem currentConsumable;

    [Header("Current Equipment")]
    public HelmetEquipment currentHelmetEquipment;

    [Header("Quick Slots Weapon")]
    public int rightHandWeaponIndex;
    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    public int leftHandWeaponIndex;
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];




    protected virtual void Start()
    {
        if (rightWeapon != null)
        {
            rightWeapon = weaponsInRightHandSlots[0];
        }

        if (leftWeapon != null)
        {
            leftWeapon = weaponsInLeftHandSlots[0];
        }
    }
}
